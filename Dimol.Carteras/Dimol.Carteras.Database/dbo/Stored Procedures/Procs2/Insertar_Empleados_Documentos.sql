

 Create Procedure Insertar_Empleados_Documentos(@epd_codemp integer, @epd_emplid integer, @epd_tdcid integer, @epd_item smallint, @epd_documento image) as
  INSERT INTO empleados_documentos  
         ( epd_codemp,   
           epd_emplid,   
           epd_tdcid,   
           epd_item,   
           epd_documento )  
  VALUES ( @epd_codemp,   
           @epd_emplid,   
           @epd_tdcid,   
           @epd_item,   
           @epd_documento )
