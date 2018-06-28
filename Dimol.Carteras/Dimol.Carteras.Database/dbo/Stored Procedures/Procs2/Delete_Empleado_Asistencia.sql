

Create Procedure Delete_Empleado_Asistencia(@epa_codemp integer, @epa_sucid integer, @epa_emplid integer, @epa_dia datetime) as 
  DELETE FROM empleado_asistencia  
   WHERE ( empleado_asistencia.epa_codemp = @epa_codemp ) AND  
         ( empleado_asistencia.epa_sucid = @epa_sucid ) AND  
         ( empleado_asistencia.epa_emplid = @epa_emplid ) AND  
         ( empleado_asistencia.epa_dia = @epa_dia )
