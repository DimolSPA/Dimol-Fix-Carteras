

Create Procedure Delete_Empleados_Documentos(@epd_codemp integer, @epd_emplid integer,  @epd_item smallint) as 
  DELETE FROM empleados_documentos  
   WHERE ( empleados_documentos.epd_codemp = @epd_codemp ) AND  
         ( empleados_documentos.epd_emplid = @epd_emplid ) AND  
         ( empleados_documentos.epd_item = @epd_item )
