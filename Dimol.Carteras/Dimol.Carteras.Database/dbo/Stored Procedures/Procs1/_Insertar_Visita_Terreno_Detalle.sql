CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Detalle](
@solicitudId int,
@estadoVisita int,
@visita char(1),
@comentarios varchar(1000),
@direccionActual varchar(1),
@fecEnvio datetime,
@direccionEnvio varchar(100),
@posicionEnvio varchar(100))

AS
BEGIN
	declare 
	@idvisitadetalle int = 0, 
	@ctcid int = 0, 
	@visitaId int = 0,
	@existeDetalle int = 0;
	select @ctcid = ctcid, @visitaId = id_visita from VISITA_TERRENO where solicitud_id = @solicitudId
	-- VERIFICAMOS QUE NO EXISTA DETALLE PARA LA SOLICITUD
	--set @existeDetalle = (select count(id_visita_detalle) from VISITA_TERRENO_DETALLE 
	--					where ID_VISITA = @visitaId and CTCID = @ctcid)
	-- SI NO EXISTE SE CREA
	if (@existeDetalle = 0)
	begin
	SET @idvisitadetalle =(SELECT IsNull(Max(ID_VISITA_DETALLE)+1, 1)
						FROM VISITA_TERRENO_DETALLE
						WHERE ID_VISITA = @visitaId)
	INSERT INTO [dbo].[VISITA_TERRENO_DETALLE]
           ([ID_VISITA_DETALLE]
           ,[ID_VISITA]
           ,[CTCID]
           ,[ESTADO_VISITA]
           ,[VISITA]
           ,[COMENTARIOS]
		   ,[DIRECCIONACTUAL]
		   ,[FEC_ENVIO]
		   ,[DIRECCIONENVIO]
		   ,[POSICIONENVIO])
     VALUES
           (@idvisitadetalle
           ,@visitaId
           ,@ctcid
           ,@estadoVisita
           ,@visita
           ,@comentarios
		   ,@direccionActual
		   ,@fecEnvio
		   ,@direccionEnvio
		   ,@posicionEnvio)
	end
	--else
	--begin
	---- eXISTE, SE ACTUALIZA
	--SET @idvisitadetalle =(select id_visita_detalle from VISITA_TERRENO_DETALLE 
	--						where ID_VISITA = @visitaId and CTCID = @ctcid)
	--UPDATE VISITA_TERRENO_DETALLE
	--SET ESTADO_VISITA = @estadoVisita, VISITA = @visita, COMENTARIOS = @comentarios, DIRECCIONACTUAL = @direccionActual 
	--WHERE ID_VISITA = @visitaId
	--AND ID_VISITA_DETALLE = @idvisitadetalle
	--AND CTCID = @ctcid
	--end

	select @idvisitadetalle idvisitadetalle
END
