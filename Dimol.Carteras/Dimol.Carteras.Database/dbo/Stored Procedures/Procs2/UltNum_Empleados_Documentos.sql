

Create Procedure UltNum_Empleados_Documentos(@epd_codemp integer, @epd_emplid integer) as
  SELECT IsNull(Max(empleados_documentos.epd_item)+1, 1)       
    FROM empleados_documentos  
   WHERE ( empleados_documentos.epd_codemp = @epd_codemp ) AND  
         ( empleados_documentos.epd_emplid = @epd_emplid )
