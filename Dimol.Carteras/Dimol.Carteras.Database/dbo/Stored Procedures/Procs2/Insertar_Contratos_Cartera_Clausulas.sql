

Create Procedure Insertar_Contratos_Cartera_Clausulas(@ccl_codemp integer, @ccl_cctid integer, @ccl_clcid integer) as
  INSERT INTO contratos_cartera_clausulas  
         ( ccl_codemp,   
           ccl_cctid,   
           ccl_clcid )  
  VALUES ( @ccl_codemp,   
           @ccl_cctid,   
           @ccl_clcid )
