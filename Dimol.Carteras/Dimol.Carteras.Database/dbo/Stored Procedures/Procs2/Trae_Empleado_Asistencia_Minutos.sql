

Create Procedure Trae_Empleado_Asistencia_Minutos(@codemp integer, @sucid integer, @empleado integer, @dia datetime, @item smallint) as
  SELECT datediff("minute", eah_entrada, getdate()) 
    FROM empleado_asistencia_horas  
   WHERE ( empleado_asistencia_horas.eah_codemp = @codemp ) AND  
         ( empleado_asistencia_horas.eah_sucid = @sucid ) AND  
         ( empleado_asistencia_horas.eah_emplid = @empleado ) AND  
         ( datepart(year,empleado_asistencia_horas.eah_dia) = datepart(year,@dia) ) AND  
         ( datepart(month,empleado_asistencia_horas.eah_dia) = datepart(month,@dia) ) AND  
         ( datepart(day,empleado_asistencia_horas.eah_dia) = datepart(day,@dia) ) AND  
         ( empleado_asistencia_horas.eah_item = @item )
