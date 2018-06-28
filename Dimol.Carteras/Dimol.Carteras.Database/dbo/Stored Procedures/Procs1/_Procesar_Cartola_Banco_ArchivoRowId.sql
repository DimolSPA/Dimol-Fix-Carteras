CREATE PROCEDURE [dbo].[_Procesar_Cartola_Banco_ArchivoRowId](
@archivoRowId int)
AS
BEGIN
	update CARTOLA_BANCO_EXCEL
	set PROCESADO = 'S'
	where EXCEL_ROW_ID = @archivoRowId
END
