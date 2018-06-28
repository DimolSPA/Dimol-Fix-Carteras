

 Create Procedure Insertar_Empleado_Asistencia(@epa_codemp integer, @epa_sucid integer, @epa_emplid integer, @epa_dia datetime) as
  INSERT INTO empleado_asistencia  
         ( epa_codemp,   
           epa_sucid,   
           epa_emplid,   
           epa_dia )  
  VALUES ( @epa_codemp,   
           @epa_sucid,   
           @epa_emplid,   
           @epa_dia )
