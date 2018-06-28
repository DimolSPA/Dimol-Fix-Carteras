

Create Procedure Delete_Tipos_Tribunal(@ttb_codemp integer, @ttb_ttbid integer) as
  DELETE FROM tipos_tribunal_idiomas  
   WHERE ( tipos_tribunal_idiomas.tbi_codemp = @ttb_codemp ) AND  
         ( tipos_tribunal_idiomas.tbi_ttbid = @ttb_ttbid ) 

  DELETE FROM tipos_tribunal  
   WHERE ( tipos_tribunal.ttb_codemp = @ttb_codemp ) AND  
         ( tipos_tribunal.ttb_ttbid = @ttb_ttbid )
