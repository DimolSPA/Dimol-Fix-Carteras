

Create Procedure Find_Tipos_Causa_Guias(@tgd_codemp integer, @tgd_tgdid integer) as
   
  SELECT count(tipos_causa_guias.tgd_tgdid)  
    FROM tipos_causa_guias  
   WHERE ( tipos_causa_guias.tgd_codemp = @tgd_codemp ) AND  
         ( tipos_causa_guias.tgd_tgdid = @tgd_tgdid )
