

Create Procedure Delete_Tipos_Transporte(@tpt_codemp integer, @tpt_tptid integer) as

  DELETE FROM tipos_transporte_idiomas  
   WHERE ( tipos_transporte_idiomas.tti_codemp = @tpt_codemp ) AND  
         ( tipos_transporte_idiomas.tti_tptid = @tpt_tptid ) 

  DELETE FROM tipos_transporte  
   WHERE ( tipos_transporte.tpt_codemp = @tpt_codemp ) AND  
         ( tipos_transporte.tpt_tptid = @tpt_tptid )
