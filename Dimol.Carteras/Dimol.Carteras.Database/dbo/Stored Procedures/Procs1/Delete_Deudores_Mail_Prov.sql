

CREATE Procedure [dbo].[Delete_Deudores_Mail_Prov](@codemp integer, @ctcid numeric (15), @mail varchar (80)) as  

   DELETE FROM [dbo].[DEUDORES_CONTACTOS_MAIL_PROV]  
   WHERE ( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CODEMP = @codemp ) AND  
         ( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CTCID = @ctcid ) AND  
         ( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MAIL = @mail )