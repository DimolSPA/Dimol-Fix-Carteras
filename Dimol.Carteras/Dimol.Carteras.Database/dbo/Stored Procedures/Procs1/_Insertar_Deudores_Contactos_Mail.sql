CREATE Procedure [dbo].[_Insertar_Deudores_Contactos_Mail](@dcm_codemp integer, @dcm_ctcid numeric (15), @dcm_ddcid smallint,
                                                                                     @dcm_mail varchar (80), @dcm_tipo char (1), @dcm_masivo char(1)) as 
  
declare @existe int = 0   
SELECT @existe = count(deudores_contactos_mail.dcm_mail)
    FROM deudores_contactos_mail  
   WHERE ( deudores_contactos_mail.dcm_codemp = @dcm_codemp ) AND  
         ( deudores_contactos_mail.dcm_ctcid = @dcm_ctcid ) AND  
         ( deudores_contactos_mail.dcm_ddcid = @dcm_ddcid ) AND  
         ( deudores_contactos_mail.dcm_mail = @dcm_mail )
 if(@existe = 0 )
  
begin  

  
  
  INSERT INTO deudores_contactos_mail  
         ( dcm_codemp,   
           dcm_ctcid,   
           dcm_ddcid,   
           dcm_mail,   
           dcm_tipo,
           dcm_masivo )  
  VALUES ( @dcm_codemp,   
           @dcm_ctcid,   
           @dcm_ddcid,   
           @dcm_mail,   
           @dcm_tipo,
           @dcm_masivo )
end 
else
begin
  UPDATE deudores_contactos_mail  
     SET dcm_tipo = @dcm_tipo,
         dcm_masivo  = @dcm_masivo
   WHERE ( deudores_contactos_mail.dcm_codemp = @dcm_codemp ) AND  
         ( deudores_contactos_mail.dcm_ctcid = @dcm_ctcid ) AND  
         ( deudores_contactos_mail.dcm_ddcid = @dcm_ddcid ) AND  
         ( deudores_contactos_mail.dcm_mail = @dcm_mail )
end

