

Create Procedure Insertar_Tipos_Documentos_Deudores_Idiomas(@tdi_codemp integer, @tdi_tddid integer, @tdi_idid integer, @tdi_nombre varchar(200)) as
   INSERT INTO tipos_documentos_deudores_idiomas  
         ( tdi_codemp,   
           tdi_tddid,   
           tdi_idid,   
           tdi_nombre )  
  VALUES ( @tdi_codemp,   
           @tdi_tddid,   
           @tdi_idid,   
           @tdi_nombre )
