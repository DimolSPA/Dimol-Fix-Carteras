CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Detalle_Nuevas_Direcciones](
@solicitudId int,
@idvisitadetalle int,
@direccion varchar(50),
@comuna varchar(50),
@ciudad varchar(50),
@latitud NUMERIC(12,6),
@longitud NUMERIC(12,6))
AS
BEGIN
declare 
	@visitaId int = 0, @consec int = 0;

	select @visitaId = id_visita from VISITA_TERRENO where solicitud_id = @solicitudId

	set @consec = (SELECT IsNull(Max(ALTITUD)+1, 1)
						FROM VISITA_TERRENO_DETALLE_GPS)

	INSERT INTO [dbo].[VISITA_TERRENO_DETALLE_GPS]
           ([ID_VISITA_DETALLE]
           ,[ID_VISITA]
           ,[LATITUD]
           ,[LONGITUD]
           ,[ALTITUD]
           ,[DIRECCION]
           ,[COMUNA]
           ,[CIUDAD])
     VALUES
           (@idvisitadetalle
           ,@visitaId
           ,@latitud
           ,@longitud
           ,@consec
           ,@direccion
           ,@comuna
           ,@ciudad)	
END

