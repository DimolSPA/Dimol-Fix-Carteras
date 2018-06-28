-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor_Email_Sitrel]
(
@CODEMP int 
,@CTCID int
,@EMAIL varchar(255)
,@ORIGEN VARCHAR(1)
,@ENVIADO VARCHAR(1)
)
AS

declare @existe int=0

set @existe = (select count([CODEMP])    from [DEUDORES_MAIL_SITREL] where  [CODEMP] =@CODEMP  and [CTCID]=@CTCID and [MAIL]=@EMAIL)  
                                                                  
if @existe = 0
BEGIN
	
	INSERT INTO [DEUDORES_MAIL_SITREL]
           ([CODEMP]
           ,[CTCID]
           ,[MAIL]
           ,[FECHA]
           ,[ORIGEN]
           ,[ENVIADO])
     VALUES
           (@CODEMP
			,@CTCID 
			,@EMAIL
			,GETDATE()
			,@ORIGEN
			,@ENVIADO
			)

END
