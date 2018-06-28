

Create Procedure Find_Cartera_Clientes_Campana(@ccc_codemp integer, @ccc_cccid integer) as
  SELECT count(cartera_clientes_campana.ccc_codemp )
    FROM cartera_clientes_campana  
   WHERE ( cartera_clientes_campana.ccc_codemp = @ccc_codemp ) AND  
         ( cartera_clientes_campana.ccc_cccid = @ccc_cccid )
