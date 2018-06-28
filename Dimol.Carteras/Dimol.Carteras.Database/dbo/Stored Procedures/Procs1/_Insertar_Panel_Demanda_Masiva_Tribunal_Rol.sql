CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Masiva_Tribunal_Rol](
@panelId int,
@fecIngresoTribunal datetime,
@RolAdjudicado varchar(30),
@RolId int,
@ingresarFechaEntrega varchar(1),
@fecEntrega datetime,
@user int)
AS 
BEGIN
	declare @panelCorrecion int = 0
	
	UPDATE PANEL_DEMANDA_MASIVA_DETALLE
	SET FEC_INGRESO_TRIBUNAL = @fecIngresoTribunal, ROL_ADJUDICADO = @RolAdjudicado, ROLID = @RolId
	WHERE ID_PANEL_MASIVO = @panelId

	UPDATE PANEL_DEMANDA_MASIVA
	SET PROCESADA = 'S'
	WHERE ID_PANEL_MASIVO = @panelId

	if @ingresarFechaEntrega = 'S'
	begin
		UPDATE PANEL_DEMANDA_MASIVA_DETALLE
		SET FEC_ENTREGA = @fecEntrega
		WHERE ID_PANEL_MASIVO = @panelId
		
		SET @panelCorrecion = (SELECT IsNull(Max(IDCORRECCION)+1, 1)
								FROM PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL)
		INSERT INTO PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL(ID_PANEL_MASIVO, IDCORRECCION, FEC_ENTREGA, USRID_REGISTRO)
		VALUES(@panelId, @panelCorrecion, @fecEntrega, @user)
	end
END
