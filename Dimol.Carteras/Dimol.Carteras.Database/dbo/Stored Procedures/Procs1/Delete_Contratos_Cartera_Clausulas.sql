

Create Procedure Delete_Contratos_Cartera_Clausulas(@ccl_codemp integer, @ccl_cctid integer, @ccl_clcid integer) as
  DELETE FROM contratos_cartera_clausulas  
   WHERE ( contratos_cartera_clausulas.ccl_codemp = @ccl_codemp ) AND  
         ( contratos_cartera_clausulas.ccl_cctid = @ccl_cctid ) AND  
         ( contratos_cartera_clausulas.ccl_clcid = @ccl_clcid )
