CREATE procedure [dbo].[_Insertar_Panel_Demanda_Masiva] (
@codemp int,
@pclid numeric(15,0),
@ctcid numeric(15,0),
@sbcid int,
@trbid int,
@tpcid int,
@user int,
@ccbid int,
@fecdem datetime, 
@cuodem smallint, 
@mondem numeric(15,2),
@monpcuodem numeric(15,2), 
@monucoudem numeric(15,2), 
@fecpcoudem datetime,
@fecucoudem datetime, 
@intdem numeric(5,3), 
@fecave datetime, 
@cuoave smallint, 
@monave numeric(15,2), 
@monpcouave numeric(15,2),
@monucouave numeric(15,2), 
@fecpcouave datetime,
@fecucouave datetime, 
@intave numeric(5,3)) 
as

begin
declare
	@panelId int = 0

	SET @panelId = (SELECT IsNull(Max(ID_PANEL_MASIVO)+1, 1) FROM PANEL_DEMANDA_MASIVA)
	
	INSERT INTO PANEL_DEMANDA_MASIVA(ID_PANEL_MASIVO, CODEMP, PCLID, CTCID, SBCID, TRBID, TPCID, USRID, FECDEM, CUODEM, MONDEM, MONUCOUDEM, FECPCOUDEM, FECUCOUDEM, INTDEM, FECAVE, CUOAVE, MONAVE, MONUCOUAVE, FECPCOUAVE, FECUCOUAVE, INTAVE, MONPCUODEM, MONPCOUAVE)
	VALUES(@panelId, @codemp, @pclid, @ctcid, @sbcid, @trbid, @tpcid, @user, @fecdem, @cuodem, @mondem, @monucoudem, @fecpcoudem, @fecucoudem, @intdem, @fecave, @cuoave, @monave, @monucouave, @fecpcouave, @fecucouave, @intave, @monpcuodem, @monpcouave)
	
	INSERT INTO PANEL_DEMANDA_MASIVA_DOCUMENTOS(ID_PANEL_MASIVO, CODEMP, PCLID, CTCID, CCBID, USRID)
	VALUES(@panelId, @codemp, @pclid, @ctcid, @ccbid, @user)

	select @panelId panelId

end
