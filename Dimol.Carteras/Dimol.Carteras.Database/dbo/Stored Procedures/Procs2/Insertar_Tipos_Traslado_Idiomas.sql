

Create Procedure Insertar_Tipos_Traslado_Idiomas(@tli_codemp integer, @tli_ttlid integer, @tli_idid integer, @tli_nombre varchar(80)) as
    INSERT INTO tipos_traslado_idiomas  
         ( tli_codemp,   
           tli_ttlid,   
           tli_idid,   
           tli_nombre )  
  VALUES ( @tli_codemp,   
           @tli_ttlid,   
           @tli_idid,   
           @tli_nombre )
