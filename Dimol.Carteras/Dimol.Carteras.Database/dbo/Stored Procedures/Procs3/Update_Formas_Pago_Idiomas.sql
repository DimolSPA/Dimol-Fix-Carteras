

Create Procedure Update_Formas_Pago_Idiomas(@fpi_codemp integer, @fpi_frpid integer, @fpi_idid integer, @fpi_nombre varchar(200) ) as
  UPDATE formas_pago_idiomas  
     SET fpi_nombre = @fpi_nombre
   WHERE ( formas_pago_idiomas.fpi_codemp = @fpi_codemp ) AND  
         ( formas_pago_idiomas.fpi_frpid = @fpi_frpid ) AND  
         ( formas_pago_idiomas.fpi_idid = @fpi_idid )
