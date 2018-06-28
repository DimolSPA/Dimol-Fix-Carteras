CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Detalle_Fotos](
@solicitudId int,
@idvisitadetalle int,
@rutaImageGeo varchar(1000),
@pathImage varchar(1000))
AS
BEGIN
declare 
	@visitaId int = 0, @idfoto int = 0;

	select @visitaId = id_visita from VISITA_TERRENO where solicitud_id = @solicitudId
---Ingresar fotos
	SET @idfoto = (SELECT IsNull(Max(ID_FOTO)+1, 1)
						FROM VISITA_TERRENO_DETALLE_FOTOS
						WHERE ID_VISITA = @visitaId 
						and ID_VISITA_DETALLE = @idvisitadetalle)
	INSERT INTO [dbo].[VISITA_TERRENO_DETALLE_FOTOS]
           ([ID_VISITA_DETALLE]
           ,[ID_VISITA]
           ,[ID_FOTO]
           ,[RUTA_IMAGEN]
           ,[PATH_IMAGEN])
     VALUES
           (@idvisitadetalle
           ,@visitaId
           ,@idfoto
           ,@rutaImageGeo
           ,@pathImage)
	select @idfoto IDFOTO
END
