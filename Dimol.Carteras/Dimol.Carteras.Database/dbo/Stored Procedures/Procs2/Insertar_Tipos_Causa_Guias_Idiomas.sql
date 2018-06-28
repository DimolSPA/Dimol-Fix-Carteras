

Create Procedure Insertar_Tipos_Causa_Guias_Idiomas(@tgi_codemp integer, @tgi_tgdid integer, @tgi_idid integer, @tgi_nombre varchar(80)) as
   
  INSERT INTO tipos_causa_guias_idiomas  
         ( tgi_codemp,   
           tgi_tgdid,   
           tgi_idid,   
           tgi_nombre )  
  VALUES ( @tgi_codemp,   
           @tgi_tgdid,   
           @tgi_idid,   
           @tgi_nombre )
