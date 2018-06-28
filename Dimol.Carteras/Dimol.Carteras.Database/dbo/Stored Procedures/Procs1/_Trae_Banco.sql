-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Trae_Banco] (@codemp int, @nombre varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
	Select  bancos.bco_bcoid as bcoid FROM bancos WHERE  bancos.bco_codemp = @codemp and bancos.bco_nombre = @nombre
	
END
