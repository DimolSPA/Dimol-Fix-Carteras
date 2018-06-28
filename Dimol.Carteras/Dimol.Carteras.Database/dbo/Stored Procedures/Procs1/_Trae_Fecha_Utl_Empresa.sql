-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 08-04-2014
-- Description:	Trae fecha utl empresa
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Fecha_Utl_Empresa] (@codemp as int)
AS
BEGIN
	SET NOCOUNT ON;

    select emp_fecutl from empresa where emp_codemp = @codemp
END
