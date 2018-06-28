-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Roles_Spider_Deudor]
(
@codemp int,
@idioma int,
@pclid int,
@ctcid int
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
	and rol_rolid = ' + CONVERT(VARCHAR,@ctcid) + '
	
	order by rol_fecdem desc'

  

--select @query
exec(@query)	
	

END
