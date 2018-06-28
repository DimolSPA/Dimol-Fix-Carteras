

Create Procedure Find_Tipos_Transporte(@tpt_codemp integer, @tpt_tptid integer) as
  SELECT count(tipos_transporte.tpt_tptid)  
    FROM tipos_transporte  
   WHERE ( tipos_transporte.tpt_codemp = @tpt_codemp ) AND  
         ( tipos_transporte.tpt_tptid = @tpt_tptid )
