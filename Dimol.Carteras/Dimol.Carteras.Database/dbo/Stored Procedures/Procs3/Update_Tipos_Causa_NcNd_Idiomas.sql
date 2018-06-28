

Create Procedure Update_Tipos_Causa_NcNd_Idiomas(@tni_codemp integer, @tni_tntid integer, @tni_idid integer, @tni_nombre varchar(100)) as

  UPDATE tipos_causa_ncnd_idiomas  
     SET tni_nombre = @tni_nombre  
   WHERE ( tipos_causa_ncnd_idiomas.tni_codemp = @tni_codemp ) AND  
         ( tipos_causa_ncnd_idiomas.tni_tntid = @tni_tntid ) AND  
         ( tipos_causa_ncnd_idiomas.tni_idid = @tni_idid )
