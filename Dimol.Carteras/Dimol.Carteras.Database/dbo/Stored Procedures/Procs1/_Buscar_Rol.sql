-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento obtener un rol por su id
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Rol]
(
	@codemp int,
	@rolid int,
	@idioma int 
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		[rol_codemp]
		,[rol_rolid]
		,[rol_pclid]
		,[rol_ctcid]
		,[rol_numero]
		,[rol_trbid]
		,[rol_tcaid]
		,[rol_estid]
		,[rol_fecemi]
		,[rol_fecdem]
		,[rol_fecrol]
		,[rol_total]
		,[rol_comentario]
		,[rol_esjid]
		,[rol_fecjud]
		,[rol_fecultgest]
		,[trb_nombre]
		,[eci_idid]
		,[eci_nombre]
		,[tci_idid]
		,[tci_nombre]
		,[mji_idid]
		,[mji_nombre]
		,[pcl_rut]
		,[pcl_nomfant]
		,[ctc_rut]
		,[ctc_nomfant]
		,[pai_paiid]
		,[pai_nombre]
		,[pai_codtel]
		,[reg_regid]
		,[reg_nombre]
		,[reg_orden]
		,[ciu_ciuid]
		,[ciu_nombre]
		,[ciu_codarea]
		,[com_comid]
		,[com_nombre]
		,[com_codpost]
		,[trb_direccion]
		,[ctc_numero]
		,[ctc_digito]
		,[ctc_nombre]
		,[ctc_apepat]
		,[ctc_apemat]
		,[ctc_direccion]
		,[rol_bloqueo]
		,[rol_prequiebra]
		,[rol_tipo_rol]
		,(SELECT CTC_QUIEBRA FROM deudores WHERE CTC_CODEMP = rol_codemp AND CTC_CTCID = rol_ctcid) AS ctc_quiebra
		,[ID_COMPETENCIA]
	FROM view_rol 
	WHERE
		rol_codemp = @codemp 
		AND rol_rolid = @rolid
		AND eci_idid = @idioma
		AND tci_idid = @idioma
		AND mji_idid = @idioma
END
