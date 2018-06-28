Create Procedure Trae_Empleado_Asistencia_Item(@codemp integer, @codsuc integer, @empleado integer, @dia datetime) as
  SELECT count(empleado_asistencia_horas.eah_item)  
    FROM empleado_asistencia_horas  
   WHERE ( empleado_asistencia_horas.eah_codemp = @codemp ) AND  
         ( empleado_asistencia_horas.eah_sucid = @codsuc ) AND  
         ( empleado_asistencia_horas.eah_emplid = @empleado ) AND  
         ( datepart(year,empleado_asistencia_horas.eah_dia) = datepart(year,@dia) and datepart(month,empleado_asistencia_horas.eah_dia) = datepart(month,@dia) and  datepart(day,empleado_asistencia_horas.eah_dia) = datepart(day,@dia))
