

Create Procedure Insertar_Deudores_Documentos(@dcd_codemp integer, @dcd_ctcid integer, @dcd_dcdid integer, @dcd_tddid integer, @dcd_nombre varchar(800), @dcd_pclid integer) as
  INSERT INTO deudores_documentos  
         ( dcd_codemp,   
           dcd_ctcid,   
           dcd_dcdid,   
           dcd_tddid,   
           dcd_nombre,   
           dcd_pclid )  
  VALUES ( @dcd_codemp,   
           @dcd_ctcid,   
           @dcd_dcdid,   
           @dcd_tddid,   
           @dcd_nombre,   
           @dcd_pclid )
