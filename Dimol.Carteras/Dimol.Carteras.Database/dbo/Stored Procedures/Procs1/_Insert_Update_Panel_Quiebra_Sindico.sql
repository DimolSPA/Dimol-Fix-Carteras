CREATE PROCEDURE [dbo].[_Insert_Update_Panel_Quiebra_Sindico](
@codemp integer, 
@rolId integer, 
@sindico varchar (100), 
@veedor varchar (100), 
@interventor varchar (100),
@user integer)
AS
BEGIN
	declare @existe int = 0
		select @existe = count(ROLID)
		from PANEL_QUIEBRA_SINDICO
		where ROLID  = @rolId

	if(@existe = 0 )
	begin  
		
		INSERT INTO PANEL_QUIEBRA_SINDICO  
			 ( CODEMP,   
			   ROLID,   
			   SINDICO,   
			   VEEDOR, 
			   INTERVENTOR,  
			   USRID_REGISTRO)  
		VALUES (@codemp,   
			   @rolId,   
			   @sindico,   
			   @veedor, 
			   @interventor,  
			   @user)


	end
	else
	begin
		UPDATE PANEL_QUIEBRA_SINDICO  
		 SET SINDICO = @sindico,   
			 VEEDOR = @veedor,
			 INTERVENTOR = @interventor
	   WHERE ROLID  = @rolId
	end
END
