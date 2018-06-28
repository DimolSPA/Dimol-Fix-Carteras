

CREATE procedure [dbo].[_Trae_Ruta_Logo_Empresa_PJ] (@id int, @tipo varchar(1)) as 	 

declare @sql varchar(8000)

 set @sql = 'select 
 top 1 ID,
 PCLID,
 ISNULL(PCL_NOMFANT, '''') PCL_NOMFANT,
 RUTA,
	case 
	WHEN ID <= (select min(ID) ID from USUARIOS_PJ_RUTAS) THEN 1
	ELSE 0 END AS L,
	case ID
	WHEN (select max(ID) ID from USUARIOS_PJ_RUTAS) THEN 1
	ELSE 0 END AS R 	
 from USUARIOS_PJ_RUTAS 
 LEFT JOIN PROVCLI 
 ON PCL_PCLID = PCLID '

if @tipo = 'R' 
begin
	set @sql = @sql + 'where ID > ' + convert(varchar,@id)
end 
else
begin
	set @sql = @sql + 'where ID < ' + convert(varchar,@id) + ' ORDER BY ID DESC'
end	 

exec(@sql)

