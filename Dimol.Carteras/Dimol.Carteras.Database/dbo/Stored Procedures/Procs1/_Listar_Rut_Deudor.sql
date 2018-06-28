-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rut_Deudor] (@rut varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @like varchar(25) = @rut + '%'
   select LTRIM(RTRIM(d.CTC_NUMERO))+LTRIM(RTRIM(d.CTC_DIGITO))
			,d.CTC_CTCID 
   from DEUDORES d 
   where convert(char,d.CTC_NUMERO)+d.CTC_DIGITO like @like
END
