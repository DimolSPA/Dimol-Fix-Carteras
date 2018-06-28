CREATE PROCEDURE [dbo].[_Insertar_Remesa_Anticipo](
@remesaId int,
@codemp int,
@pclid int,
@ctcid int,
@anticipo decimal(15,2),
@anticipodebitado decimal(15,2),
@documentoid int,
@userId int)
AS
BEGIN
declare @idRemesaAnticipo int = 0
	set @idRemesaAnticipo = (SELECT IsNull(Max(REMESA_ANTICIPO_ID)+1, 1)
						FROM REMESA_ANTICIPO)
	INSERT INTO REMESA_ANTICIPO(REMESA_ANTICIPO_ID,REMESA_ID,CODEMP,PCLID,CTCID,ANTICIPO,DEBITADO,DOCUMENTOID,USRID_REGISTRO)
	VALUES(@idRemesaAnticipo, @remesaId, @codemp,@pclid, @ctcid, @anticipo,@anticipodebitado,@documentoid, @userId)


END
