-- =============================================                                
-- Author:  Pablo Leyton                                              
-- Create date: 25-09-2014                                              
-- Description: Procedimiento para cantidad Ciudades para jQgrid                                
-- =============================================                     
                  
                             
CREATE PROCEDURE [dbo].[_Listar_Ciudades_Grilla_Count]                                      
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
    (select *,ROW_NUMBER() OVER (ORDER BY Count asc )  as row from                                     
    ( SELECT    COUNT(ciu_ciuid) as Count                    
   FROM ciudad,region,pais        
  WHERE  region.reg_regid = ciudad.ciu_regid        
  and  pais.pai_paiid = region.reg_paiid                       
  ) as tabla  ) as t                                
  where  row >= 0 '                                 
                                
if @where is not null                                
begin                                
set @query = @query + @where;                                
end                                
                                
--select @query                                
 exec(@query)                                 
                                 
                                
END
