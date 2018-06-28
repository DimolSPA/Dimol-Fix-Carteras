
CREATE PROCEDURE [dbo].[_Trae_Estados_Cartera_Perfil_Count]
(
@codemp integer, 
@prfid int, 
@agrupaid char(1), 
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
 
set @query = '  select count(estid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('
  
set @query = @query + 'SELECT ec.ect_estid estid, eci.eci_nombre nombre, CASE
											WHEN EXISTS (SELECT 1
											   FROM perfiles_estados pfe with(nolock)
											  WHERE pfe.PFE_CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
												AND pfe.PFE_ESTID = eci.eci_estid
												AND pfe.PFE_PRFID = ' + CONVERT(VARCHAR,@prfid) +') THEN ''true''
											ELSE ''false''
										END AS IsSelected 
	FROM ESTADOS_CARTERA ec with(nolock)
	JOIN ESTADOS_CARTERA_IDIOMAS eci with(nolock)
	on ec.ect_codemp = eci.eci_codemp
	and ec.ect_estid = eci.eci_estid
	WHERE eci.eci_idid = ' + CONVERT(VARCHAR,@idioma) +'
	AND ec.ect_codemp = ' + CONVERT(VARCHAR,@codemp) +'
	AND ec.ect_agrupa = ' + CONVERT(VARCHAR,@agrupaid) +'
	AND ec.ect_prejud IN (''P'', ''A'')
	UNION
	SELECT ec.ect_estid estid, eci.eci_nombre nombre, CASE
											WHEN EXISTS (SELECT 1
											   FROM perfiles_estados pfe with(nolock)
											  WHERE pfe.PFE_CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
												AND pfe.PFE_ESTID = eci.eci_estid
												AND pfe.PFE_PRFID = ' + CONVERT(VARCHAR,@prfid) +') THEN ''true''
											ELSE ''false''
										END AS IsSelected 
	FROM 
	ESTADOS_CARTERA ec with(nolock)
	JOIN ESTADOS_CARTERA_IDIOMAS eci with(nolock)
	on ec.ect_codemp = eci.eci_codemp
	and ec.ect_estid = eci.eci_estid
	WHERE eci.eci_idid = ' + CONVERT(VARCHAR,@idioma) +'
	AND ec.ect_codemp = ' + CONVERT(VARCHAR,@codemp) +'
	AND ec.ect_agrupa = ' + CONVERT(VARCHAR,@agrupaid) +'
	AND ec.ect_prejud = ''J''
	AND ec.ect_estid not in ( select mej_estid from materia_estados with(nolock) where mej_codemp = ' + CONVERT(VARCHAR,@codemp) +')'


   set @query = @query +')as tabla ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END