

Create Procedure UltNum_Empleados(@epl_codemp integer) as
  SELECT IsNull(Max(empleados.epl_emplid)+1, 1)  
    FROM empleados  
   WHERE ( empleados.epl_codemp = @epl_codemp )
