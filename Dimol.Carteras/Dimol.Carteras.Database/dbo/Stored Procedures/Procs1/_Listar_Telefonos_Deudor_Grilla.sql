-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Telefonos_Deudor_Grilla]
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

set @query = @query +'select ddt_codemp Codemp, ddt_ctcid Ctcid,
			ddt_numero Numero, 
			ddt_tipo IdTipoTelefono, 
			CASE ddt_tipo
			WHEN ''C'' THEN ''Casa''
			WHEN ''M'' THEN ''Celular''
			WHEN ''O'' THEN ''Otro''
			WHEN ''F'' THEN ''Fax''
			ELSE ''''
			END as TipoTelefono,
			ddt_estado IdEstadoTelefono,
			CASE ddt_estado
			WHEN ''A'' THEN ''Activo''
			WHEN ''C'' THEN ''Cortado''
			WHEN ''M'' THEN ''Malo''
			ELSE ''''
			END as EstadoTelefono
			from deudores_telefonos 
			where ddt_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
			and ddt_ctcid = ' + CONVERT(VARCHAR,@ctcid ) 

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
