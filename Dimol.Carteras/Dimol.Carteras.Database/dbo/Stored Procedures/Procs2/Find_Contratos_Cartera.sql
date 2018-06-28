

Create Procedure Find_Contratos_Cartera(@cct_codemp integer, @cct_cctid integer) as
  SELECT count(contratos_cartera.cct_cctid)  
    FROM contratos_cartera  
   WHERE ( contratos_cartera.cct_codemp = @cct_codemp ) AND  
         ( contratos_cartera.cct_cctid = @cct_cctid )
