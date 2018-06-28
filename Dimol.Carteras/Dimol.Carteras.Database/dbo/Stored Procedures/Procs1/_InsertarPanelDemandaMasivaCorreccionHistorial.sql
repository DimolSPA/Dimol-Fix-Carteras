
-- =============================================
-- Author:		César León
-- Create date: 09-04-2018
-- Description:	Inserta un registro en la tabla PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL
-- =============================================
CREATE PROCEDURE [dbo].[_InsertarPanelDemandaMasivaCorreccionHistorial](
	@panelId int,
	@fechaEntrega datetime,
	@usrId int,
	@fechaRegistro datetime
)
AS
BEGIN
	DECLARE @idCorreccion int = 0

	SET @idCorreccion = (SELECT IsNull(Max(IDCORRECCION)+1, 1) FROM PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL)

	INSERT INTO [dbo].[PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL]
	(
		[ID_PANEL_MASIVO]
		,[IDCORRECCION]
		,[FEC_ENTREGA]
		,[USRID_REGISTRO]
		,[FEC_REGISTRO]
	)
	VALUES
	(
		@panelId,
		@idCorreccion,
		@fechaEntrega,
		@usrId,
		@fechaRegistro
	)
END
