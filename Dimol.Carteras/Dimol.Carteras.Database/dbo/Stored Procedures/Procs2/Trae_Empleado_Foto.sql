

Create Procedure Trae_Empleado_Foto(@epl_codemp integer, @epl_emplid integer) as
  SELECT epl_foto
    FROM empleados  
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_emplid = @epl_emplid )
