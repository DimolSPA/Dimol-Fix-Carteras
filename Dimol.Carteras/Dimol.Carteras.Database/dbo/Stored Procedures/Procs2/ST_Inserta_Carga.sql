-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Carga]
(
@codemp int ,
@pclid int
)
AS
BEGIN

INSERT INTO [SITREL_CARGA]
           ([CODEMP]
           ,[PCLID]
           ,[FECHA_CARGA]
           ,[ESTADO]
           ,[ERROR])
     VALUES
           (@codemp
           ,@pclid
           ,GETDATE()
           ,'PR'
           ,'') 
           
select SCOPE_IDENTITY()   id         

END
