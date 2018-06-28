﻿-- =============================================                                          
-- Author:  Pablo Leyton                                          
-- Create date: 24-09-2014                                          
-- Description: Procedimiento para listar Paises para jQgrid                                          
-- =============================================              
            
                                        
CREATE PROCEDURE [dbo].[_Listar_Paises_Grilla]                                          
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
  PAI_PAIID as Id,    
  PAI_NOMBRE as Nombre,    
  PAI_CODTEL as Codigo    
 from pais         
 ) as tabla  ) as t                                          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                          
                                          
if @where is not null                                          
begin                                          
set @query = @query + @where;                                          
end                                          
                                          
--select @query                                          
 exec(@query)                                           
                                 
                      
                                          
END