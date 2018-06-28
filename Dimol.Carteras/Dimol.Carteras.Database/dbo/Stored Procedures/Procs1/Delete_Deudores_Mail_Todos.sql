

Create Procedure Delete_Deudores_Mail_Todos(@ddm_codemp integer, @ddm_ctcid numeric (15), @ddm_mail varchar (80)) as  
  DELETE FROM deudores_mail  
   WHERE ( deudores_mail.ddm_codemp = @ddm_codemp ) AND  
         ( deudores_mail.ddm_ctcid = @ddm_ctcid ) AND  
         ( deudores_mail.ddm_mail = @ddm_mail )   

   DELETE FROM deudores_contactos_mail  
   WHERE ( deudores_contactos_mail.dcm_codemp = @ddm_codemp ) AND  
         ( deudores_contactos_mail.dcm_ctcid = @ddm_ctcid ) AND  
         ( deudores_contactos_mail.dcm_mail = @ddm_mail )
