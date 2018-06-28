

Create Procedure Trae_Empleado_Asistencia_Item_Salida(@codemp integer, @sucid integer, @empleado integer, @dia datetime) as
  SELECT empleado_asistencia_horas.eah_item  
    FROM empleado_asistencia_horas  
   WHERE  empleado_asistencia_horas.eah_codemp = @codemp  AND  
          empleado_asistencia_horas.eah_sucid = @sucid  AND  
          empleado_asistencia_horas.eah_emplid = @empleado  AND  
          datepart(year,empleado_asistencia_horas.eah_dia) = datepart(year,@dia)  AND  
          datepart(month,empleado_asistencia_horas.eah_dia) = datepart(month,@dia)  AND  
          datepart(day,empleado_asistencia_horas.eah_dia) = datepart(day,@dia)  AND  
          empleado_asistencia_horas.eah_entrada = empleado_asistencia_horas.eah_salida
