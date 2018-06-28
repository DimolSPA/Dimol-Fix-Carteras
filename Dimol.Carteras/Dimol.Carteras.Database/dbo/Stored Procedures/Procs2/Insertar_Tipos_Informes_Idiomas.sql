

 Create Procedure Insertar_Tipos_Informes_Idiomas(@tfi_codemp integer, @tfi_tifid integer, @tfi_idid integer, @tfi_nombre varchar (100)) as
 INSERT INTO tipos_informes_idiomas  
         ( tfi_codemp,   
           tfi_tifid,   
           tfi_idid,   
           tfi_nombre )  
  VALUES ( @tfi_codemp,   
           @tfi_tifid,   
           @tfi_idid,   
           @tfi_nombre )
