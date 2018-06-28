

Create Procedure Find_Empleados(@epl_codemp integer, @epl_emplid integer) as
  SELECT count(empleados.epl_emplid)  
    FROM empleados  
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_emplid = @epl_emplid )
