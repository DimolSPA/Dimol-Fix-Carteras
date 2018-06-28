CREATE PROCEDURE [dbo].[_Insertar_Panel_Traspasos_Avenimiento](
@codemp int,
@rolId int,
@rolNumero varchar(20),
@pclid numeric(15,0),
@ctcid numeric(15,0),
@trbId int,
@user int)
As
BEGIN
	declare @existTraspaso int

	set @existTraspaso = (select count(ROLID) from PANEL_TRASPASOS_AVENIMIENTO where CODEMP =@codemp and ROLID = @rolId)

	if @existTraspaso = 0
	begin
		INSERT INTO PANEL_TRASPASOS_AVENIMIENTO(
		CODEMP,ROLID,ROLNUMERO,PCLID,CTCID,TRBID,USRID_REGISTRO)
		VALUES(@codemp,@rolId,@rolNumero,@pclid,@ctcid,@trbId,@user)
		select @rolId rolId
	end
	else
	begin
		select @rolId rolId
	end
END
