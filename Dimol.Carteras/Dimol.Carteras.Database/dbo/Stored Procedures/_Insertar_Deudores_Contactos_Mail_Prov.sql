
CREATE Procedure [dbo].[_Insertar_Deudores_Contactos_Mail_Prov](
@codemp integer, 
@ctcid numeric (15), 
@ddcid smallint,
@mail varchar (80), 
@tipo char (1), 
@masivo char(1), 
@pclid numeric (15),
@userid integer,
@fecha_creacion datetime)
 AS 

DECLARE @Existe int = 0  

--Verificamos si el email existe antes de crear o hacer update

SELECT @Existe = count(
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MAIL)
FROM [dbo].[DEUDORES_CONTACTOS_MAIL_PROV] 
WHERE ( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CODEMP = @codemp ) AND  
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CTCID = @ctcid ) AND  
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].DDCID = @ddcid ) AND  
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MAIL = @mail ) AND
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].PCLID = @pclid )

--Si no existe creamos

IF(@Existe = 0 )
BEGIN  

INSERT INTO [dbo].[DEUDORES_CONTACTOS_MAIL_PROV]  
( 
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CODEMP,   
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CTCID,   
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].DDCID,   
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MAIL,   
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].TIPO,
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MASIVO,
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].PCLID,
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].[FECHA_CREACION],
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].USERID

)  
VALUES ( 
@codemp,   
@ctcid,   
@ddcid,   
@mail,   
@tipo,
@masivo,
@pclid ,
@fecha_creacion,
@userid
)
END 

--Si @Existe > 0 actualizamos el registro

ELSE
BEGIN
UPDATE [dbo].[DEUDORES_CONTACTOS_MAIL_PROV] 
SET [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].TIPO = @tipo,
[dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MASIVO  = @masivo
WHERE ( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CODEMP = @codemp ) AND  
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].CTCID = @ctcid ) AND  
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].DDCID = @ddcid ) AND  
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].MAIL = @mail ) AND
	( [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].PCLID = @pclid)
END

