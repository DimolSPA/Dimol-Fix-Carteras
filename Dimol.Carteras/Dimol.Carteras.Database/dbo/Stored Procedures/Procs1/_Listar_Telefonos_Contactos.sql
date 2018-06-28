-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Listar_Telefonos_Contactos] (@codemp int, @ctcid int)
AS
BEGIN
	SET NOCOUNT ON;
	Select deudores_telefonos.ddt_numero Numero
	FROM deudores_telefonos
	WHERE  deudores_telefonos.ddt_codemp =  @codemp
	and deudores_telefonos.ddt_ctcid =  @ctcid
	and deudores_telefonos.ddt_estado = 'A'    

				UNION 
	Select  deudores_contactos_telefonos.dct_numero
	FROM deudores_contactos_telefonos 
	WHERE  deudores_contactos_telefonos.dct_codemp = @codemp
	and deudores_contactos_telefonos.dct_ctcid =  @ctcid
	and deudores_contactos_telefonos.dct_estado = 'A'    

	ORDER BY 1 ASC

END

