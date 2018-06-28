-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Comprobante_Estado_Grilla]
(
@codemp int,
@idioma int,
@codsuc int ,
@tipo varchar(2) ,
@estado varchar(2),
@desde datetime,
@hasta datetime,
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

set @query = @query + 'SELECT vcc.cbc_tpcid IdTipoDocumento,   
vcc.cbc_numero Numero,   
vcc.pcl_rut Rut,   
vcc.pcl_nomfant NombreFantasia,   
replace(cast(vcc.tci_nombre as varchar(max)),''BOLETA HONORARIOS'' ,''BH'') TipoDocumento ,
vcc.cbc_numprovcli NumeroCliente,
vcc.cbc_neto Bruto,    
vcc.cbc_retenido Retenido,
vcc.cbc_final Monto, 
vcc.cbc_feccpbt Fecha,   
vcc.mon_nombre Moneda,    
ccc.clb_contable Contable,   
ccc.clb_cartcli Cartera,   
ccc.clb_findeuda FinDeuda, 
cbc_tipcambio TipoCambio, 
cbc_feccont FechaContable,
(select  distinct top 1 PCL_NOMFANT
from DETALLE_COMPROBANTES_ROL (nolock)
inner join ROL
on DCR_CODEMP = ROL_CODEMP
and DCR_ROLID = ROL_ROLID
inner join PROVCLI
on ROL_CODEMP = PCL_CODEMP
and ROL_PCLID = PCL_PCLID
where vcc.cbc_codemp = DCR_CODEMP
and vcc.cbc_tpcid = DCR_TPCID
and vcc.cbc_numero = DCR_NUMERO ) Cliente,
(select top 1 USR_NOMBRE from [CABACERA_COMPROBANTES_ESTADOS]  (nolock)
inner join USUARIOS  (nolock)  
on CBE_CODEMP = USR_CODEMP
and CBE_USRID = USR_USRID
and CBE_ESTADO = '''+ @estado +'''
and CBE_SUCID = cbc_sucid
and CBE_CODEMP = cbc_codemp
and CBE_TPCID = cbc_tpcid
and CBE_NUMERO = cbc_numero) Usuario, 
(select distinct top 1 PCL_PCLID
from DETALLE_COMPROBANTES_ROL (nolock)
inner join ROL
on DCR_CODEMP = ROL_CODEMP
and DCR_ROLID = ROL_ROLID
inner join PROVCLI
on ROL_CODEMP = PCL_CODEMP
and ROL_PCLID = PCL_PCLID
where vcc.cbc_codemp = DCR_CODEMP
and vcc.cbc_tpcid = DCR_TPCID
and vcc.cbc_numero = DCR_NUMERO ) PCLID,

(select distinct top 1 ROL_CTCID 
		from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp =ROL_CODEMP and DCR_ROLID =ROL_ROLID
		inner join TRIBUNALES on DCR_CODEMP = TRB_CODEMP and ROL_TRBID = TRB_TRBID
		where dcr_codemp = vcc.cbc_codemp
		and dcr_sucid = vcc.cbc_sucid
		and dcr_tpcid = vcc.cbc_tpcid
		and dcr_numero = vcc.cbc_numero) CTCID,
		(select distinct top 1 ROL_ROLID  
		from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp =ROL_CODEMP and DCR_ROLID =ROL_ROLID
		inner join TRIBUNALES on DCR_CODEMP = TRB_CODEMP and ROL_TRBID = TRB_TRBID
		where dcr_codemp = vcc.cbc_codemp
		and dcr_sucid = vcc.cbc_sucid
		and dcr_tpcid = vcc.cbc_tpcid
		and dcr_numero = vcc.cbc_numero) ROLID    
FROM view_cabecera_comprobantes vcc  (nolock),   
clasificacion_cpbtdoc ccc  (nolock),   
tipos_cpbtdoc tcc  (nolock)
WHERE  tcc.tpc_codemp = ccc.clb_codemp  and  
tcc.tpc_clbid = ccc.clb_clbid  and  
vcc.cbc_codemp = tcc.tpc_codemp  and  
vcc.cbc_tpcid = tcc.tpc_tpcid  and  
vcc.cbc_codemp = '+ convert(char,@codemp) +'
and vcc.cbc_sucid = '+ convert(char,@codsuc) +'
and vcc.cbt_estado = '''+ @estado +'''
and vcc.idi_idid =  '+ convert(char,@idioma) +'
and clb_tipcpbtdoc =  '''+ @tipo +''' 
and cbc_tpcid in ( 35,59 ) '
--Select perfiles_comprobantes.pfc_tpcid
--FROM perfiles_comprobantes
--WHERE  perfiles_comprobantes.pfc_codemp = '+ convert(char,@codemp) +'
--and  perfiles_comprobantes.pfc_prfid =  '+ convert(char,@perfil) +') '

if @desde is not null
begin
	set @query = @query + ' and cbe_fecha >= ' + CONVERT(varchar, @desde, 120)
end 
if @hasta is not null
begin
	set @query = @query + ' and cbe_fecha < ' + CONVERT(varchar, @hasta, 120)
end 

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
