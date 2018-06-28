

Create Procedure Find_Tipos_Causa_NcNd(@tnt_codemp integer, @tnt_tntid integer) as

   SELECT count(tipos_causa_ncnd.tnt_tntid)  
    FROM tipos_causa_ncnd  
   WHERE ( tipos_causa_ncnd.tnt_codemp = @tnt_codemp ) AND  
         ( tipos_causa_ncnd.tnt_tntid = @tnt_tntid )
