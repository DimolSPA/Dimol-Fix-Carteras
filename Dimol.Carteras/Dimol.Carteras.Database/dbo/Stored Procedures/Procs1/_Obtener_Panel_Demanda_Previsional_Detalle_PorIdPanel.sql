
-- =============================================
-- Author:		César León
-- Create date: 16-04-2018
-- Description:	Obtiene un registro de la tabla Panel_Demanda_Previsional
-- =============================================
CREATE PROCEDURE _Obtener_Panel_Demanda_Previsional_Detalle_PorIdPanel
(
	@IdPanel int
)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT PANEL_ID
      ,USRID_ENCARGADO
      ,FEC_ENVIO
      ,FEC_ENTREGA
      ,FEC_INGRESO_TRIBUNAL
      ,ROL_ADJUDICADO
      ,ROLID
      ,COMENTARIOS
      ,USRID_REGISTRO
      ,FEC_REGISTRO
	FROM PANEL_DEMANDA_PREVISIONAL_DETALLE
	WHERE PANEL_ID = @IdPanel
END
