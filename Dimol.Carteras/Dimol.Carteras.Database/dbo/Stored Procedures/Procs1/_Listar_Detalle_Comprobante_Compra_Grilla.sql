-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Detalle_Comprobante_Compra_Grilla]
(
@codemp int,
@sucid int , 
@tpcid int ,
@numero int ,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = ''
declare @query2 varchar(8000)= ''
declare @query_final varchar(8000)=''


set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query + 'SELECT detalle_comprobantes.dcc_item Item,   
detalle_comprobantes.dcc_insid Insid,   
insumos.ins_codigo Codigo,   
insumos.ins_nombre Nombre, 
insumos.ins_abreviado Abreviado,
detalle_comprobantes.dcc_prereal PrecioReal,   
detalle_comprobantes.dcc_precio Precio,   
detalle_comprobantes.dcc_cantidad Cantidad,   
detalle_comprobantes.dcc_neto Neto,   
detalle_comprobantes.dcc_total Total,   
detalle_comprobantes.dcc_impuesto Impuesto,   
case detalle_comprobantes.dcc_retenido 
when ''S'' then ''SI'' else ''NO'' end as Retenido,
bodegas.bod_nombre NombreBodega,   
bodegas_sector.bds_nombre NombreSectorBodega, 
dcc_prereal * dcc_cantidad  as TotalNeto,
isnull(convert(varchar,(select top 1 DDE_FECJUD from Deudores_Estampes where DCC_CODEMP = DDE_CODEMP AND DCC_TPCID = DDE_TPCID AND DCC_NUMERO = DDE_NUMERO AND DCC_ITEM = DDE_ITEM AND DCC_INSID = DDE_INSID AND DDE_ROLID = (select DCR_ROLID from detalle_comprobantes_rol where DCR_CODEMP = DCC_CODEMP AND DCR_SUCID = DCC_SUCID AND DCC_TPCID = DCR_TPCID AND DCR_NUMERO = DCC_NUMERO AND DCR_ITEM = DCC_ITEM) ORDER BY DDE_DDEID DESC),105), '''') as FecJud, 
(select top 1 CAST(DDE_PCLID AS VARCHAR) + ''\\'' + CAST(DDE_CTCID AS VARCHAR) + ''\\'' + CAST(DDE_ROLID AS VARCHAR) + ''\\'' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT from Deudores_Estampes where DCC_CODEMP = DDE_CODEMP AND DCC_TPCID = DDE_TPCID AND DCC_NUMERO = DDE_NUMERO AND DCC_ITEM = DDE_ITEM AND DCC_INSID = DDE_INSID AND DDE_ROLID = (select DCR_ROLID from detalle_comprobantes_rol where DCR_CODEMP = DCC_CODEMP AND DCR_SUCID = DCC_SUCID AND DCC_TPCID = DCR_TPCID AND DCR_NUMERO = DCC_NUMERO AND DCR_ITEM = DCC_ITEM) ORDER BY DDE_DDEID DESC) as ArchivoEstampe,
(select top 1 DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT from Deudores_Estampes where DCC_CODEMP = DDE_CODEMP AND DCC_TPCID = DDE_TPCID AND DCC_NUMERO = DDE_NUMERO AND DCC_ITEM = DDE_ITEM AND DCC_INSID = DDE_INSID AND DDE_ROLID = (select DCR_ROLID from detalle_comprobantes_rol where DCR_CODEMP = DCC_CODEMP AND DCR_SUCID = DCC_SUCID AND DCC_TPCID = DCR_TPCID AND DCR_NUMERO = DCC_NUMERO AND DCR_ITEM = DCC_ITEM) ORDER BY DDE_DDEID DESC) as NombreArchivo 
FROM {oj detalle_comprobantes LEFT OUTER JOIN bodegas_sector ON detalle_comprobantes.dcc_codemp = bodegas_sector.bds_codemp AND detalle_comprobantes.dcc_bodid = bodegas_sector.bds_bodid AND detalle_comprobantes.dcc_bdsid = bodegas_sector.bds_bdsid 
LEFT OUTER JOIN bodegas ON bodegas_sector.bds_codemp = bodegas.bod_codemp AND bodegas_sector.bds_bodid = bodegas.bod_bodid},   
insumos
WHERE  insumos.ins_codemp = detalle_comprobantes.dcc_codemp  and  
insumos.ins_insid = detalle_comprobantes.dcc_insid  and  
detalle_comprobantes.dcc_codemp =  '+ convert(char,@codemp) +'
and detalle_comprobantes.dcc_sucid =  '+ convert(char,@sucid) +'
and detalle_comprobantes.dcc_tpcid =  '+ convert(char,@tpcid) +'
and detalle_comprobantes.dcc_numero =  '+ convert(char,@numero)

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
