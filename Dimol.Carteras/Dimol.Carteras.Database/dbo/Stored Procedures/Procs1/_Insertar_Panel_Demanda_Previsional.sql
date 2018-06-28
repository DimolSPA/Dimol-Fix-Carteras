
-- =============================================
-- Author:		César León
-- Create date: 29-03-2018
-- Description:	Inserta los documentos en la tabla Panel_Demanda_Previsional
-- =============================================
CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_Previsional]
(
	@codemp int,
	@pclid numeric(15,0),
	@ctcid numeric(15,0),
	@sbcid int,
	@tpcid int,
	@user int
)	
AS
BEGIN
	DECLARE @panelId int = 0
	
	SET @panelId = (SELECT IsNull(Max(PANEL_ID)+1, 1) FROM PANEL_DEMANDA_PREVISIONAL)
		
	INSERT INTO PANEL_DEMANDA_PREVISIONAL(PANEL_ID, CODEMP, PCLID, CTCID, SBCID, TPCID, USRID_REGISTRO)
	VALUES(@panelId, @codemp, @pclid, @ctcid, @sbcid,@tpcid, @user)
	
	SELECT @panelId panelId
END