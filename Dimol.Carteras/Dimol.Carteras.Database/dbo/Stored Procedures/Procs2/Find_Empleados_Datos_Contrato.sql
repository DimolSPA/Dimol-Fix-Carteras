

Create Procedure Find_Empleados_Datos_Contrato(@edc_codemp integer, @edc_emplid integer) as
  SELECT Count(empleados_datos_contrato.edc_emplid) 
    FROM empleados_datos_contrato  
   WHERE ( empleados_datos_contrato.edc_codemp = @edc_codemp ) AND  
         ( empleados_datos_contrato.edc_emplid = @edc_emplid )
