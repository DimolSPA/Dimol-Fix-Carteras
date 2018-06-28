-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Comprobante_Aceptar_Grilla]
(
@codemp int,
@idioma int,
@codsuc int ,
@tipo varchar(2) ,
@estado varchar(2),
@cartera varchar(2),
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(max) = ''



set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query + 'SELECT vcc.cbc_tpcid IdTipoDocumento, vcc.cbc_numero Numero,  vcc.pcl_rut Rut,   vcc.pcl_nomfant NombreFantasia,   case vcc.cbc_tpcid when 35 then ''BHM'' when 59 then ''BHE'' end TipoDocumento, vcc.cbc_numprovcli NumeroCliente, vcc.cbc_feccpbt Fecha, vcc.mon_nombre Moneda, vcc.cbc_final Monto,   ccc.clb_contable Contable, ccc.clb_cartcli Cartera,   ccc.clb_findeuda FinDeuda, cbc_tipcambio TipoCambio, cbc_feccont FechaContable,
(select top 1 i.INS_NOMBRE from DETALLE_COMPROBANTES d inner join INSUMOS i on i.INS_CODEMP = d.DCC_CODEMP and i.INS_INSID = d.DCC_INSID where d.DCC_CODEMP = vcc.cbc_codemp and d.DCC_TPCID = vcc.cbc_tpcid and d.DCC_NUMERO=vcc.cbc_numero) Gestion,
(select USR_NOMBRE  from CABACERA_COMPROBANTES_ESTADOS e  inner join USUARIOS u on u.USR_CODEMP = e.CBE_CODEMP and u.USR_USRID = e.CBE_USRID where e.CBE_ESTADO = ''E'' and e.CBE_CODEMP = vcc.cbc_codemp and e.CBE_TPCID = vcc.cbc_tpcid and e.CBE_NUMERO = vcc.cbc_numero) Usuario, 
(select top 1 CTC_NOMBRE from DEUDORES left join rol on rol_ctcid = ctc_ctcid and rol_codemp = ctc_codemp left join detalle_comprobantes_rol on rol_rolid = dcr_rolid and rol_codemp = dcr_codemp where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero) Deudor, 
(select top 1 rol_numero from rol left join detalle_comprobantes_rol on rol.ROL_CODEMP = detalle_comprobantes_rol.DCR_CODEMP and rol.ROL_ROLID = detalle_comprobantes_rol.DCR_ROLID  where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero) Rol, 
(select top 1 trb_nombre from tribunales left join rol on trb_codemp = rol_codemp and rol_trbid = trb_trbid left join detalle_comprobantes_rol on rol.ROL_CODEMP = detalle_comprobantes_rol.DCR_CODEMP and rol.ROL_ROLID = detalle_comprobantes_rol.DCR_ROLID where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero) Tribunal,  
dbo._Trae_Asegurado_Rol (vcc.cbc_codemp, isnull((select top 1 dcr_rolid from detalle_comprobantes_rol where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero),0)) Asegurado,   
isnull((select distinct top 1 PCL_PCLID from DETALLE_COMPROBANTES_ROL inner join ROL on DCR_CODEMP=ROL_CODEMP and DCR_ROLID=ROL_ROLID inner join PROVCLI on ROL_CODEMP=PCL_CODEMP and ROL_PCLID=PCL_PCLID where vcc.cbc_codemp=DCR_CODEMP and vcc.cbc_tpcid=DCR_TPCID and vcc.cbc_numero=DCR_NUMERO), 0) PCLID,
isnull((select distinct top 1 ROL_CTCID from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp=ROL_CODEMP and DCR_ROLID=ROL_ROLID inner join TRIBUNALES on DCR_CODEMP=TRB_CODEMP and ROL_TRBID=TRB_TRBID where dcr_codemp=vcc.cbc_codemp and dcr_sucid=vcc.cbc_sucid and dcr_tpcid=vcc.cbc_tpcid and dcr_numero=vcc.cbc_numero), 0) CTCID,
isnull((select distinct top 1 ROL_ROLID from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp=ROL_CODEMP and DCR_ROLID=ROL_ROLID inner join TRIBUNALES on DCR_CODEMP=TRB_CODEMP and ROL_TRBID=TRB_TRBID where dcr_codemp=vcc.cbc_codemp and dcr_sucid=vcc.cbc_sucid and dcr_tpcid=vcc.cbc_tpcid and dcr_numero=vcc.cbc_numero), 0) ROLID 
FROM view_cabecera_comprobantes vcc, clasificacion_cpbtdoc ccc, tipos_cpbtdoc tcc WHERE  tcc.tpc_codemp = ccc.clb_codemp  and tcc.tpc_clbid = ccc.clb_clbid  and  vcc.cbc_codemp = tcc.tpc_codemp  and vcc.cbc_tpcid = tcc.tpc_tpcid  and  vcc.cbc_codemp =  '+ convert(char,@codemp) +' and vcc.cbc_sucid = '+ convert(char,@codsuc) +' and vcc.cbt_estado = '''+ @estado +''' and vcc.idi_idid =  '+ convert(char,@idioma) +' and clb_cartcli = '''+ @cartera +''' and clb_tipcpbtdoc = '''+ @tipo +''' and cbc_tpcid in (35,59)
union 
SELECT vcc.cbc_tpcid, vcc.cbc_numero,  vcc.pcl_rut, vcc.pcl_nomfant, case vcc.cbc_tpcid when 35 then ''BHM'' when 59 then ''BHE'' end TipoDocumento, vcc.cbc_numprovcli, vcc.cbc_feccpbt, vcc.mon_nombre, vcc.cbc_final, ccc.clb_contable, ccc.clb_cartcli, ccc.clb_findeuda, cbc_tipcambio, cbc_feccont,
(select top 1 i.INS_NOMBRE  from DETALLE_COMPROBANTES d inner join INSUMOS i on i.INS_CODEMP = d.DCC_CODEMP and i.INS_INSID = d.DCC_INSID where d.DCC_CODEMP = vcc.cbc_codemp and d.DCC_TPCID = vcc.cbc_tpcid and d.DCC_NUMERO=vcc.cbc_numero) Gestion,
(select USR_NOMBRE from CABACERA_COMPROBANTES_ESTADOS e inner join USUARIOS u on u.USR_CODEMP = e.CBE_CODEMP and u.USR_USRID = e.CBE_USRID where e.CBE_ESTADO = ''E'' and e.CBE_CODEMP = vcc.cbc_codemp and e.CBE_TPCID = vcc.cbc_tpcid and e.CBE_NUMERO = vcc.cbc_numero) Usuario, 
(select top 1 CTC_NOMFANT from DEUDORES left join rol on rol_ctcid = ctc_ctcid and rol_codemp = ctc_codemp left join detalle_comprobantes_rol on rol_rolid = dcr_rolid and rol_codemp = dcr_codemp where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero) Deudor, 
(select top 1 rol_numero from rol left join detalle_comprobantes_rol on rol.ROL_CODEMP = detalle_comprobantes_rol.DCR_CODEMP and rol.ROL_ROLID = detalle_comprobantes_rol.DCR_ROLID  where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero) Rol, 
(select top 1 trb_nombre from tribunales left join rol on trb_codemp = rol_codemp and rol_trbid = trb_trbid left join detalle_comprobantes_rol on rol.ROL_CODEMP = detalle_comprobantes_rol.DCR_CODEMP and rol.ROL_ROLID = detalle_comprobantes_rol.DCR_ROLID where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero) Tribunal,  
dbo._Trae_Asegurado_Rol (vcc.cbc_codemp, isnull((select top 1 dcr_rolid from detalle_comprobantes_rol where detalle_comprobantes_rol.dcr_codemp = vcc.cbc_codemp and detalle_comprobantes_rol.dcr_sucid = vcc.cbc_sucid and detalle_comprobantes_rol.dcr_tpcid = vcc.cbc_tpcid and detalle_comprobantes_rol.dcr_numero = vcc.cbc_numero),0)) Asegurado,  
isnull((select distinct top 1 PCL_PCLID from DETALLE_COMPROBANTES_ROL inner join ROL on DCR_CODEMP=ROL_CODEMP and DCR_ROLID=ROL_ROLID inner join PROVCLI on ROL_CODEMP=PCL_CODEMP and ROL_PCLID=PCL_PCLID where vcc.cbc_codemp=DCR_CODEMP and vcc.cbc_tpcid=DCR_TPCID and vcc.cbc_numero=DCR_NUMERO), 0) PCLID,
isnull((select distinct top 1 ROL_CTCID from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp=ROL_CODEMP and DCR_ROLID=ROL_ROLID inner join TRIBUNALES on DCR_CODEMP=TRB_CODEMP and ROL_TRBID=TRB_TRBID where dcr_codemp=vcc.cbc_codemp and dcr_sucid=vcc.cbc_sucid and dcr_tpcid=vcc.cbc_tpcid and dcr_numero=vcc.cbc_numero), 0) CTCID,
isnull((select distinct top 1 ROL_ROLID from DETALLE_COMPROBANTES_ROL inner join rol on dcr_codemp=ROL_CODEMP and DCR_ROLID=ROL_ROLID inner join TRIBUNALES on DCR_CODEMP=TRB_CODEMP and ROL_TRBID=TRB_TRBID where dcr_codemp=vcc.cbc_codemp and dcr_sucid=vcc.cbc_sucid and dcr_tpcid=vcc.cbc_tpcid and dcr_numero=vcc.cbc_numero), 0) ROLID 
FROM view_cabecera_comprobantes vcc, clasificacion_cpbtdoc ccc, tipos_cpbtdoc tcc WHERE  tcc.tpc_codemp = ccc.clb_codemp  and tcc.tpc_clbid = ccc.clb_clbid  and  vcc.cbc_codemp = tcc.tpc_codemp  and vcc.cbc_tpcid = tcc.tpc_tpcid  and  vcc.cbc_codemp = '+ convert(char,@codemp) +' and vcc.cbc_sucid =  '+ convert(char,@codsuc) +' and vcc.cbt_estado = '''+ @estado +''' and vcc.idi_idid =  '+ convert(char,@idioma) +' and clb_cartcli <> '''+ @cartera +''' and clb_tipcpbtdoc = '''+ @tipo +''' and cbc_tpcid in (35,59)'

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
