CREATE PROCEDURE [dbo].[_Listar_Clausulas_Grilla]
(
@codemp int,
@idioma int,
@ccl_cctid int
)
AS
BEGIN
	SELECT contratos_cartera_clausulas.ccl_clcid,   
            clausulas_contcart_idiomas.cli_nombre,
			contratos_cartera_clausulas.CCL_CCTID as idCCT
            FROM contratos_cartera_clausulas,   
            clausulas_contcart_idiomas
            WHERE  contratos_cartera_clausulas.ccl_codemp = clausulas_contcart_idiomas.cli_codemp  and  
            contratos_cartera_clausulas.ccl_clcid = clausulas_contcart_idiomas.cli_clcid  and  
            contratos_cartera_clausulas.ccl_codemp = @codemp
            and contratos_cartera_clausulas.ccl_cctid = @ccl_cctid
            and clausulas_contcart_idiomas.cli_idid = @idioma
		
   END
