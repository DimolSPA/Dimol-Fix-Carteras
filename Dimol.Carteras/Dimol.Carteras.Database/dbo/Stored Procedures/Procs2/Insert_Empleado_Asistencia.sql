

Create Procedure Insert_Empleado_Asistencia(@codemp integer, @codsuc integer, @empleado integer, @dia datetime) as
  INSERT INTO empleado_asistencia  
         ( epa_codemp,   
           epa_sucid,   
           epa_emplid,   
           epa_dia )  
  VALUES ( @codemp,   
           @codsuc,   
           @empleado,   
           @dia )
