

CREATE PROCEDURE [dbo].[_Listar_Nombre_Deudor_PJ] (@texto varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	   SELECT TOP 1 REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(NOMBRE)), char(9), ''), char(10), ''), char(13), '') NOMBRE 
	   FROM [10.0.1.11].PoderJudicial.dbo.PODER_JUDICIAL_LITIGANTE J  with(nolock)
	   WHERE J.RUT = @texto
END 
