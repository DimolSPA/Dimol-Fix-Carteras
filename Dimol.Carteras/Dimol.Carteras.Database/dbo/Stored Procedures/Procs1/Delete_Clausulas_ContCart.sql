

Create Procedure Delete_Clausulas_ContCart(@clc_codemp integer, @clc_clcid integer) as

     DELETE FROM clausulas_contcart_rangos  
   WHERE ( clausulas_contcart_rangos.clr_codemp = @clc_codemp ) AND  
         ( clausulas_contcart_rangos.clr_clcid = @clc_clcid ) 

   DELETE FROM clausulas_contcart_idiomas  
   WHERE ( clausulas_contcart_idiomas.cli_codemp = @clc_codemp ) AND  
         ( clausulas_contcart_idiomas.cli_clcid = @clc_clcid ) 

  DELETE FROM clausulas_contcart  
   WHERE ( clausulas_contcart.clc_codemp = @clc_codemp ) AND  
         ( clausulas_contcart.clc_clcid = @clc_clcid )
