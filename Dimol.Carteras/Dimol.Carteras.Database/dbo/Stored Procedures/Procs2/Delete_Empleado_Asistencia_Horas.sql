

Create Procedure Delete_Empleado_Asistencia_Horas(@eah_codemp integer, @eah_sucid integer, @eah_emplid integer,
                                                                                         @eah_dia datetime, @eah_item smallint) as 
  DELETE FROM empleado_asistencia_horas  
   WHERE ( empleado_asistencia_horas.eah_codemp = @eah_codemp ) AND  
         ( empleado_asistencia_horas.eah_sucid = @eah_sucid ) AND  
         ( empleado_asistencia_horas.eah_emplid = @eah_emplid ) AND  
         ( empleado_asistencia_horas.eah_dia = @eah_dia ) AND  
         ( empleado_asistencia_horas.eah_item = @eah_item )
