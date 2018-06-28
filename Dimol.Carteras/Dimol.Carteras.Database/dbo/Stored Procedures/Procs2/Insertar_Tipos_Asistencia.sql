

Create Procedure Insertar_Tipos_Asistencia(@tia_codemp integer, @tia_tipoid integer, @tia_nombre varchar(50), @tia_tipo smallint) as
  INSERT INTO tipos_asistencia  
         ( tia_codemp,   
           tia_tipoid,   
           tia_nombre,   
           tia_tipo )  
  VALUES ( @tia_codemp,   
           @tia_tipoid,   
           @tia_nombre,   
           @tia_tipo )
