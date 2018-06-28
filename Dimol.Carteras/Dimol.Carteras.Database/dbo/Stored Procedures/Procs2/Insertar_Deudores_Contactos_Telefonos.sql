

Create Procedure Insertar_Deudores_Contactos_Telefonos(@dct_codemp integer, @dct_ctcid numeric (15), @dct_ddcid smallint,
                                                                                              @dct_numero numeric(12), @dct_tipo char (1), @dct_estado char (1)) as 
  INSERT INTO deudores_contactos_telefonos  
         ( dct_codemp,   
           dct_ctcid,   
           dct_ddcid,   
           dct_numero,   
           dct_tipo,   
           dct_estado,
           dct_prioridad )  
  VALUES ( @dct_codemp,   
           @dct_ctcid,   
           @dct_ddcid,   
           @dct_numero,   
           @dct_tipo,   
           @dct_estado,
           4 )
