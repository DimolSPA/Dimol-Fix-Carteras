

Create Procedure Delete_Empleados_Datos_Contratos(@edc_codemp integer, @edc_emplid integer) as 
  DELETE FROM empleados_datos_contrato  
   WHERE ( empleados_datos_contrato.edc_codemp = @edc_codemp ) AND  
         ( empleados_datos_contrato.edc_emplid = @edc_emplid )
