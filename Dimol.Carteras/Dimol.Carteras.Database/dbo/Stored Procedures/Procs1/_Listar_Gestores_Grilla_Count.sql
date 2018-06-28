CREATE PROCEDURE [dbo].[_Listar_Gestores_Grilla_Count]
(
@codemp int ,
@sucursal int,
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
 
set @query = '  select count(ges_gesid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('
  
set @query = @query + 'SELECT ges_gesid, ges_nombre nombre, ges_telefono, ges_email, 
						CASE ges_estado when ''A'' then ''Activo'' else ''Inactivo'' end as estado,
						ges_tipcart, grupo_cobranza_gestor.gcg_grcid as grupoid, ges_remoto,
						ges_visita_terreno, ges_telefono_terreno, ges_imei, ges_emplid						
						FROM gestor inner join grupo_cobranza_gestor    
						on GESTOR.GES_CODEMP =  grupo_cobranza_gestor.GCG_CODEMP AND     
						GESTOR.GES_SUCID =  grupo_cobranza_gestor.GCG_SUCID AND    
						GESTOR.GES_GESID =  grupo_cobranza_gestor.GCG_GESID 
						WHERE ges_codemp = ' + CONVERT(VARCHAR,@codemp) +'
						and ges_sucid = ' + CONVERT(VARCHAR,@sucursal)


   set @query = @query +')as tabla ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
