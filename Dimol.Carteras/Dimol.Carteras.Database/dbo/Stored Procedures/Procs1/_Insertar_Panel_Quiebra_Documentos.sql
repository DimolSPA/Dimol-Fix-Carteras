CREATE PROCEDURE [dbo].[_Insertar_Panel_Quiebra_Documentos](
@panelId int,
@codemp int,
@pclid numeric(15,0),
@ctcid numeric(15,0),
@ccbid int,
@user int)

AS
BEGIN
	declare @exist int
	SET @exist =(select count(ccbid) as ccbid from panel_quiebra_documentos where quiebra_id = @panelId and codemp = @codemp
				and pclid = @pclid and ctcid = @ctcid and ccbid = @ccbid)
	IF @exist = 0
	BEGIN
		-- INSERTAR DATOS DE DOCUMENTOS
		INSERT INTO PANEL_QUIEBRA_DOCUMENTOS(
		QUIEBRA_ID,
		CODEMP,
		PCLID,
		CTCID,
		CCBID,
		USRID_REGISTRO)
		VALUES(@panelId, @codemp, @pclid, @ctcid, @ccbid, @user)
		select @panelId panelId
	END
	ELSE
	BEGIN
		select @panelId panelId
	END
END
