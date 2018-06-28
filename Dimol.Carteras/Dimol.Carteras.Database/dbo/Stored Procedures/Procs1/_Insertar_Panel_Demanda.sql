CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda](
@codemp int,
@pclid numeric(15,0),
@ctcid numeric(15,0),
@sbcid int,
@tpcid int,
@user int)
	
AS
BEGIN

declare
	@panelId int = 0
	
	-- INSERTAR DATOS EN VISITA TERRENO
	SET @panelId = (SELECT IsNull(Max(PANEL_ID)+1, 1)
						FROM PANEL_DEMANDA)
						--WHERE PCLID = @pclid
						--AND CTCID = @ctcid
						--AND SBCID = ISNULL(@sbcid,SBCID)
						--AND TPCID = @tpcid)
		
	INSERT INTO PANEL_DEMANDA(PANEL_ID, CODEMP, PCLID, CTCID, SBCID, TPCID, USRID_REGISTRO)
	VALUES(@panelId, @codemp, @pclid, @ctcid, @sbcid,@tpcid, @user)
	select @panelId panelId
END
