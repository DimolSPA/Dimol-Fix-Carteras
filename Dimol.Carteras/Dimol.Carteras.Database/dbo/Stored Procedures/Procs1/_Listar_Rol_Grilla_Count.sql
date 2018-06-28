-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Rol_Grilla_Count]
(
@codemp int,
@ctcid int,
@idioma int,
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
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  ('

set @query = @query +'SELECT count(r.rol_rolid) count 
				FROM view_rol r
				WHERE  r.rol_codemp =' + CONVERT(VARCHAR,@codemp) +'
						and r.rol_ctcid = ' + CONVERT(VARCHAR,@ctcid ) +'
						and r.eci_idid = ' + CONVERT(VARCHAR,@idioma) +'
						and r.mji_idid = ' + CONVERT(VARCHAR,@idioma) +'
						and tci_idid =' + CONVERT(VARCHAR,@idioma)

set @query = @query +') as tabla  ) as t
  where  row >= 0 and row <= 11111'
  
if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
