

Create Procedure Update_Tipos_Causa_Guias_Idiomas(@tgi_codemp integer, @tgi_tgdid integer, @tgi_idid integer, @tgi_nombre varchar(80)) as
   
   UPDATE tipos_causa_guias_idiomas  
     SET tgi_nombre = @tgi_nombre 
   WHERE ( tipos_causa_guias_idiomas.tgi_codemp = @tgi_codemp ) AND  
         ( tipos_causa_guias_idiomas.tgi_tgdid = @tgi_tgdid ) AND  
         ( tipos_causa_guias_idiomas.tgi_idid = @tgi_idid )
