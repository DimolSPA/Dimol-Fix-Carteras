﻿CREATE PROCEDURE [dbo].[_Listar_Tipos_Insumos_PorTabla_Count]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(ID) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select tii_tipid as ID, TII_NOMBRE as NOMBRE
	  FROM [dbo].[tipos_insumo_idiomas]
	  where tii_codemp = ' + CONVERT(VARCHAR,@codemp)

   set @query = @query +')as tabla ) as t
  where  row >= 0' 

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END