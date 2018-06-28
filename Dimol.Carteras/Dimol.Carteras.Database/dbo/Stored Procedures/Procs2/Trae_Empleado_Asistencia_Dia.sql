

Create Procedure Trae_Empleado_Asistencia_Dia(@codemp integer, @codsuc integer, @empleado integer) as
  SELECT empleado_asistencia.epa_dia  
    FROM empleado_asistencia  
   WHERE ( empleado_asistencia.epa_codemp = @codemp ) AND  
         ( empleado_asistencia.epa_sucid = @codsuc ) AND  
         ( empleado_asistencia.epa_emplid = @empleado ) AND  
         ( datepart(year,empleado_asistencia.epa_dia) = datepart(year, getdate()) ) AND  
         ( datepart(month,empleado_asistencia.epa_dia) = datepart(month, getdate()) ) AND  
         ( datepart(day,empleado_asistencia.epa_dia) = datepart(day, getdate()) )
