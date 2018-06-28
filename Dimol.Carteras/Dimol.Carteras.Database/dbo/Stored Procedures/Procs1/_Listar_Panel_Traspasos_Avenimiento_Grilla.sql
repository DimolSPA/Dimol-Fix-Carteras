CREATE PROCEDURE [dbo].[_Listar_Panel_Traspasos_Avenimiento_Grilla]
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
  
set @query = @query + 'select 
	pta.ROLID,
	pta.PCLID, 
	pta.CTCID, 
	pta.TRBID TribunalId, 
	pta.ROLNUMERO Rol, 
	p.PCL_NOMFANT Cliente,
	d.CTC_NOMFANT Deudor,
	tr.TRB_NOMBRE Tribunal,
	pta.FEC_REGISTRO FechaTraspasoAvenimiento
from PANEL_TRASPASOS_AVENIMIENTO pta with(nolock)
join PROVCLI p with(nolock)
on pta.CODEMP = p.PCL_CODEMP
and pta.PCLID = p.PCL_PCLID
join DEUDORES d with(nolock)
on pta.CODEMP = d.CTC_CODEMP
and pta.CTCID = d.CTC_CTCID
join TRIBUNALES tr with(nolock)
on pta.CODEMP = tr.TRB_CODEMP
and pta.TRBID = tr.TRB_TRBID
where pta.CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
and pta.estatus = ''E'''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
