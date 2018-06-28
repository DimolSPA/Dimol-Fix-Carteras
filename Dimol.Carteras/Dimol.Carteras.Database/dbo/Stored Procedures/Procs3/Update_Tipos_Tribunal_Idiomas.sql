

Create Procedure Update_Tipos_Tribunal_Idiomas(@tbi_codemp integer, @tbi_ttbid integer, @tbi_idid integer, @tbi_nombre varchar (70)) as  
  UPDATE tipos_tribunal_idiomas  
     SET tbi_codemp = @tbi_codemp,   
         tbi_ttbid = @tbi_ttbid,   
         tbi_idid = @tbi_idid,   
         tbi_nombre = @tbi_nombre  
   WHERE ( tipos_tribunal_idiomas.tbi_codemp = @tbi_codemp ) AND  
         ( tipos_tribunal_idiomas.tbi_ttbid = @tbi_ttbid ) AND  
         ( tipos_tribunal_idiomas.tbi_idid = @tbi_idid )
