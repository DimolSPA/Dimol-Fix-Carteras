

Create Procedure Update_Deudores_Contactos_Telefonos(@dct_codemp integer, @dct_ctcid numeric (15), @dct_ddcid smallint,
                                                                                               @dct_numero integer, @dct_tipo char (1), @dct_estado char (1)) as  
  UPDATE deudores_contactos_telefonos  
     SET dct_tipo = @dct_tipo,   
         dct_estado = @dct_estado  
   WHERE ( deudores_contactos_telefonos.dct_codemp = @dct_codemp ) AND  
         ( deudores_contactos_telefonos.dct_ctcid = @dct_ctcid ) AND  
         ( deudores_contactos_telefonos.dct_ddcid = @dct_ddcid ) AND  
         ( deudores_contactos_telefonos.dct_numero = @dct_numero )
