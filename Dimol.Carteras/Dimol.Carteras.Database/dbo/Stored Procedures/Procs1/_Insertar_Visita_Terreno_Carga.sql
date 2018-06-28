CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Carga](
@archivo varchar(100),
@userId int)
AS
BEGIN
declare @idCarga int = 0
	set @idCarga = (SELECT IsNull(Max(IDCARGA)+1, 1)
						FROM VISITA_TERRENO_CARGA)
	INSERT INTO VISITA_TERRENO_CARGA(IDCARGA,ARCHIVO,USRID_DESCARGA)
	VALUES(@idCarga,@archivo, @userId)

	select @idCarga IDCARGA
END
