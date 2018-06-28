CREATE PROCEDURE [dbo].[_Listar_PanelQuiebra_Diagrama_Liquidaciones_Grilla]
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
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select PQ.QUIEBRA_ID QuiebraId,
	p.PCL_PCLID Pclid,
	p.PCL_NOMFANT Cliente,
	d.CTC_CTCID Ctcid,
	d.CTC_RUT Rut,
	d.CTC_NOMFANT Deudor,
	(select sbc.SBC_NOMBRE from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PQ.CODEMP and sbc.SBC_SBCID = PQ.SBCID) Asegurado, 
	PQD.FEC_RESOLUCION_LIQUIDACION_BC FECPUBLICACION,
	DATEDIFF(day, PQD.FEC_RESOLUCION_LIQUIDACION_BC, isnull(PQD.FEC_VERIFICACION, GETDATE())) dias
from PANEL_QUIEBRA PQ with(nolock)
join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID 
join ROL r with(nolock)
on PQ.ROLID = r.ROL_ROLID
join PROVCLI p with(nolock)
on r.ROL_CODEMP= p.PCL_CODEMP
and r.ROL_PCLID = p.PCL_PCLID
join DEUDORES d with(nolock)
on r.ROL_CTCID = d.CTC_CTCID
where PQ.CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
and r.ROL_ESJID IN(3,9)
and PQD.FEC_RESOLUCION_LIQUIDACION_BC IS NOT NULL'
set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
