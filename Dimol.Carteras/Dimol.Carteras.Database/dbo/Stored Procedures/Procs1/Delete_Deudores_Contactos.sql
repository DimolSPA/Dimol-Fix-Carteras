

Create Procedure Delete_Deudores_Contactos(@ddc_codemp integer, @ddc_ctcid numeric (15), @ddc_ddcid smallint) as  

   DELETE FROM deudores_contactos_mail  
   WHERE ( deudores_contactos_mail.dcm_codemp = @ddc_codemp ) AND  
         ( deudores_contactos_mail.dcm_ctcid = @ddc_ctcid ) AND  
         ( deudores_contactos_mail.dcm_ddcid = @ddc_ddcid )   

  DELETE FROM deudores_contactos_telefonos  
   WHERE ( deudores_contactos_telefonos.dct_codemp = @ddc_codemp ) AND  
         ( deudores_contactos_telefonos.dct_ctcid = @ddc_ctcid ) AND  
         ( deudores_contactos_telefonos.dct_ddcid = @ddc_ddcid ) 

  DELETE FROM deudores_contactos  
   WHERE ( deudores_contactos.ddc_codemp = @ddc_codemp ) AND  
         ( deudores_contactos.ddc_ctcid = @ddc_ctcid ) AND  
         ( deudores_contactos.ddc_ddcid = @ddc_ddcid )
