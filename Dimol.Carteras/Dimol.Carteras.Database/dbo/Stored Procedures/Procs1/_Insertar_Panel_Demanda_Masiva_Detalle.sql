CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Masiva_Detalle]
(
	@panelId int,
	@userEncargado int,
	@fecEnvio datetime,
	@fecEntrega datetime,
	@fecIngreso datetime = NULL,
	@comentarios varchar(250),
	@user int,
	@ingresarFechaEntrega varchar(1)
)
AS
BEGIN

declare
	@panelIdExist int = 0, @panelCorrecion int = 0
	
	SET @panelIdExist = (SELECT count(ID_PANEL_MASIVO)
						FROM PANEL_DEMANDA_MASIVA_DETALLE
						WHERE ID_PANEL_MASIVO = @panelId)
						
	if @panelIdExist = 0
	BEGIN
		INSERT INTO PANEL_DEMANDA_MASIVA_DETALLE(ID_PANEL_MASIVO, USRID_ENCARGADO, FEC_ENVIO, FEC_ENTREGA,FEC_INGRESO_TRIBUNAL, COMENTARIOS, USRID_REGISTRO)
		VALUES(@panelId, @userEncargado, @fecEnvio, @fecEntrega, @fecIngreso,@comentarios, @user)
	END	
	else
	BEGIN
		if @panelIdExist> 0 and @ingresarFechaEntrega = 'S'
		begin
			UPDATE PANEL_DEMANDA_MASIVA_DETALLE
			SET FEC_ENTREGA = @fecEntrega, COMENTARIOS = @comentarios
			WHERE ID_PANEL_MASIVO = @panelId

			SET @panelCorrecion = (SELECT IsNull(Max(IDCORRECCION)+1, 1)
									FROM PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL)
			INSERT INTO PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL(ID_PANEL_MASIVO, IDCORRECCION, FEC_ENTREGA, USRID_REGISTRO)
			VALUES(@panelId, @panelCorrecion, @fecEntrega, @user)
		end
		else
		begin
			if @panelIdExist> 0 and @ingresarFechaEntrega = 'N'
			begin 
				UPDATE PANEL_DEMANDA_MASIVA_DETALLE
				SET USRID_ENCARGADO =  @userEncargado, FEC_INGRESO_TRIBUNAL = @fecIngreso, COMENTARIOS = @comentarios
				WHERE ID_PANEL_MASIVO = @panelId
			end
		end
	END
END