CREATE PROCEDURE [dbo].[_Listar_Clausulas_Todas]
(
@codemp int,
@idioma int
)
AS
BEGIN
	SELECT DISTINCT contratos_cartera_clausulas.ccl_clcid,
	clausulas_contcart_idiomas.cli_nombre
            FROM contratos_cartera_clausulas,   
            clausulas_contcart_idiomas
            WHERE  contratos_cartera_clausulas.ccl_codemp = clausulas_contcart_idiomas.cli_codemp  and  
            contratos_cartera_clausulas.ccl_clcid = clausulas_contcart_idiomas.cli_clcid  and  
            contratos_cartera_clausulas.ccl_codemp = @codemp
            and clausulas_contcart_idiomas.cli_idid = @idioma
		
   END
