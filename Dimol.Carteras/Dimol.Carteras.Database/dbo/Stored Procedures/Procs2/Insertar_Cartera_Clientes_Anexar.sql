

Create Procedure Insertar_Cartera_Clientes_Anexar(@cca_codemp integer, @cca_ccaid integer, @cca_pclid numeric (15), @cca_ctcid numeric (15)) as 
  INSERT INTO cartera_clientes_anexar  
         ( cca_codemp,   
           cca_ccaid,   
           cca_pclid,   
           cca_ctcid )  
  VALUES ( @cca_codemp,   
           @cca_ccaid,   
           @cca_pclid,   
           @cca_ctcid )
