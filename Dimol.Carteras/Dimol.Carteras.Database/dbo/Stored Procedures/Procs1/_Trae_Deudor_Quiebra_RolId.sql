CREATE PROCEDURE [dbo].[_Trae_Deudor_Quiebra_RolId](
@codemp int,
@pclid numeric(15,0),
@ctcid int,
@rolNumero varchar(20))
	
AS
BEGIN

declare
	@existRol int = 0
	
	SET @existRol = (select count(ROL_ROLID) from ROL where ROL_CODEMP = @codemp 
														and ROL_PCLID = @pclid 
														and ROL_CTCID = @ctcid
														and ROL_NUMERO= @rolNumero)
	if @existRol > 0
	begin
		select ROL_ROLID  as rolId
		from ROL 
		where ROL_CODEMP = @codemp 
		and ROL_PCLID = @pclid 
		and ROL_CTCID = @ctcid
		and ROL_NUMERO= @rolNumero
		
	end
	else
	begin
		select 0 as rolId
	end
END
