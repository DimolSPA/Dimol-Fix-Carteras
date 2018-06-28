

Create Procedure Insertar_Bancos(@bco_codemp integer, @bco_bcoid integer, @bco_rut varchar (20), @bco_nombre varchar (200), @bco_protesto text) as 
  INSERT INTO bancos  
         ( bco_codemp,   
           bco_bcoid,   
           bco_rut,   
           bco_nombre,
           bco_protesto )  
  VALUES ( @bco_codemp,   
           @bco_bcoid,   
           @bco_rut,   
           @bco_nombre,
           @bco_protesto )
