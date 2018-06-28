

Create Procedure Find_Tipos_Traslado(@ttl_codemp integer, @ttl_ttlid integer) as
    SELECT count(tipos_traslado.ttl_ttlid)  
    FROM tipos_traslado  
   WHERE tipos_traslado.ttl_codemp = @ttl_codemp  and  ttl_ttlid = @ttl_ttlid
