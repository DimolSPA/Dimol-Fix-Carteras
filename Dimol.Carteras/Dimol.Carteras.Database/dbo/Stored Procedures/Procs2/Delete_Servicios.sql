

Create Procedure Delete_Servicios(@sve_codemp integer, @sve_sveid integer) as
    DELETE FROM servicios_idiomas  
   WHERE ( servicios_idiomas.svi_codemp = @sve_codemp ) AND  
         ( servicios_idiomas.svi_sveid = @sve_sveid )   
           

  DELETE FROM servicios  
   WHERE ( servicios.sve_codemp = @sve_codemp ) AND  
         ( servicios.sve_sveid = @sve_sveid )
