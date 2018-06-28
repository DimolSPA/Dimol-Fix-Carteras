CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Documentos](
@panelId int,
@codemp int,
@pclid numeric(15,0),
@ctcid numeric(15,0),
@ccbid int,
@user int)

AS
BEGIN
	
	-- INSERTAR DATOS DE DOCUMENTOS
	
	INSERT INTO PANEL_DEMANDA_DOCUMENTOS(PANEL_ID, CODEMP, PCLID, CTCID, CCBID, USRID_REGISTRO)
	VALUES(@panelId, @codemp, @pclid, @ctcid, @ccbid, @user)
	select @panelId panelId
END
