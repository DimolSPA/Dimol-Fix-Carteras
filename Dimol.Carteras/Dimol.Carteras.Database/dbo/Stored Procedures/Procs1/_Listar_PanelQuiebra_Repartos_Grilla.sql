CREATE PROCEDURE [dbo].[_Listar_PanelQuiebra_Repartos_Grilla]
(
@quiebraId int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select 
	REPARTO_ID RepartoId, 
	QUIEBRA_ID QuiebraId, 
	FEC_REPARTO FecReparto, 
	MTO_REPARTO MtoReparto  
from PANEL_QUIEBRA_REPARTOS with(nolock)
where QUIEBRA_ID = '+ CONVERT(VARCHAR,@quiebraId) + ''
set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
