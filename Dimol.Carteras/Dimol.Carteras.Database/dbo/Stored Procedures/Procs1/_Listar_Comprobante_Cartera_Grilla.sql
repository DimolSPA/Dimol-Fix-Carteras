-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Comprobante_Cartera_Grilla]
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

@rut varchar(20),
@nombre_fantasia varchar(800),
@telefono varchar(100),
@email varchar(100) ,
@direccion varchar(100),

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


set @query_final = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = 'SELECT DISTINCT cbc_tpcid IdTipoDocumento,   
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
FROM {oj view_cabecera_comprobantes LEFT OUTER JOIN detalle_comprobantes ON view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp AND view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid AND view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid AND view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero}  
WHERE  view_cabecera_comprobantes.cbc_codemp =  '+ convert(char,@codemp) +'
and view_cabecera_comprobantes.cbc_sucid = '+convert(char,@codsuc)+
' and view_cabecera_comprobantes.idi_idid =  '+convert(char,@idioma)+
' and cbc_tpcid in (select tpc_tpcid from View_Tipos_CpbtDoc_Clasificacion where  tpc_codemp = '+convert(char,@codemp) +
' and clb_cartcli =''S'')'
If @tipo > 0 begin
	set @query =@query + ' and cbc_tpcid ='+convert(char,@tipo)
End
If @numero <> 0 begin
    set @query =@query + ' and cbc_numero ='+convert(char,@numero)
End 
If @pclid <> '' begin
    set @query =@query + ' and cbc_pclid = '+convert(char,@pclid) 
End 
If @emision_desde <> '' begin
    set @query =@query + ' and cbc_feccpbt >=''' + @emision_desde + ''''
End 
If @emision_hasta <>'' begin
    set @query =@query + ' and cbc_feccpbt <=''' + @emision_hasta + ''''
End 
If @vencimiento_desde <> '' begin
    set @query =@query + ' and cbc_fecvenc >=''' + @vencimiento_desde + ''''
End
If @vencimiento_hasta<>'' begin
    set @query =@query + ' and cbc_fecvenc <=''' + @vencimiento_hasta + ''''
End 
If @monto_desde > 0 begin
    set @query =@query + ' and cbc_final >='+convert(char,@monto_desde) 
End 
If @monto_hasta > 0 begin
    set @query =@query + ' and cbc_final <='+convert(char,@monto_hasta) 
End 
set @query_final = @query_final + @query
if  @rut<>''  or @nombre_fantasia<>'' or @telefono<>'' or @email<>'' or @direccion<>'' 
begin
set @query2 = @query
set @query =@query + 'and convert(varchar, dcc_pclid) + ''_'' + convert(varchar, dcc_ctcid) + ''_'' + convert(varchar, dcc_ccbid) in ('
set @query =@query + 'SELECT convert(varchar, ccb_pclid) + ''_'' + convert(varchar, ccb_ctcid) + ''_'' + convert(varchar, ccb_ccbid)
FROM cartera_clientes_documentos_cpbt_doc
WHERE cartera_clientes_documentos_cpbt_doc.ccb_codemp ='+ convert(char,@codemp) 

If @pclid > 0 begin
    set @query =@query + ' and ccb_pclid = '+ convert(char,@pclid)
End
If @rut <> '' begin
    set @query =@query + ' and ctc_rut like ''%' + @rut+ '%'''
End
If @nombre_fantasia <> '' begin
    set @query =@query + ' and ctc_nomfant like ''%' + @nombre_fantasia + '%'''
End
If @telefono <> '' begin
    set @query =@query + ' and ccb_ctcid in ('
    set @query =@query + ' select ddt_ctcid from deudores_telefonos where ddt_codemp = '+ convert(char,@codemp) 
    set @query =@query + ' and ddt_numero like ''%' + @telefono + '%'''
    set @query =@query + ' UNION '
    set @query =@query + ' select dct_ctcid from deudores_contactos_telefonos where dct_codemp = '+ convert(char,@codemp) 
    set @query =@query + ' and dct_numero like ''%' + @telefono + '%'''
    set @query =@query + ' ) '
