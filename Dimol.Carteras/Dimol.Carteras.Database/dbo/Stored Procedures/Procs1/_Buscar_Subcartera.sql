-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Subcartera] (@codemp int, @rut varchar(10))
AS
BEGIN
	SET NOCOUNT ON;
	Select  sbc_sbcid as sbcid
             FROM subcarteras (nolock)
            WHERE  sbc_codemp = @codemp
            and sbc_rut = @rut
	
END
