

Create Procedure Delete_Formas_Pago_Idiomas(@fpi_codemp integer, @fpi_frpid integer, @fpi_idi integer) as
  DELETE FROM formas_pago_idiomas  
   WHERE ( formas_pago_idiomas.fpi_codemp = @fpi_codemp ) AND  
         ( formas_pago_idiomas.fpi_frpid = @fpi_frpid ) AND  
         ( formas_pago_idiomas.fpi_idid = @fpi_idi )
