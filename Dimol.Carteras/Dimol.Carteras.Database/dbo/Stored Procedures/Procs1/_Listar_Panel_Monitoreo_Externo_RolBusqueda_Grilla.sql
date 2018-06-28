CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_Externo_RolBusqueda_Grilla]
(
@codemp int,
@zonaId int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'WITH RolesBuscados AS (
	Select  tar.TRIBUNALID, 
		tar.TRIBUNAL, 
		tar.ANIO, 
		tar.MINROL, 
		tar.MAXROL, 
		(tar.MAXROL -tar.MINROL) + 1 Buscados,  
		count(tar.ROL) NoEncontrados
		from TRIBUNALES_ZONAS tz
		join TRIBUNALES_A_PODER_JUDICIAL tpj
		on tz.CODEMP = tpj.CODEMP
		and tz.TRBID = tpj.TRBID
		join PODER_JUDICIAL_MONITOREO_ALERTAROLES tar
		on tpj.ID_TRIBUNAL = tar.TRIBUNALID
		where tz.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
		and tz.ZONAID = ' + CONVERT(VARCHAR,@zonaId) +'
		and tar.ROLENCONTRADO is null
		group by tar.TRIBUNALID, tar.TRIBUNAL, tar.ANIO, tar.MINROL, tar.MAXROL)
select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'Select TRIBUNALID,
	 TRIBUNAL,
	 ANIO,
	 MINROL,
	 MAXROL,
	 Buscados - NoEncontrados Encontrados,
	 NoEncontrados,
	 CAST(CONVERT(decimal(5,0), (Buscados - NoEncontrados) * 100.00 / Buscados) as varchar(5)) + ''%'' Porcentaje
from RolesBuscados'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
