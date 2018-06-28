

Create Procedure Report_Empleado_Asistencia_x_Empleado(@codemp integer, @empleado integer, @anio integer, @mes integer) as
 SELECT empleados.epl_rut Rut,   
         epa_dia dia,   
         min(empleado_asistencia_horas.eah_entrada) as Entrada,   
         max(empleado_asistencia_horas.eah_salida) as Salida
         into #Asistencia  
    FROM empleados,   
         empleado_asistencia,   
         empleado_asistencia_horas,   
         estados_empleado  
   WHERE ( empleado_asistencia.epa_codemp = empleados.epl_codemp ) and  
         ( empleado_asistencia.epa_emplid = empleados.epl_emplid ) and  
         ( empleado_asistencia_horas.eah_codemp = empleado_asistencia.epa_codemp ) and  
         ( empleado_asistencia_horas.eah_sucid = empleado_asistencia.epa_sucid ) and  
         ( empleado_asistencia_horas.eah_emplid = empleado_asistencia.epa_emplid ) and  
         ( empleado_asistencia_horas.eah_dia = empleado_asistencia.epa_dia ) and  
         ( estados_empleado.eem_codemp = empleados.epl_codemp ) and  
         ( estados_empleado.eem_eemid = empleados.epl_eemid ) and  
         ( ( empleados.epl_codemp = @codemp ) AND  
         ( estados_empleado.eem_accion = 'A' and epl_emplid = @empleado and datepart(year, epa_dia) = @anio and datepart(month, epa_dia) = @mes ) )   
Group by epl_rut, epa_dia 


SELECT empleados.epl_rut RutCS,   
         epa_dia diaCS,   
         max(empleado_asistencia_horas.eah_salida) as SalCola
         into #ColacionSal  
    FROM empleados,   
         empleado_asistencia,   
         empleado_asistencia_horas,   
         estados_empleado  
   WHERE ( empleado_asistencia.epa_codemp = empleados.epl_codemp ) and  
         ( empleado_asistencia.epa_emplid = empleados.epl_emplid ) and  
         ( empleado_asistencia_horas.eah_codemp = empleado_asistencia.epa_codemp ) and  
         ( empleado_asistencia_horas.eah_sucid = empleado_asistencia.epa_sucid ) and  
         ( empleado_asistencia_horas.eah_emplid = empleado_asistencia.epa_emplid ) and  
         ( empleado_asistencia_horas.eah_dia = empleado_asistencia.epa_dia ) and  
         ( estados_empleado.eem_codemp = empleados.epl_codemp ) and  
         ( estados_empleado.eem_eemid = empleados.epl_eemid ) and  
         ( ( empleados.epl_codemp = @codemp ) AND  
         ( estados_empleado.eem_accion = 'A' and epl_emplid = @empleado and datepart(year, epa_dia) = @anio and datepart(month, epa_dia) = @mes ) and eah_item = 1 )   
Group by epl_rut, epa_dia 


SELECT empleados.epl_rut RutCE,   
         epa_dia diaCE,   
         min(empleado_asistencia_horas.eah_entrada) as EntCola
         into #ColacionEnt  
    FROM empleados,   
         empleado_asistencia,   
         empleado_asistencia_horas,   
         estados_empleado  
   WHERE ( empleado_asistencia.epa_codemp = empleados.epl_codemp ) and  
         ( empleado_asistencia.epa_emplid = empleados.epl_emplid ) and  
         ( empleado_asistencia_horas.eah_codemp = empleado_asistencia.epa_codemp ) and  
         ( empleado_asistencia_horas.eah_sucid = empleado_asistencia.epa_sucid ) and  
         ( empleado_asistencia_horas.eah_emplid = empleado_asistencia.epa_emplid ) and  
         ( empleado_asistencia_horas.eah_dia = empleado_asistencia.epa_dia ) and  
         ( estados_empleado.eem_codemp = empleados.epl_codemp ) and  
         ( estados_empleado.eem_eemid = empleados.epl_eemid ) and  
         ( ( empleados.epl_codemp = @codemp ) AND  
         ( estados_empleado.eem_accion = 'A' and epl_emplid = @empleado and datepart(year, epa_dia) = @anio and datepart(month, epa_dia) = @mes ) and eah_item = 2 )   
Group by epl_rut, epa_dia 




  SELECT empleados.epl_rut,   
         empleados.epl_nombre,   
         empleados.epl_apepat,   
         empleados.epl_apemat,   
         empleado_asistencia_horas.eah_item,   
         empleado_asistencia_horas.eah_entrada,   
         empleado_asistencia_horas.eah_salida,   
         empleado_asistencia_horas.eah_authora,   
         empleado_asistencia_horas.eah_pagsueldo,
         Entrada,
         Salida,
         SalCola,
         EntCola,
         tia_nombre,
         tia_tipo    
    FROM empleados,   
         empleado_asistencia,   
         empleado_asistencia_horas,   
         estados_empleado,
         #Asistencia,
         #ColacionSal, 
         #ColacionEnt,
         tipos_asistencia  
   WHERE ( empleado_asistencia.epa_codemp = empleados.epl_codemp ) and  
         ( empleado_asistencia.epa_emplid = empleados.epl_emplid ) and  
         ( eah_codemp = tia_codemp ) and  
         ( eah_tipoid = tia_tipoid ) and  
         ( empleado_asistencia_horas.eah_codemp = empleado_asistencia.epa_codemp ) and  
         ( empleado_asistencia_horas.eah_sucid = empleado_asistencia.epa_sucid ) and  
         ( empleado_asistencia_horas.eah_emplid = empleado_asistencia.epa_emplid ) and  
         ( empleado_asistencia_horas.eah_dia = empleado_asistencia.epa_dia ) and  
         ( estados_empleado.eem_codemp = empleados.epl_codemp ) and  
         ( estados_empleado.eem_eemid = empleados.epl_eemid ) and  
         ( ( empleados.epl_codemp = @codemp ) AND epl_rut = rutCS and epa_dia = diaCS and epl_rut = rutCE and epa_dia = diaCE and
         ( estados_empleado.eem_accion = 'A' ) and epl_rut = rut and epa_dia = dia  and datepart(year, epa_dia) = @anio and datepart(month, epa_dia) = @mes      )   
ORDER BY empleados.epl_apepat ASC,   
         empleado_asistencia_horas.eah_entrada ASC
