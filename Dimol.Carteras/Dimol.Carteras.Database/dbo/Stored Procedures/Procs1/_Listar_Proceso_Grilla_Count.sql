-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Proceso_Grilla_Count]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = ''

set @query = 'select count (Proceso) as count from
  (select *,ROW_NUMBER() OVER (ORDER BY Nombre asc) as row from    
  ('

set @query = @query + 'SELECT TOP 1000 P.[CODEMP] Codemp
      ,[PROCESO] Proceso
      ,T.NOMBRE_TIPO_PROCESO TipoProceso
      ,[NOMBRE] Nombre
      ,[DESCRIPCION] Descripcion
      ,[SERVIDOR] Servidor
      ,u1.USR_NOMBRE UsuarioIngreso
      ,[FECHA_INGRESO] FechaIngreso
      ,u2.USR_NOMBRE UsuarioModificacion
      ,[FECHA_MODIFICACION] FechaModificacion
  FROM [PROCESO] P with (nolock) 
  inner join TIPOS_PROCESO T with (nolock) 
  ON P.CODEMP = T.CODEMP
  AND P.TIPO_PROCESO = T.TIPO_PROCESO
  inner join USUARIOS u1
  on u1.USR_CODEMP = p.CODEMP
  and u1.USR_USRID = p.USUARIO_INGRESO
  inner join USUARIOS u2
  on u2.USR_CODEMP = p.CODEMP
  and u2.USR_USRID = p.USUARIO_MODIFICACION
  where p.CODEMP = '+ convert(char,@codemp) +'
  and p.Servidor != ''N/A'' '


set @query = @query +') as tabla  ) as t
  where  row > 0' 

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
