CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Detalle](
@panelId int,
@userEncargado int,
@fecEnvio datetime,
@fecEntrega datetime,
@fecIngreso datetime,
@comentarios varchar(250),
@user int,
@ingresarFechaEntrega varchar(1))
	
AS
BEGIN

declare
	@panelIdExist int = 0, @panelCorrecion int = 0
	
	-- INSERTAR DATOS EN VISITA TERRENO
	SET @panelIdExist = (SELECT count(PANEL_ID)
						FROM PANEL_DEMANDA_DETALLE
						WHERE PANEL_ID = @panelId)
						
	if @panelIdExist = 0
	BEGIN
		INSERT INTO PANEL_DEMANDA_DETALLE(PANEL_ID, USRID_ENCARGADO, FEC_ENVIO, FEC_ENTREGA,FEC_INGRESO_TRIBUNAL, COMENTARIOS, USRID_REGISTRO)
		VALUES(@panelId, @userEncargado, @fecEnvio, @fecEntrega, @fecIngreso,@comentarios, @user)
	END	
	else
	BEGIN
		if @panelIdExist> 0 and @ingresarFechaEntrega = 'S'
		begin
			UPDATE PANEL_DEMANDA_DETALLE
			SET FEC_ENTREGA = @fecEntrega, COMENTARIOS = @comentarios
			WHERE PANEL_ID = @panelId

			SET @panelCorrecion = (SELECT IsNull(Max(IDCORRECCION)+1, 1)
									FROM PANEL_DEMANDA_CORRECCION_HISTORIAL)
			INSERT INTO PANEL_DEMANDA_CORRECCION_HISTORIAL(PANEL_ID, IDCORRECCION, FEC_ENTREGA, USRID_REGISTRO)
			VALUES(@panelId, @panelCorrecion, @fecEntrega, @user)
		end
		else
		begin
			if @panelIdExist> 0 and @ingresarFechaEntrega = 'N'
			begin 
				UPDATE PANEL_DEMANDA_DETALLE
				SET USRID_ENCARGADO =  @userEncargado, FEC_INGRESO_TRIBUNAL = @fecIngreso, COMENTARIOS = @comentarios
				WHERE PANEL_ID = @panelId
			end
		end
	END
END