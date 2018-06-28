CREATE PROCEDURE [dbo].[_Listar_Panel_Quiebra_Rol_Liquidacion_Grilla]
(
@codemp int,
@rolId int,
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
	QUIEBRA_ID, 
	ROLID,
	ROLNUMERO Rol,
	(select SBC_NOMBRE from SUBCARTERAS with(nolock) where SBC_CODEMP = CODEMP and SBC_SBCID = SBCID) Nombre,
	CUANTIA,
	(select ROL_ESJID from ROL where ROL_ROLID = ROLID) Materia
from PANEL_QUIEBRA with(nolock)
where CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
 AND ROLID = '+ CONVERT(VARCHAR,@rolId) + ''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
