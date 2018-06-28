-- =============================================                                      
-- Author:  Pablo Leyton                                      
-- Create date: 23-09-2014                                      
-- Description: Procedimiento para listar Idiomas para jQgrid                                      
-- =============================================          
        
                                    
CREATE PROCEDURE [dbo].[_Listar_Idiomas_Grilla]                                      
(                                      
    @codemp int,                                      
    @idid int,                                      
    @where varchar(1000),                                      
    @sidx varchar(255),                                      
    @sord varchar(10),                                      
    @inicio int,                                      
    @limite int                                      
)                                      
AS                                      
BEGIN                                      
 SET NOCOUNT ON;                                      
                                
declare @query varchar(7000);                                      
                          
                                    
set @query = '  select * from                                      
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from                                          
  (  select     
  IDI_IDID as Id,    
  IDI_NOMBRE as Nombre,    
  IDI_IDISRC as Recurso    
 from idiomas     
 ) as tabla  ) as t                                      
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                      
                                      
if @where is not null                                      
begin                                      
set @query = @query + @where;                                      
end                                      
                                      
--select @query                                      
 exec(@query)                                       
                             
                  
                                      
END
