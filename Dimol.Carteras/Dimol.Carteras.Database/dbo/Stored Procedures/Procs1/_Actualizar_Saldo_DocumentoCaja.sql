CREATE PROCEDURE [dbo].[_Actualizar_Saldo_DocumentoCaja](
@remesaId int,
@documentoid int,
@userId int)
AS
BEGIN
 declare @debitado decimal(15,2);
	set @debitado = (select ISNULL(sum(DEBITADO), 0) from REMESA_ANTICIPO where REMESA_ID = @remesaId)

	update CAJA_RECEPCION_DOCUMENTOS
	set saldo = saldo  - @debitado
	where DOCUMENTO_ID = @documentoid

END
