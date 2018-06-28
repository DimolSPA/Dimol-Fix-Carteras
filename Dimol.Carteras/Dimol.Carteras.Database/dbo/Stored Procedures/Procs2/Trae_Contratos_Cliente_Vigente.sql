

Create Procedure Trae_Contratos_Cliente_Vigente(@cct_codemp integer, @ctc_pclid integer, @cct_tipo integer) as
  SELECT contratos_clientes.ctc_cctid  
    FROM contratos_cartera,   
         contratos_clientes  
   WHERE  contratos_clientes.ctc_codemp = contratos_cartera.cct_codemp  and  
          contratos_clientes.ctc_cctid = contratos_cartera.cct_cctid  and  
           contratos_cartera.cct_codemp = @cct_codemp  AND  
          contratos_clientes.ctc_pclid = @ctc_pclid  AND  
          contratos_cartera.cct_tipo = @cct_tipo  AND  
          contratos_clientes.ctc_indefinido = 'S'      

union

  SELECT contratos_clientes.ctc_cctid  
    FROM contratos_cartera,   
         contratos_clientes  
   WHERE  contratos_clientes.ctc_codemp = contratos_cartera.cct_codemp  and  
          contratos_clientes.ctc_cctid = contratos_cartera.cct_cctid  and  
           contratos_cartera.cct_codemp = @cct_codemp  AND  
          contratos_clientes.ctc_pclid = @ctc_pclid  AND  
          contratos_cartera.cct_tipo = @cct_tipo  AND  
          contratos_clientes.ctc_indefinido = 'N'      AND
          contratos_clientes.ctc_fecfin >= getdate()
