

Create Procedure Update_Deudores_Mail(@ddm_codemp integer, @ddm_ctcid numeric (15), @ddm_mail varchar (80), @ddm_tipo char (1), @ddm_masivo char(1)) as  
  UPDATE deudores_mail  
     SET ddm_tipo = @ddm_tipo,
         ddm_masivo  = @ddm_masivo
   WHERE ( deudores_mail.ddm_codemp = @ddm_codemp ) AND  
         ( deudores_mail.ddm_ctcid = @ddm_ctcid ) AND  
         ( deudores_mail.ddm_mail = @ddm_mail )
