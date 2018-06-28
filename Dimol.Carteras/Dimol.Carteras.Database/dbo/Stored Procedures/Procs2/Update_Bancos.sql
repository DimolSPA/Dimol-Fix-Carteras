

Create Procedure Update_Bancos(@bco_codemp integer, @bco_bcoid integer, @bco_rut varchar (20), @bco_nombre varchar (200), @bco_protesto text) as 
  UPDATE bancos  
     SET bco_rut = @bco_rut,   
         bco_nombre = @bco_nombre,
         bco_protesto = @bco_protesto  
   WHERE ( bancos.bco_codemp = @bco_codemp ) AND  
         ( bancos.bco_bcoid = @bco_bcoid )
