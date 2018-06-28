﻿

Create Procedure UltNum_Empleado_Asistencia_Hora(@eah_codemp integer, @eah_sucid integer, @eah_emplid integer, @eah_dia datetime) as
  SELECT IsNull(Max(eah_item)+1, 1)  
    FROM empleado_asistencia_horas  
   WHERE ( empleado_asistencia_horas.eah_codemp = @eah_codemp ) AND  
         ( empleado_asistencia_horas.eah_sucid = @eah_sucid ) AND  
         ( empleado_asistencia_horas.eah_emplid = @eah_emplid ) AND  
         ( datepart(year,empleado_asistencia_horas.eah_dia) = datepart(year,@eah_dia) ) AND  
         ( datepart(month,empleado_asistencia_horas.eah_dia) = datepart(month,@eah_dia) ) AND  
         ( datepart(day,empleado_asistencia_horas.eah_dia) = datepart(day,@eah_dia) )
