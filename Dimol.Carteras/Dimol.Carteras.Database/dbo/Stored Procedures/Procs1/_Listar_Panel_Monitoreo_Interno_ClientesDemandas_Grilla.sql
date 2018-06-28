CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_Interno_ClientesDemandas_Grilla]
(
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'WITH causas AS(
SELECT DISTINCT v.rol_pclid, h.id_causa, fecha.FECHA_HISTORIAL, 
case when fecha.FECHA_HISTORIAL > (DATEADD(DD, -15, GETDATE())) then 
''A'' 
else 
	case when fecha.FECHA_HISTORIAL > (DATEADD(DD, -30, GETDATE())) and fecha.FECHA_HISTORIAL < (DATEADD(DD, -15, GETDATE())) then 
	''B'' 
	else 
		case when fecha.FECHA_HISTORIAL > (DATEADD(DD, -60, GETDATE())) and fecha.FECHA_HISTORIAL < (DATEADD(DD, -30, GETDATE())) then 
		''C'' 
		else 
			case when fecha.FECHA_HISTORIAL < (DATEADD(DD, -60, GETDATE())) then 
			''D'' 
			else 
			 NULL 
			end 
		end  
	end 
end Tiempo
from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h WITH (NOLOCK)
inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
on R.ID_CAUSA = h.ID_CAUSA
inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
ON r.TRIBUNAL = K.ID_TRIBUNAL
inner join [Iluvatar].[dbo].VIEW_ROL v WITH (NOLOCK)
on v.rol_numero = CONVERT(varchar, R.NUMERO)+''-''+CONVERT(varchar, R.anio) and v.trb_nombre = k.TRIBUNAL
inner join (SELECT TOP 1 WITH TIES 
				FECHA_HISTORIAL, ID_CAUSA
			FROM 
				[10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial WITH (NOLOCK)
			ORDER BY
				ROW_NUMBER() OVER(PARTITION BY ID_CAUSA ORDER BY FECHA_HISTORIAL DESC)) fecha
on h.ID_CAUSA = fecha.ID_CAUSA)
select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select causas.rol_pclid Pclid, pv.PCL_NOMFANT Cliente,
    count(*) TotalCausas,
    sum(case when causas.Tiempo = ''A'' then 1 else 0 end) ACount,
    sum(case when causas.Tiempo = ''B'' then 1 else 0 end) BCount,
    sum(case when causas.Tiempo = ''C'' then 1 else 0 end) CCount,
    sum(case when causas.Tiempo = ''D'' then 1 else 0 end) DCount,
	sum(case when causas.Tiempo = ''A'' then 1 else 0 end) ActualizadasCount,
	sum(case when causas.Tiempo = ''B'' or causas.Tiempo = ''C'' or causas.Tiempo = ''D'' then 1 else 0 end) NoActualizadasCount,
	Cast(CONVERT(decimal(5,0), sum(case when causas.Tiempo = ''A'' then 1 else 0 end) * 100.00/count(*)) as varchar(5)) + ''%'' Porcentaje
from causas
join PROVCLI pv
on causas.rol_pclid = pv.PCL_PCLID
group by causas.rol_pclid, pv.PCL_NOMFANT'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
