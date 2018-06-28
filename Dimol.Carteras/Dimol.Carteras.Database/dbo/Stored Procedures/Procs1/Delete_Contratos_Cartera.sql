

Create Procedure Delete_Contratos_Cartera(@cct_codemp integer, @cct_cctid integer) as  

  DELETE FROM contratos_cartera_clausulas  
   WHERE ( contratos_cartera_clausulas.ccl_codemp = @cct_codemp ) AND  
         ( contratos_cartera_clausulas.ccl_cctid = @cct_cctid ) 


  DELETE FROM contratos_cartera  
   WHERE ( contratos_cartera.cct_codemp = @cct_codemp ) AND  
         ( contratos_cartera.cct_cctid = @cct_cctid )
