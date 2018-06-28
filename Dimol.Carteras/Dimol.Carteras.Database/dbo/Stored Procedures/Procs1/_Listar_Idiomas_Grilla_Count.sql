﻿-- =============================================                            
-- Author:  Pablo Leyton                                          
-- Create date: 23-09-2014                                          
-- Description: Procedimiento para cantidad listar Idiomas para jQgrid                            
-- =============================================                 
              
                         
CREATE PROCEDURE [dbo].[_Listar_Idiomas_Grilla_Count]                                  
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
    ( SELECT    COUNT (IDI_IDID) as Count                
  from idiomas                          
  ) as tabla  ) as t                            
  where  row >= 0 '                            
                            
if @where is not null                            
begin                            
set @query = @query + @where;                            
end                            
                            
--select @query                            
 exec(@query)                             
                             
                            
END
