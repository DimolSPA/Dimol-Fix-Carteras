-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Roles_Pendientes_Spider_Cliente]
(
@codemp int,
@idioma int,
@pclid int,
@estados varchar(1000)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select rol_codemp, rol_rolid, rol_numero, trb_nombre, isnull(rol_tipo_rol,''C'') rol_tipo_rol, ISNULL(RPJ_ID_CAUSA, 0) id_causa,
  dbo._Trae_Ultimo_Historial_PJ(RPJ_ID_CAUSA) ult_historial,
  dbo._Trae_Ultimo_Receptor_PJ(RPJ_ID_CAUSA) ult_receptor

	from view_rol left join  [ROL_PODER_JUDICIAL] on rol_codemp = RPJ_CODEMP
	and rol_rolid = RPJ_ROLID
	where rol_codemp = ' + CONVERT(VARCHAR,@codemp) + '
	and rol_pclid = ' + CONVERT(VARCHAR,@pclid) + '
	and eci_idid = ' + CONVERT(VARCHAR,@idioma) + '
	and tci_idid = ' + CONVERT(VARCHAR,@idioma) + '
	and mji_idid = ' + CONVERT(VARCHAR,@idioma) + '
	and rol_numero like ''%-%''
	and rol_estid not in (' + @estados + ')
	
	and rol_rolid not in (
	select distinct rol_rolid from (
select --r.*,
r.pcl_rut "RUT CLIENTE", r.rol_rolid,
r.pcl_nomfant "NOMBRE CLIENTE",
r.ctc_rut "RUT DEUDOR",
r.ctc_nomfant "NOMBRE DEUDOR",
r.rol_numero ROL,
r.trb_nombre TRIBUNAL,
t.TRIBUNAL TRIB_PJ,
r.rol_fecemi "FECHA INGRESO",
c.Desc_cuaderno CUADERNO,
h.ETAPA, 
h.TRAMITE,
h.DESC_TRAMITE DESCRIPCION,
h.FECHA_TRAMITE
from rol_poder_judicial rpj, poder_judicial_tribunal t, view_rol r, PODER_JUDICIAL_CUADERNO c,PODER_JUDICIAL_HISTORIAL h
where rpj.rpj_tribunal = t.id_tribunal
and r.rol_codemp = rpj.rpj_codemp
and r.rol_rolid = rpj.rpj_rolid
and c.ID_CAUSA = rpj.rpj_ID_CAUSA
and h.ID_CAUSA = rpj.rpj_ID_CAUSA
and c.id_cuaderno = h.id_cuaderno
--order by ctc_nomfant, c.Desc_cuaderno, h.FECHA_TRAMITE
) as a
	
	
	)
	order by rol_fecdem desc'

  

--select @query
 exec(@query)	
	

END
