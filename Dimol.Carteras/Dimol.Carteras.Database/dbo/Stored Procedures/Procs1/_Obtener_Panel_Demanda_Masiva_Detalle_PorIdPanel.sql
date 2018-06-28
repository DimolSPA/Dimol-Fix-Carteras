
-- =============================================
-- Author:		César León
-- Create date: 25-04-2018
-- Description:	Obtiene un registro de la tabla Panel_Demanda_Masiva_Detalle
-- =============================================
CREATE PROCEDURE [dbo].[_Obtener_Panel_Demanda_Masiva_Detalle_PorIdPanel]
(
	@IdPanel int
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT ID_PANEL_MASIVO
      ,USRID_ENCARGADO
      ,FEC_ENVIO
      ,FEC_ENTREGA
      ,FEC_INGRESO_TRIBUNAL
      ,ROL_ADJUDICADO
      ,ROLID
      ,COMENTARIOS
      ,USRID_REGISTRO
      ,FEC_REGISTRO
	FROM PANEL_DEMANDA_MASIVA_DETALLE
	WHERE ID_PANEL_MASIVO = @IdPanel
END
