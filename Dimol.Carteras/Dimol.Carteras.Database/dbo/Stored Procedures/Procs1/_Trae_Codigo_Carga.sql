-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Codigo_Carga] (@codemp int, @pclid int, @codigo_carga varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
	Select  pcc_codid  FROM provcli_codigo_carga
	WHERE  pcc_codemp = @codemp
	and  pcc_pclid = @pclid
    and  pcc_codigo = @codigo_carga
	
END
