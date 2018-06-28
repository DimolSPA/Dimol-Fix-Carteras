CREATE PROCEDURE [dbo].[_Listar_Deudores_Quiebra_Grilla]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = ''

set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('
  
  
set @query = @query + 'select 
	dq.TRIBUNALID,
	dq.TIPOCAUSAID, 
	dq.MATERIAJODICIALID, 
	dq.RUT Rut, 
	dq.NOMBRE Deudor, 
	dq.ROLNUMERO,
	t.TRB_NOMBRE Tribunal,
	tci.TCI_NOMBRE Causa,
	mji.MJI_NOMBRE Materia  
from DEUDOR_QUIEBRA dq with(nolock)
left join TRIBUNALES t with(nolock)
on dq.CODEMP = t.TRB_CODEMP
and dq.TRIBUNALID = t.TRB_TRBID
left join TIPOS_CAUSA_IDIOMAS tci with(nolock)
on dq.CODEMP = tci.TCI_CODEMP
and dq.TIPOCAUSAID = tci.TCI_TCAID
left join MATERIA_JUDICIAL_IDIOMAS mji with(nolock)
on dq.CODEMP = mji.MJI_CODEMP
and dq.MATERIAJODICIALID = mji.MJI_ESJID
where dq.CODEMP = '+ convert(char,@codemp) + ''

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

exec(@query)	

END
