CREATE PROCEDURE [dbo].[_Listar_Usuarios_Grilla]
(
@codemp int,
@usr_nombre varchar(255),
@usr_login varchar(255),
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
  (' set @query = @query + 'select usr_usrid as Id, usr_nombre as Nombre, case usr_estado 
  when ''H'' then ''Habilitado'' else ''Bloqueado'' end as Estado, usr_fecultlog as FechaUltimoIngreso, usr_fecblock as FechaBloqueo
  from [dbo].[USUARIOS] 
  where usr_codemp = ' + CONVERT(VARCHAR,@codemp)

	  	
	 		
	  if @usr_nombre is not null 
		begin
			set @query = @query + ' and usr_nombre like ''%' +@usr_nombre + '%''';
		end
		
	   if @usr_login is not null 
		begin
			set @query = @query + ' and usr_login like ''%' +@usr_login + '%''';
		end
		
		    
   set @query = @query +')as tabla ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
