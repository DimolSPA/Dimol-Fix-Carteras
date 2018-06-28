

Create Procedure UltNum_Tipos_Traslado(@ttl_codemp integer) as
    SELECT IsNull(Max(ttl_ttlid)+1, 1)
    FROM tipos_traslado  
   WHERE tipos_traslado.ttl_codemp = @ttl_codemp
