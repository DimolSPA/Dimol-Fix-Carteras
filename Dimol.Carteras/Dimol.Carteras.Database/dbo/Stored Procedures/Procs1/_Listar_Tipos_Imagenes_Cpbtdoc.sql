CREATE PROCEDURE [dbo].[_Listar_Tipos_Imagenes_Cpbtdoc]
(
@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
	  tpc_nombre,
	  tpc_tpcid
	FROM tipos_imagenes_cpbtdoc
	WHERE tpc_codemp = @codemp

	

END
