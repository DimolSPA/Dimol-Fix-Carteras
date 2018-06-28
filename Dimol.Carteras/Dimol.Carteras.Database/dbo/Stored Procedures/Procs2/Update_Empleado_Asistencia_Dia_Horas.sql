

Create Procedure Update_Empleado_Asistencia_Dia_Horas(@codemp integer, @codsuc integer, @empleado integer, @dia datetime, @item integer) as
  UPDATE empleado_asistencia_horas  
     SET eah_salida = getdate()  
   WHERE ( empleado_asistencia_horas.eah_codemp = @codemp ) AND  
         ( empleado_asistencia_horas.eah_sucid = @codsuc ) AND  
         ( empleado_asistencia_horas.eah_emplid = @empleado ) AND  
         ( datepart(year,empleado_asistencia_horas.eah_dia) = datepart(year,@dia) ) AND 
         ( datepart(month,empleado_asistencia_horas.eah_dia) = datepart(month,@dia) ) AND 
         ( datepart(day,empleado_asistencia_horas.eah_dia) = datepart(day,@dia) ) AND 
         ( empleado_asistencia_horas.eah_item = @item )
