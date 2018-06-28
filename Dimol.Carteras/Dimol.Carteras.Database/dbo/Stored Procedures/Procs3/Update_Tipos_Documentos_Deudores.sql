

Create Procedure Update_Tipos_Documentos_Deudores(@tdd_codemp integer, @tdd_tddid integer, @tdd_nombre varchar(150), @tdd_tipo char(1)) as
  UPDATE tipos_documentos_deudores  
     SET tdd_nombre = @tdd_nombre,   
         tdd_tipo = @tdd_tipo  
   WHERE ( tipos_documentos_deudores.tdd_codemp = @tdd_codemp ) AND  
         ( tipos_documentos_deudores.tdd_tddid = @tdd_tddid )
