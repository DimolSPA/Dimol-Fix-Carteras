


CREATE procedure [dbo].[_Trae_User_PJ] (@id int, @tipo varchar(1)) as

declare @sql varchar(8000)

set @sql = 'select 
top 1 USRID, 
NOMBRE, 
LOGIN, 
PASSWORD, 
PCLID, 
ISNULL(PCL_RUT + '' - '' + PCL_NOMFANT, '''') RUTCLIENTE, 
ACTIVA, 
ADM, 
case 
WHEN USRID <= (select min(USRID) USRID from USUARIOS_PJ) THEN 1
ELSE 0 END AS L,
case USRID
WHEN (select max(USRID) USRID from USUARIOS_PJ) THEN 1
ELSE 0 END AS R 
from USUARIOS_PJ
LEFT JOIN PROVCLI 
ON PCL_PCLID = PCLID
 '

if @tipo = 'R' 
begin
	set @sql = @sql + 'where USRID > ' + convert(varchar,@id)
end 
else
begin
	set @sql = @sql + 'where USRID < ' + convert(varchar,@id) + ' ORDER BY USRID DESC'
end	 

exec(@sql)


