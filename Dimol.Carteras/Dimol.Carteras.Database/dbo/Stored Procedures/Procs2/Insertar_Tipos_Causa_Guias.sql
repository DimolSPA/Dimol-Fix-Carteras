

Create Procedure Insertar_Tipos_Causa_Guias(@tgd_codemp integer, @tgd_tgdid integer, @tgd_nombre varchar(80), @tgd_codigo varchar(5)) as
  INSERT INTO tipos_causa_guias  
         ( tgd_codemp,   
           tgd_tgdid,   
           tgd_nombre,   
           tgd_codigo )  
  VALUES ( @tgd_codemp,   
           @tgd_tgdid,   
           @tgd_nombre,   
           @tgd_codigo )
