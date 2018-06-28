-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Acciones] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
	SELECT acciones_idiomas.aci_accid ID,   
           acciones_idiomas.aci_nombre Nombre
FROM acciones_idiomas with (nolock) 
WHERE  acciones_idiomas.aci_codemp =  @codemp   AND  
acciones_idiomas.aci_idid =  @idioma
ORDER BY acciones_idiomas.aci_nombre ASC 
         


END

