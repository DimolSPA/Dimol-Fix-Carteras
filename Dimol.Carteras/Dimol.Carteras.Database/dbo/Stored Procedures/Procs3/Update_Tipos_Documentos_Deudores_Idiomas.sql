

Create Procedure Update_Tipos_Documentos_Deudores_Idiomas(@tdi_codemp integer, @tdi_tddid integer, @tdi_idid integer, @tdi_nombre varchar(200)) as
   UPDATE tipos_documentos_deudores_idiomas  
     SET tdi_nombre = @tdi_nombre  
   WHERE ( tipos_documentos_deudores_idiomas.tdi_codemp = @tdi_codemp ) AND  
         ( tipos_documentos_deudores_idiomas.tdi_tddid = @tdi_tddid ) AND  
         ( tipos_documentos_deudores_idiomas.tdi_idid = @tdi_idid )
