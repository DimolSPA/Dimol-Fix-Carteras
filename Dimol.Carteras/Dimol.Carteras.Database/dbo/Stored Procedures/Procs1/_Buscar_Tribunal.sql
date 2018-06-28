-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Buscar_Tribunal] (@codemp int,  @rut varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	Select  tribunales.trb_trbid as trbid
      FROM tribunales
    WHERE  tribunales.trb_codemp = @codemp
    and tribunales.trb_rut = @rut
	
END
