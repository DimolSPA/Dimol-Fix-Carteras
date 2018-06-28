-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Revisa_Estados] (@codemp int, @estid int)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT estados_cartera.ect_utiliza Utiliza,   
	estados_cartera.ect_prejud Prejudicial,   
	estados_cartera.ect_solfecha SolicitaFecha,   
	estados_cartera.ect_genret GeneraRetiro,   
	estados_cartera.ect_compromiso Compromiso, 
	ect_agrupa Agrupa
	FROM estados_cartera
	WHERE  estados_cartera.ect_codemp =  @codemp
	and estados_cartera.ect_estid = @estid
	
END

