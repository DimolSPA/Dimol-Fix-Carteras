

Create Procedure Delete_Tipos_Transporte_Idiomas(@tti_codemp integer, @tti_tptid integer, @tti_idid integer) as
  DELETE FROM tipos_transporte_idiomas  
   WHERE ( tipos_transporte_idiomas.tti_codemp = @tti_codemp ) AND  
         ( tipos_transporte_idiomas.tti_tptid = @tti_tptid ) AND  
         ( tipos_transporte_idiomas.tti_idid = @tti_idid )
