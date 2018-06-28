

Create Procedure Insertar_Contratos_Cartera_Clon(@cct_codemp integer, @cct_cctid integer, @cct_cctidnew integer, @cct_nombre varchar (200)) as
insert into contratos_cartera
  SELECT contratos_cartera.cct_codemp,   
         @cct_cctidnew,   
         @cct_nombre,   
         contratos_cartera.cct_tipo  
    FROM contratos_cartera  
   WHERE ( contratos_cartera.cct_codemp = @cct_codemp ) AND  
         ( contratos_cartera.cct_cctid = @cct_cctid )   
           
insert into contratos_cartera_clausulas
  SELECT contratos_cartera_clausulas.ccl_codemp,   
         @cct_cctidnew,   
         contratos_cartera_clausulas.ccl_clcid  
    FROM contratos_cartera_clausulas  
   WHERE ( contratos_cartera_clausulas.ccl_codemp = @cct_codemp ) AND  
         ( contratos_cartera_clausulas.ccl_cctid = @cct_cctid )
