
-- =============================================
-- Author:		César León
-- Create date: 29-03-2018
-- Description:	Inserta los documentos en la tabla Panel_Demanda_Previsional
-- =============================================
CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Previsional_Documentos]
(
	@panelId int,
	@codemp int,
	@pclid numeric(15,0),
	@ctcid numeric(15,0),
	@ccbid int,
	@user int
)
AS
BEGIN
	-- INSERTAR DATOS DE DOCUMENTOS
	INSERT INTO PANEL_DEMANDA_PREVISIONAL_DOCUMENTOS(PANEL_ID, CODEMP, PCLID, CTCID, CCBID, USRID_REGISTRO)
	VALUES(@panelId, @codemp, @pclid, @ctcid, @ccbid, @user)
	
	SELECT @panelId panelId
END