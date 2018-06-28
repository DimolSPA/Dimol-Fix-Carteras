

Create Procedure Insertar_Estados_Documentos_Diarios(@edc_codemp integer, @edc_edcid integer, @edc_tipmov char (1),
                                                                                              @edc_nombre varchar (20), @edc_estado smallint) as
    INSERT INTO estados_documentos_diarios  
         ( edc_codemp,   
           edc_edcid,   
           edc_tipmov,   
           edc_nombre,   
           edc_estado )  
  VALUES ( @edc_codemp,   
           @edc_edcid,   
           @edc_tipmov,   
           @edc_nombre,   
           @edc_estado )
