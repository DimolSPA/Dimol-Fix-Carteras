-- =============================================                                  
-- Author:  Pablo Leyton                                                
-- Create date: 07-10-2014                                                
-- Description: Procedimiento para Cantidad Bancos para jQgrid                                  
-- =============================================                       
                    
                               
CREATE PROCEDURE [dbo].[_Listar_Bancos_Grilla_Count]                                        
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
    ( SELECT    COUNT(BCO_BCOID) as Count                      
	  from bancos   
	 WHERE BCO_CODEMP='  + CONVERT(VARCHAR,@codemp) +'                      
  ) as tabla  ) as t                                  
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                  
                                  
if @where is not null                                  
begin                                  
set @query = @query + @where;                                  
end                                  
                                  
--select @query                                  
 exec(@query)                                   
                                   
                                  
END
