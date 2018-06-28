

Create Procedure Update_Clausulas_ContCart_Idiomas(@cli_codemp integer, @cli_clcid integer, @cli_idid integer, @cli_nombre varchar (200)) as  
  UPDATE clausulas_contcart_idiomas  
     SET cli_codemp = @cli_codemp,   
         cli_clcid = @cli_clcid,   
         cli_idid = @cli_idid,   
         cli_nombre = @cli_nombre 
   WHERE ( clausulas_contcart_idiomas.cli_codemp = @cli_codemp ) AND  
         ( clausulas_contcart_idiomas.cli_clcid = @cli_clcid ) AND  
         ( clausulas_contcart_idiomas.cli_idid = @cli_idid )
