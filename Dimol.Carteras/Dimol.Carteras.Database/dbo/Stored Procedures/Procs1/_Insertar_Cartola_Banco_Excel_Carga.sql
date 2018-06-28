CREATE PROCEDURE [dbo].[_Insertar_Cartola_Banco_Excel_Carga](
@archivo varchar(100),
@userId int)
AS
BEGIN
declare @idCarga int = 0
	set @idCarga = (SELECT IsNull(Max(IDCARGA)+1, 1)
						FROM CARTOLA_BANCO_EXCEL_CARGA)
	INSERT INTO CARTOLA_BANCO_EXCEL_CARGA(IDCARGA,ARCHIVO,USRID_DESCARGA)
	VALUES(@idCarga,@archivo, @userId)

	select @idCarga IDCARGA
END
