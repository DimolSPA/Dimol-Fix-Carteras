create procedure [dbo].[_Trae_Mail_Cocha] (@idmail int, @idtemplate int) as

select Header, Asunto, Body 
from Email_Template 
where Id_Mail = @idmail
and Id_Template = @idtemplate
