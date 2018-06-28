

Create Procedure Delete_Empleados(@epl_codemp integer, @epl_emplid integer) as 

  DELETE FROM empleados_datos_contrato  
   WHERE ( empleados_datos_contrato.edc_codemp = @epl_codemp ) AND  
         ( empleados_datos_contrato.edc_emplid = @epl_emplid ) 

  DELETE FROM empleados  
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_emplid = @epl_emplid )
