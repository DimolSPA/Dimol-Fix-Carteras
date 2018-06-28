

Create Procedure Insertar_Clausulas_ContCart_Idiomas(@cli_codemp integer, @cli_clcid integer, @cli_idid integer, @cli_nombre varchar (200)) as
  INSERT INTO clausulas_contcart_idiomas  
         ( cli_codemp,   
           cli_clcid,   
           cli_idid,   
           cli_nombre )  
  VALUES ( @cli_codemp,   
           @cli_clcid,   
           @cli_idid,   
           @cli_nombre )
