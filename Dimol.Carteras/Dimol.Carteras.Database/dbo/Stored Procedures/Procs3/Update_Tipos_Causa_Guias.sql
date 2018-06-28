

Create Procedure Update_Tipos_Causa_Guias(@tgd_codemp integer, @tgd_tgdid integer, @tgd_nombre varchar(80), @tgd_codigo varchar(5)) as
   UPDATE tipos_causa_guias  
     SET tgd_nombre = @tgd_nombre,   
         tgd_codigo = @tgd_codigo  
   WHERE ( tipos_causa_guias.tgd_codemp = @tgd_codemp ) AND  
         ( tipos_causa_guias.tgd_tgdid = @tgd_tgdid )
