

Create Procedure Insertar_Tipos_Causa_NcNd_Idiomas(@tni_codemp integer, @tni_tntid integer, @tni_idid integer, @tni_nombre varchar(100)) as

  INSERT INTO tipos_causa_ncnd_idiomas  
         ( tni_codemp,   
           tni_tntid,   
           tni_idid,   
           tni_nombre )  
  VALUES ( @tni_codemp,   
           @tni_tntid,   
           @tni_idid,   
           @tni_nombre )
