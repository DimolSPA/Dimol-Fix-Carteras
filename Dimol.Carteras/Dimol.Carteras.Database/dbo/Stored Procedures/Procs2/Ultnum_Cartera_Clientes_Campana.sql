

Create Procedure Ultnum_Cartera_Clientes_Campana(@ccc_codemp integer) as
  SELECT IsNull(Max(ccc_cccid)+1, 1)
    FROM cartera_clientes_campana  
   WHERE ( cartera_clientes_campana.ccc_codemp = @ccc_codemp )
