

Create  Procedure Update_Tipos_Informes_Idiomas(@tfi_codemp integer, @tfi_tifid integer, @tfi_idid integer, @tfi_nombre varchar (100)) as  
  UPDATE tipos_informes_idiomas  
     SET tfi_nombre = @tfi_nombre 
   WHERE ( tipos_informes_idiomas.tfi_codemp = @tfi_codemp ) AND  
         ( tipos_informes_idiomas.tfi_tifid = @tfi_tifid ) AND  
         ( tipos_informes_idiomas.tfi_idid = @tfi_idid )
