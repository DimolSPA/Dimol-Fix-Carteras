

Create Procedure Delete_Deudores_Contactos_Telefonos(@dct_codemp integer, @dct_ctcid numeric (15), @dct_ddcid smallint, @dct_numero integer) as  
  DELETE FROM deudores_contactos_telefonos  
   WHERE ( deudores_contactos_telefonos.dct_codemp = @dct_codemp ) AND  
         ( deudores_contactos_telefonos.dct_ctcid = @dct_ctcid ) AND  
         ( deudores_contactos_telefonos.dct_ddcid = @dct_ddcid ) AND  
         ( deudores_contactos_telefonos.dct_numero = @dct_numero )
