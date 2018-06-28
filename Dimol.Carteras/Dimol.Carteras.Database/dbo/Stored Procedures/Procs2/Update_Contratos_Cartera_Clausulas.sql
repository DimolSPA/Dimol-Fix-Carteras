

Create Procedure Update_Contratos_Cartera_Clausulas(@ccl_codemp integer, @ccl_cctid integer, @ccl_clcid integer) as  
  UPDATE contratos_cartera_clausulas  
     SET ccl_codemp = @ccl_codemp,   
         ccl_cctid = @ccl_cctid,   
         ccl_clcid = @ccl_clcid  
   WHERE ( contratos_cartera_clausulas.ccl_codemp = @ccl_codemp ) AND  
         ( contratos_cartera_clausulas.ccl_cctid = @ccl_cctid ) AND  
         ( contratos_cartera_clausulas.ccl_clcid = @ccl_clcid )
