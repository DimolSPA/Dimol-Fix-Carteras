

Create Procedure Delete_Clausulas_ContCart_Rangos( @clr_codemp integer, @clr_clcid integer, @clr_clrid integer) as
     DELETE FROM clausulas_contcart_rangos  
   WHERE ( clausulas_contcart_rangos.clr_codemp = @clr_codemp ) AND  
         ( clausulas_contcart_rangos.clr_clcid = @clr_clcid ) AND  
         ( clausulas_contcart_rangos.clr_clrid = @clr_clrid )
