-- =============================================                
-- Author:  Pablo Leyton                
-- Create date: 25-08-2014                
-- Description: Procedimiento para listar tipos causa para jQgrid                
-- =============================================                
CREATE PROCEDURE [dbo].[_Listar_Tipos_Tribunales_Grilla]                
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
  (   SELECT             
     TTB_CODEMP AS CODEMP ,      
     TTB_TTBID AS ID,      
     TTB_NOMBRE AS NOMBRE    
   from tipos_tribunal          
   WHERE  TTB_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'                 
 ) as tabla  ) as t                
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                
                
if @where is not null                
begin                
set @query = @query + @where;                
end                
                
--select @query                
 exec(@query)                 
                 
                
END
