CREATE PROCEDURE [dbo].[_Trae_Tribunal_Entes](
@codemp int,
@tribunalId int)
AS 
BEGIN
	Select ETJID from TRIBUNAL_ENTE where CODEMP = @codemp AND TRBID = @tribunalId
END
