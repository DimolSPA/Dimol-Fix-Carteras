

Create Procedure Insertar_Tipos_Tribunal_Idiomas(@tbi_codemp integer, @tbi_ttbid integer, @tbi_idid integer, @tbi_nombre varchar (70)) as  
  INSERT INTO tipos_tribunal_idiomas  
         ( tbi_codemp,   
           tbi_ttbid,   
           tbi_idid,   
           tbi_nombre )  
  VALUES ( @tbi_codemp,   
           @tbi_ttbid,   
           @tbi_idid,   
           @tbi_nombre )
