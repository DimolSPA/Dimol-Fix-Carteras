-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 14-04-2014
-- Description:	Trae menu encriptado
-- =============================================
create PROCEDURE [dbo].[_Trae_Menu_Encriptado] (@codemp as int) 
AS
BEGIN
	SET NOCOUNT ON;
	select emp_menu, emp_nombre from empresa where emp_codemp=@codemp
	

END