End 
If @email <> '' begin
    set @query =@query + ' and ccb_ctcid in ('
    set @query =@query + ' select ddm_ctcid from deudores_mail where ddm_codemp = '+ convert(char,@codemp)
    set @query =@query + ' and ddm_mail like ''%' + upper(@email) + '%'''
    set @query =@query + ' UNION '
    set @query =@query + ' select dcm_ctcid from deudores_contactos_mail where dcm_codemp = '+ convert(char,@codemp)
    set @query =@query + ' and dcm_mail like ''%' + upper(@email) + '%'''
    set @query =@query + ' ) '
End
If @direccion <> '' begin
    set @query =@query + ' and ctc_direccion like ''%' + @direccion + '%'''
End



set @query =@query + ' ) '

set @query2 = @query2 + 'and convert(varchar, dcc_anio) + ''_'' + convert(varchar, dcc_numapl) + ''_'' + convert(varchar, dcc_itemapl) in ('
set @query2 = @query2 + 'SELECT convert(varchar, api_anio) + ''_'' + convert(varchar, api_numapl) + ''_'' + convert(varchar, api_item)'
set @query2 = @query2 + ' FROM cartera_clientes_documentos_cpbt_doc, aplicaciones_items'
set @query2 = @query2 + ' WHERE cartera_clientes_documentos_cpbt_doc.ccb_codemp ='+ convert(char,@codemp)
set @query2 = @query2 + ' and ccb_codemp = api_codemp '
set @query2 = @query2 + ' and ccb_pclid = api_pclid '
set @query2 = @query2 + ' and ccb_ctcid = api_ctcid '
set @query2 = @query2 + ' and ccb_ccbid = api_ccbid '

If @pclid > 0 begin
    set @query2 = @query2 + ' and ccb_pclid = '+ convert(char,@pclid)
End 
If @rut <> '' begin
    set @query2 = @query2 + ' and ctc_rut like ''%' + @rut+ '%'''
End 
If @nombre_fantasia <> '' begin
    set @query2 = @query2 + ' and ctc_nomfant like ''%' + @nombre_fantasia + '%'''
End 
If @telefono <> '' begin
    set @query2 = @query2 + ' and ccb_ctcid in ('
    set @query2 = @query2 + ' select ddt_ctcid from deudores_telefonos where ddt_codemp = '+ convert(char,@codemp)
    set @query2 = @query2 + ' and ddt_numero like ''%' + @telefono + '%'''
    set @query2 = @query2 + ' UNION '
    set @query2 = @query2 + ' select dct_ctcid from deudores_contactos_telefonos where dct_codemp = '+ convert(char,@codemp)
    set @query2 = @query2 + ' and dct_numero like ''%' + @telefono + '%'''
    set @query2 = @query2 + ' ) '
End 
If @email <> '' begin
    set @query2 = @query2 + ' and ccb_ctcid in ('
    set @query2 = @query2 + ' select ddm_ctcid from deudores_mail where ddm_codemp = '+ convert(char,@codemp)
    set @query2 = @query2 + ' and ddm_mail like ''%' + upper(@email) + '%'''
    set @query2 = @query2 + ' UNION '
    set @query2 = @query2 + ' select dcm_ctcid from deudores_contactos_mail where dcm_codemp = '+ convert(char,@codemp)
    set @query2 = @query2 + ' and dcm_mail like ''%' + upper(@email) + '%'''
    set @query2 = @query2 + ' ) '
End 
If @direccion <> '' begin
    set @query2 = @query2 + ' and ctc_direccion like ''%' + @direccion + '%'''
End
set @query2 =@query2 + ' ) '


set @query_final = @query_final + ' UNION ' + @query2
end



set @query_final = @query_final +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query_final = @query_final + @where;
end

--select @query
--select @query2
--select @query_final
exec(@query_final)	
	

END
