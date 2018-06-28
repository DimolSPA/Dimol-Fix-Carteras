-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Pre_Email_Grilla]
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

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query +'SELECT [CODEMP] Codemp
      ,[CTCID] Ctcid
      ,[EMAIL] Email
      ,[FECHA_ENVIO] FechaEnvio
      ,CASE [ESTADO]
			WHEN ''V'' THEN ''VIGENTE''
			WHEN ''N'' THEN ''NO VIGENTE''
			ELSE ''''
			END as Estado     
      ,[FECHA_ULT_MODIF] FechaModificacion
      ,[USR_NOMBRE] UsuarioModificacion
  FROM [PRE_EMAIL_DEUDORES], USUARIOS
  WHERE CODEMP =  ' + CONVERT(VARCHAR,@codemp)+ '
  AND CTCID = ' + CONVERT(VARCHAR,@ctcid )  + '
  and USR_CODEMP=CODEMP
  and USR_USRID = USUARIO_ULT_MODIF
  and [ESTADO] = ''V'' '

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
