

Create Procedure Delete_Tipos_Tribunal_Idiomas(@tbi_codemp integer, @tbi_ttbid integer, @tbi_idid integer) as
  DELETE FROM tipos_tribunal_idiomas  
   WHERE ( tipos_tribunal_idiomas.tbi_codemp = @tbi_codemp ) AND  
         ( tipos_tribunal_idiomas.tbi_ttbid = @tbi_ttbid ) AND  
         ( tipos_tribunal_idiomas.tbi_idid = @tbi_idid )
