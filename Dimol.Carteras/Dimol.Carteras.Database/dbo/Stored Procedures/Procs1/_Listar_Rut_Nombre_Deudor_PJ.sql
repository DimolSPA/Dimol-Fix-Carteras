
create PROCEDURE [dbo].[_Listar_Rut_Nombre_Deudor_PJ] (@texto varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%' 
declare @rut varchar(20) = @texto + '%' 
	
	   select LTRIM(RTRIM(d.RUT)) + ' - ' + (SELECT TOP 1 REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(NOMBRE)), char(9), ''), char(10), ''), char(13), '') FROM PoderJudicial.dbo.PODER_JUDICIAL_LITIGANTE J  with (nolock) WHERE J.RUT = d.RUT) --+ REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(NOMBRE)), char(9), ''), char(10), ''), char(13), '')
	   , 1 ID_CAUSA --d.ID_CAUSA 
	   from [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_LITIGANTE] d with (nolock)
	   where LTRIM(RTRIM(convert(char,d.RUT))) like @rut
	   --or d.NOMBRE like @nombre
	   group by d.RUT  
END 

