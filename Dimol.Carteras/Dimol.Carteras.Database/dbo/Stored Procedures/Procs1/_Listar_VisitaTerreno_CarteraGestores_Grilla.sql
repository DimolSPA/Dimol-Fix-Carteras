CREATE PROCEDURE [dbo].[_Listar_VisitaTerreno_CarteraGestores_Grilla]
(
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
  (	' 
  
set @query = @query + 'select vtcg.CARTERA_ID, 
						vtcg.CARTERA_NOMBRE, 
						vtcg.DESCRIPCION,
						ges.GES_GESID,
						ges.GES_NOMBRE, 
						ges.GES_TELEFONO_TERRENO, 
						ges.GES_IMEI 
						from VISITA_TERRENO_CARTERA_GESTOR vtcg
						Join GESTOR ges
						on vtcg.GESID = ges.GES_GESID'
 
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
