
-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Pre_SMS_Grilla_Count]
(
@codemp int,
@ctcid integer, 
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);

set @query = '  select count(Numero) count from
  (select *,ROW_NUMBER() OVER (ORDER BY Numero) as row from    
  ('

set @query = @query +'SELECT [CODEMP] Codemp
      ,[CTCID] Ctcid
      ,[NUMERO] Numero
      ,[FECHA_ENVIO] FechaEnvio
      ,CASE [ESTADO]
			WHEN ''V'' THEN ''VIGENTE''
			WHEN ''N'' THEN ''NO VIGENTE''
			ELSE ''''
			END as Estado      
      ,[FECHA_ULT_MODIF] FechaModificacion
      ,[USUARIO_ULT_MODIF] UsuarioModificacion
  FROM [PRE_SMS_DEUDORES]
  WHERE CODEMP =  ' + CONVERT(VARCHAR,@codemp)+ '
  AND CTCID = ' + CONVERT(VARCHAR,@ctcid ) 

set @query = @query +') as tabla  ) as t
  where  row >=0 '
  
if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
