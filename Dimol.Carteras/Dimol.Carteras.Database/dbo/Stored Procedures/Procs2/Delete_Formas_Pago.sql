

Create Procedure Delete_Formas_Pago(@frp_codemp integer, @frp_frpid integer) as
   DELETE FROM formas_pago_idiomas  
   WHERE ( formas_pago_idiomas.fpi_codemp = @frp_codemp ) AND  
         ( formas_pago_idiomas.fpi_frpid = @frp_frpid )   
           

  DELETE FROM formas_pago  
   WHERE ( formas_pago.frp_codemp =@frp_codemp ) AND  
         ( formas_pago.frp_frpid = @frp_frpid )
