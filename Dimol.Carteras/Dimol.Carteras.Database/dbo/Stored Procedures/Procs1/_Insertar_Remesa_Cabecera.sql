CREATE PROCEDURE [dbo].[_Insertar_Remesa_Cabecera](
@userId int)
AS
BEGIN
declare @idRemesa int = 0
	set @idRemesa = (SELECT IsNull(Max(REMESA_ID)+1, 1)
						FROM REMESA)
	INSERT INTO REMESA(REMESA_ID,USRID_REGISTRO)
	VALUES(@idRemesa, @userId)

	select @idRemesa IDREMESA

END
