

Create Procedure Delete_Tipos_Causa_Guias(@tgd_codemp integer, @tgd_tgdid integer) as
   
   DELETE FROM tipos_causa_guias_idiomas  
   WHERE ( tipos_causa_guias_idiomas.tgi_codemp = @tgd_codemp ) AND  
         ( tipos_causa_guias_idiomas.tgi_tgdid = @tgd_tgdid )   


  DELETE FROM tipos_causa_guias  
   WHERE ( tipos_causa_guias.tgd_codemp = @tgd_codemp ) AND  
         ( tipos_causa_guias.tgd_tgdid = @tgd_tgdid )
