

Create Procedure Delete_Cartera_Clientes_Campana(@ccc_codemp integer, @ccc_sucid integer, @ccc_cccid integer) as
  DELETE FROM cartera_clientes_campana  
   WHERE ( cartera_clientes_campana.ccc_codemp = @ccc_codemp ) AND  
         ( cartera_clientes_campana.ccc_sucid = @ccc_sucid ) AND  
         ( cartera_clientes_campana.ccc_cccid = @ccc_cccid )
