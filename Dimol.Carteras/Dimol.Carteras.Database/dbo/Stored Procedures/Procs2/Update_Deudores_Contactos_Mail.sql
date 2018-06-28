

Create Procedure Update_Deudores_Contactos_Mail(@dcm_codemp integer, @dcm_ctcid numeric (15), @dcm_ddcid smallint,
                                                                                      @dcm_mail varchar (80), @dcm_tipo char (1), @dcm_masivo char(1)) as  
  UPDATE deudores_contactos_mail  
     SET dcm_tipo = @dcm_tipo,
         dcm_masivo  = @dcm_masivo
   WHERE ( deudores_contactos_mail.dcm_codemp = @dcm_codemp ) AND  
         ( deudores_contactos_mail.dcm_ctcid = @dcm_ctcid ) AND  
         ( deudores_contactos_mail.dcm_ddcid = @dcm_ddcid ) AND  
         ( deudores_contactos_mail.dcm_mail = @dcm_mail )
