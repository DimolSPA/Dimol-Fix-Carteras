

Create Procedure Update_Tipos_Traslado_Idiomas(@tli_codemp integer, @tli_ttlid integer, @tli_idid integer, @tli_nombre varchar(80)) as
    UPDATE tipos_traslado_idiomas  
     SET tli_nombre = @tli_nombre  
   WHERE ( tipos_traslado_idiomas.tli_codemp = @tli_codemp ) AND  
         ( tipos_traslado_idiomas.tli_ttlid = @tli_ttlid ) AND  
         ( tipos_traslado_idiomas.tli_idid = @tli_idid )
