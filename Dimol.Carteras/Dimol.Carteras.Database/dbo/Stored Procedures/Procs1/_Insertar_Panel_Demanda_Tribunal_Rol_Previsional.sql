
CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Tribunal_Rol_Previsional]
(
	@panelId int,
	@fecIngresoTribunal datetime,
	@RolAdjudicado varchar(30),
	@RolId int,
	@ingresarFechaEntrega varchar(1),
	@fecEntrega datetime,
	@user int
)
AS 
BEGIN
	declare @panelCorrecion int = 0

	UPDATE PANEL_DEMANDA_PREVISIONAL_DETALLE
	SET FEC_INGRESO_TRIBUNAL = @fecIngresoTribunal, ROL_ADJUDICADO = @RolAdjudicado, ROLID = @RolId
	WHERE PANEL_ID = @panelId

	UPDATE PANEL_DEMANDA_PREVISIONAL
	SET PROCESADA = 'S'
	WHERE PANEL_ID = @panelId

	if @ingresarFechaEntrega = 'S'
	begin
		UPDATE PANEL_DEMANDA_PREVISIONAL_DETALLE
		SET FEC_ENTREGA = @fecEntrega
		WHERE PANEL_ID = @panelId

		SET @panelCorrecion = (SELECT IsNull(Max(IDCORRECCION)+1, 1)
								FROM PANEL_DEMANDA_PREVISIONAL_CORRECCION_HISTORIAL)
		INSERT INTO PANEL_DEMANDA_PREVISIONAL_CORRECCION_HISTORIAL(PANEL_ID, IDCORRECCION, FEC_ENTREGA, USRID_REGISTRO)
		VALUES(@panelId, @panelCorrecion, @fecEntrega, @user)
	end
END