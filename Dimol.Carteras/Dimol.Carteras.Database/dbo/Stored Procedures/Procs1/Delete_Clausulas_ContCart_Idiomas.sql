

Create Procedure Delete_Clausulas_ContCart_Idiomas(@cli_codemp integer, @cli_clcid integer, @cli_idid integer) as
   DELETE FROM clausulas_contcart_idiomas  
   WHERE ( clausulas_contcart_idiomas.cli_codemp = @cli_codemp ) AND  
         ( clausulas_contcart_idiomas.cli_clcid = @cli_clcid ) AND  
         ( clausulas_contcart_idiomas.cli_idid = @cli_idid )
