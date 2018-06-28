

Create Procedure Delete_Tipos_Traslado(@ttl_codemp integer, @ttl_ttlid integer) as
  DELETE FROM tipos_traslado_idiomas  
   WHERE ( tipos_traslado_idiomas.tli_codemp = @ttl_codemp ) AND  
         ( tipos_traslado_idiomas.tli_ttlid = @ttl_ttlid)   
           

  DELETE FROM tipos_traslado  
   WHERE ( tipos_traslado.ttl_codemp = @ttl_codemp ) AND  
         ( tipos_traslado.ttl_ttlid = @ttl_ttlid )
