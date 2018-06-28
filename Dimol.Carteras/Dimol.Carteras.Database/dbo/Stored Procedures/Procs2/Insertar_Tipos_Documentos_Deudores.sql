

Create Procedure Insertar_Tipos_Documentos_Deudores(@tdd_codemp integer, @tdd_tddid integer, @tdd_nombre varchar(150), @tdd_tipo char(1)) as
  INSERT INTO tipos_documentos_deudores  
         ( tdd_codemp,   
           tdd_tddid,   
           tdd_nombre,   
           tdd_tipo )  
  VALUES ( @tdd_codemp,   
           @tdd_tddid,   
           @tdd_nombre,   
           @tdd_tipo )
