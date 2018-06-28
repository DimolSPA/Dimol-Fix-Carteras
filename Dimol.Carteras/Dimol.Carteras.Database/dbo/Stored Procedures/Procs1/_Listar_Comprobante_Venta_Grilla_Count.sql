-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Comprobante_Venta_Grilla_Count]
(
@codemp int,
@idioma int,
@codsuc int ,
@tipo int ,
@numero int ,
@pclid int ,
@emision_desde varchar(30),
@emision_hasta varchar(30),
@vencimiento_desde varchar(30),
@vencimiento_hasta varchar(30),
@monto_desde int,
@monto_hasta int ,

@estado varchar(1),
@moneda int,
@numero_interno int, 

@producto int,
@comentario int,

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


set @query = 'select count(IdTipoDocumento) as count from
  (select *,ROW_NUMBER() OVER (ORDER BY NombreFantasia asc) as row from    
  ('

set @query = @query + 'SELECT DISTINCT cbc_tpcid IdTipoDocumento,   
cbc_numero Numero,   
pcl_rut Rut,   
pcl_nomfant NombreFantasia,   
tci_nombre TipoDocumento,   
cbc_numprovcli NumeroCliente,   
cbc_final Final,   
cbt_estado IdEstado, 
cbc_numero as NumFin, 
cbc_saldo Saldo,
case cbt_estado 
when ''E'' then ''' + dbo._Trae_Etiqueta('EstCab1', @idioma) + ''' 
when ''A'' then ''' + dbo._Trae_Etiqueta('EstCab2', @idioma) + ''' 
when ''F'' then ''' + dbo._Trae_Etiqueta('EstCab3', @idioma) + ''' 
when ''B'' then ''' + dbo._Trae_Etiqueta('EstCab4', @idioma) + ''' 
when ''C'' then ''' + dbo._Trae_Etiqueta('EstCab5', @idioma) + ''' 
when ''X'' then ''' + dbo._Trae_Etiqueta('EstCab6', @idioma) + ''' end as Estado,
cbc_feccpbt FechaEmision, 
cbc_fecvenc FechaVencimiento
FROM view_cabecera_comprobantes 
WHERE  cbc_codemp =  '+ convert(char,@codemp) +'
and cbc_sucid = '+convert(char,@codsuc)+
' and idi_idid =  '+convert(char,@idioma)+
' and cbc_tpcid in (select tpc_tpcid from View_Tipos_CpbtDoc_Clasificacion where tpc_codemp = '+ convert(char,@codemp) +'
and (clb_cartcli =''N'' or (clb_cartcli =''S'' and clb_selapl = ''S''))
and clb_tipcpbtdoc =''V'')'



If @pclid> 0 begin
    set @query =@query + ' and cbc_pclid = '+convert(char,@pclid) 
End

If @tipo > 0 begin
    set @query =@query + ' and cbc_tpcid = '+convert(char,@tipo) 
End

If @numero <> '' begin
    set @query =@query + ' and cbc_numprovcli like ''%' + @numero+ '%'''
End

If @estado <> 0 begin
    set @query =@query + ' and cbt_estado = ' + @estado + ''''
End

If @moneda > 0 begin
    set @query =@query + ' and cbc_codmon =  '+convert(char,@moneda) 
End

If @emision_desde <> '' begin
    set @query =@query + ' and cbc_feccpbt >=''' + @emision_desde + ''''
End

If @emision_hasta <> '' begin
    set @query =@query + ' and cbc_feccpbt <=''' + @emision_hasta + ''''
End

If @vencimiento_desde <> '' begin
    set @query =@query + ' and cbc_fecvenc >=''' + @vencimiento_desde + ''''
End

If @vencimiento_hasta <> '' begin
    set @query =@query + ' and cbc_fecvenc <=''' + @vencimiento_hasta + ''''
End

If @numero_interno > 0 begin
    set @query =@query + ' and cbc_numero= '+convert(char,@numero_interno) 
End

If @monto_desde > 0 begin
    set @query =@query + ' and cbc_final >= '+convert(char,@monto_desde) 
End

If @monto_hasta > 0 begin
    set @query =@query + ' and cbc_final <= '+convert(char,@monto_hasta) 
End
If @producto <> '' Or @comentario <> '' begin
    set @query =@query + ' and cbc_numprovcli in ( Select cabacera_comprobantes.cbc_numprovcli'
    set @query =@query + '  FROM detalle_comprobantes,  '
    set @query =@query + ' productos,  '
    set @query =@query + '  cabacera_comprobantes'
    set @query =@query + '  WHERE  productos.pdt_codemp = detalle_comprobantes.dcc_codemp    '
    set @query =@query + ' and productos.pdt_prodid = detalle_comprobantes.dcc_prodid    '
    set @query =@query + ' and cabacera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp    '
    set @query =@query + ' and cabacera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid    '
    set @query =@query + ' and cabacera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid  '
    set @query =@query + ' and cabacera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero'
    set @query =@query + ' and dcc_codemp ='+ convert(char,@codemp) 
If @producto <> '' begin
    set @query =@query + ' and pdt_nombre like ''%' + @producto+ '%''' 
End 

If @comentario <> '' begin
    set @query =@query + ' and dcc_comentario like ''%' + @comentario+ '%'''
End 
    set @query =@query + ' )'
End 

set @query = @query +') as tabla  ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
