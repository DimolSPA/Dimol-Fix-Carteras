CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_SII_Clientes_Grilla]
(
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
	msii.PCLID,
	pv.PCL_NOMFANT Cliente, 
	msii.TOTAL_CARTERA SaldoCartera, 
	msii.TOTAL_VERDE SaldoVerde,
	Cast(CONVERT(decimal(5,0), msii.TOTAL_VERDE * 100.00/msii.TOTAL_CARTERA) as varchar(5)) + ''%'' PorSaldoVerde,
	msii.TOTAL_AMARILLO SaldoAmarillo,
	Cast(CONVERT(decimal(5,0), msii.TOTAL_AMARILLO * 100.00/msii.TOTAL_CARTERA) as varchar(5)) + ''%'' PorSaldoAmarillo,
	msii.TOTAL_ROJO SaldoRojo,
	Cast(CONVERT(decimal(5,0), msii.TOTAL_ROJO * 100.00/msii.TOTAL_CARTERA) as varchar(5)) + ''%'' PorSaldoRojo
from PODER_JUDICIAL_MONITOREO_SII msii
join PROVCLI pv
on msii.PCLID = pv.PCL_PCLID'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
