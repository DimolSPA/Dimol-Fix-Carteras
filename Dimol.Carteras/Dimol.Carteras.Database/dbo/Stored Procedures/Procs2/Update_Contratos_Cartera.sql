

Create Procedure Update_Contratos_Cartera(@cct_codemp integer, @cct_cctid integer, @cct_nombre varchar (200), @cct_tipo smallint) as  
UPDATE contratos_cartera  
     SET cct_codemp = @cct_codemp,   
         cct_cctid = @cct_cctid,   
         cct_nombre = @cct_nombre,   
         cct_tipo = @cct_tipo  
   WHERE ( contratos_cartera.cct_codemp = @cct_codemp ) AND  
         ( contratos_cartera.cct_cctid = @cct_cctid )
