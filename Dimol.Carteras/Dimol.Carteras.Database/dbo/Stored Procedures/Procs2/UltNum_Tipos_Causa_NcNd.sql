

Create Procedure UltNum_Tipos_Causa_NcNd(@tnt_codemp integer) as

   SELECT IsNull(Max(tnt_tntid)+1, 1)
    FROM tipos_causa_ncnd  
   WHERE ( tipos_causa_ncnd.tnt_codemp = @tnt_codemp )
