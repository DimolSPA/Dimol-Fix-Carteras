

Create Procedure Insertar_Contratos_Cartera(@cct_codemp integer, @cct_cctid integer, @cct_nombre varchar (200), @cct_tipo smallint) as
  INSERT INTO contratos_cartera  
         ( cct_codemp,   
           cct_cctid,   
           cct_nombre,   
           cct_tipo )  
  VALUES ( @cct_codemp,   
           @cct_cctid,   
           @cct_nombre,   
           @cct_tipo )
