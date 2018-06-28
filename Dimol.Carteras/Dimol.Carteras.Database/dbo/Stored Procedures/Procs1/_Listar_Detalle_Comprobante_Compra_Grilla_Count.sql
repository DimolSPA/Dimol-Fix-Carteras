-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create  PROCEDURE [dbo].[_Listar_Detalle_Comprobante_Compra_Grilla_Count]
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


set @query = 'select count(*) count from
  (select *,ROW_NUMBER() OVER (ORDER BY item asc) as row from    
  ('

set @query = @query + 'SELECT detalle_comprobantes.dcc_item Item,   
detalle_comprobantes.dcc_insid Insid,   
insumos.ins_codigo Codigo,   
insumos.ins_nombre Nombre,   
detalle_comprobantes.dcc_prereal PrecioReal,   
detalle_comprobantes.dcc_precio Precio,   
detalle_comprobantes.dcc_cantidad Cantidad,   
detalle_comprobantes.dcc_neto Neto,   
detalle_comprobantes.dcc_total Total,   
detalle_comprobantes.dcc_impuesto Impuesto,   
case detalle_comprobantes.dcc_retenido 
when ''S'' then ''SI'' else '''' end as Retenido,
bodegas.bod_nombre NombreBodega,   
bodegas_sector.bds_nombre NombreSectorBodega, 
dcc_prereal * dcc_cantidad as TotalNeto
FROM {oj detalle_comprobantes LEFT OUTER JOIN bodegas_sector ON detalle_comprobantes.dcc_codemp = bodegas_sector.bds_codemp AND detalle_comprobantes.dcc_bodid = bodegas_sector.bds_bodid AND detalle_comprobantes.dcc_bdsid = bodegas_sector.bds_bdsid LEFT OUTER JOIN bodegas ON bodegas_sector.bds_codemp = bodegas.bod_codemp AND bodegas_sector.bds_bodid = bodegas.bod_bodid},   
insumos
WHERE  insumos.ins_codemp = detalle_comprobantes.dcc_codemp  and  
insumos.ins_insid = detalle_comprobantes.dcc_insid  and  
detalle_comprobantes.dcc_codemp =  '+ convert(char,@codemp) +'
and detalle_comprobantes.dcc_sucid =  '+ convert(char,@sucid) +'
and detalle_comprobantes.dcc_tpcid =  '+ convert(char,@tpcid) +'
and detalle_comprobantes.dcc_numero =  '+ convert(char,@numero)

set @query = @query +') as tabla  ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
