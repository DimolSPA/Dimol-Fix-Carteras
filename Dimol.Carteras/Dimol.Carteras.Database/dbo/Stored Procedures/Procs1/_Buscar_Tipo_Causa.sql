-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Buscar_Tipo_Causa] (@codemp int,  @tipo_causa varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
	 Select  tipos_causa.tca_tcaid as tcaid
     FROM tipos_causa
     WHERE  tca_codemp = @codemp
     and tca_nombre = @tipo_causa
	
	
END
