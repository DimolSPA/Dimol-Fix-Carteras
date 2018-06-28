

Create Procedure Delete_Deudores_Mail(@ddm_codemp integer, @ddm_ctcid numeric (15), @ddm_mail varchar (80)) as  
  DELETE FROM deudores_mail  
   WHERE ( deudores_mail.ddm_codemp = @ddm_codemp ) AND  
         ( deudores_mail.ddm_ctcid = @ddm_ctcid ) AND  
         ( deudores_mail.ddm_mail = @ddm_mail )
