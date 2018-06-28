

Create Procedure Delete_Tipos_Informes_Idiomas(@tfi_codemp integer, @tfi_tifid integer, @tfi_idid integer) as 
  DELETE FROM tipos_informes_idiomas  
   WHERE ( tipos_informes_idiomas.tfi_codemp = @tfi_codemp ) AND  
         ( tipos_informes_idiomas.tfi_tifid = @tfi_tifid ) AND  
         ( tipos_informes_idiomas.tfi_idid = @tfi_idid )
