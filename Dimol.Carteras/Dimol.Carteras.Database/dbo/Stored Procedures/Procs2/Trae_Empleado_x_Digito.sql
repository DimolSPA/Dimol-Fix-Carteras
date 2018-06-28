

Create Procedure Trae_Empleado_x_Digito(@epl_codemp integer, @epl_digito integer) as
  SELECT empleados.epl_emplid  
    FROM empleados  
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_digito = @epl_digito )
