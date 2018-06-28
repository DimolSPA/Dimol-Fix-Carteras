

Create Procedure Find_Deudores_Contactos_Mail(@dcm_codemp integer, @dcm_ctcid integer, @dcm_ddcid integer, @dcm_mail varchar(80)) as
  SELECT count(deudores_contactos_mail.dcm_mail)
    FROM deudores_contactos_mail  
   WHERE ( deudores_contactos_mail.dcm_codemp = @dcm_codemp ) AND  
         ( deudores_contactos_mail.dcm_ctcid = @dcm_ctcid ) AND  
         ( deudores_contactos_mail.dcm_ddcid = @dcm_ddcid ) AND  
         ( deudores_contactos_mail.dcm_mail = @dcm_mail )
