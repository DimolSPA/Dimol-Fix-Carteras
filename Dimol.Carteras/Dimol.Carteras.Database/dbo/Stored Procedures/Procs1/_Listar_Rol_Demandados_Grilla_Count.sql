CREATE PROC [dbo].[_Listar_Rol_Demandados_Grilla_Count]      
      (          
       @codemp int,           
       @rolid int,     
       @where varchar(1000),          
       @sidx varchar(255),          
       @sord varchar(10),          
       @inicio int,          
       @limite int          
      )          
      
AS      
      
declare @query varchar(7000);          
            
set @query = '  select count(*) count from          
  (select *,ROW_NUMBER() OVER (ORDER BY Rut desc) as row from              
  (   
   select * from (
	SELECT rld_rut Rut,   
	rld_nombre Nombre,   
	case rld_repleg when ''S'' then ''ON'' else ''OFF'' end as RepresentanteLegal
	FROM rol_demandados
	WHERE  rld_codemp =  '  + CONVERT(VARCHAR,@codemp) +'
	and rld_rolid = '  + CONVERT(VARCHAR,@rolid) +'
  ) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > 0'    
          
        
          
--select @query          
exec(@query) 