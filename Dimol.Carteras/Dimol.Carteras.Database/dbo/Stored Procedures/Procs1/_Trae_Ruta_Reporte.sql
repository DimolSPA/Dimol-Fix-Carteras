-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
create PROCEDURE [dbo].[_Trae_Ruta_Reporte] 
(
	@pagina as integer,
	@reporte as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [RTI_FISICO] ruta
  FROM [REPORTES_IDIOMAS]
  where RTI_TRVID = @pagina
  and RTI_RPTID = @reporte
  and RTI_IDID = @idioma


END
