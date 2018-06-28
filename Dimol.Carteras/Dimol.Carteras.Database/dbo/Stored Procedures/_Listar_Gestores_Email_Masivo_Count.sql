CREATE PROCEDURE [dbo].[_Listar_Gestores_Email_Masivo_Count]
	@codemp int,
	 @tipo_cartera int,
	 @sucid int,               
     @sidx varchar(255),                
     @sord varchar(10),                
     @inicio int,                
     @limite int 
AS
	SET NOCOUNT ON;                
                
declare @query varchar(7000);                
                  
set @query = '  select count(*) count from                 
   (select *,ROW_NUMBER() OVER (ORDER BY ID asc) as row from                                                    
  ( SELECT gestor.ges_gesid Id,   
gestor.ges_nombre Nombre, ''false'' as sel ,*
FROM gestor
WHERE  gestor.ges_codemp = ' + CONVERT(VARCHAR,@codemp) +'
and gestor.ges_sucid =  ' + CONVERT(VARCHAR,@sucid) +'
and gestor.ges_estado = ''A''  
and gestor.ges_tipcart in (3, ' + CONVERT(VARCHAR,@tipo_cartera) +'  )'    

              
 set @query = @query + ') as tabla  ) as t                        
  where  row > 0'                            
   
--select @query                
 exec(@query)               
RETURN 0
