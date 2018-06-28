

Create Procedure Update_Giros(@gir_codemp integer, @gir_girid integer, @gir_nombre varchar (1200)) as  
  UPDATE giros  
     SET gir_nombre = @gir_nombre  
   WHERE ( giros.gir_codemp = @gir_codemp ) AND  
         ( giros.gir_girid = @gir_girid )
