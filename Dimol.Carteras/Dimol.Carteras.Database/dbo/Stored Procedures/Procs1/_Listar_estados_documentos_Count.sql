-- =============================================                                    
-- Author:  Pablo Leyton                                                  
-- Create date: 07-10-2014                                                  
-- Description: Procedimiento para Cantidad estados_documentos para jQgrid                                    
-- =============================================                         
                      
                                 
CREATE PROCEDURE [dbo].[_Listar_estados_documentos_Count]                                          
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
    ( SELECT    COUNT(EDC_EDCID) as Count                        
	from estados_documentos_diarios        
   where edc_codemp = ' + CONVERT(VARCHAR,@codemp) + '                      
  ) as tabla  ) as t                                    
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                    
                                    
if @where is not null                                    
begin                                    
set @query = @query + @where;                                    
end                                    
                                    
--select @query                                    
 exec(@query)                                     
                                     
                                    
END
