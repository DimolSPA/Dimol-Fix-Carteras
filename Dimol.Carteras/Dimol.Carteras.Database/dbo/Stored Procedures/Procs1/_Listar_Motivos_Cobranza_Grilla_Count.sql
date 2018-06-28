-- =============================================      
-- Author:  Pablo Leyton      
-- Create date: 30-05-2014      
-- Description: Procedimiento para listar acciones para jQgrid      
-- =============================================      
CREATE PROCEDURE [dbo].[_Listar_Motivos_Cobranza_Grilla_Count]      
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
  (select *,ROW_NUMBER() OVER (ORDER BY total asc ) as row from          
  (    SELECT distinct COUNT (MTC_MTCID) as total  
  FROM [dbo].[motivo_cobranza]  
  WHERE  MTC_CODEMP='  + CONVERT(VARCHAR,@codemp) +'         
 ) as tabla  ) as t      
  where  row >0'     
      
if @where is not null      
begin      
set @query = @query + @where;      
end      
      
--select @query      
 exec(@query)       
      
END
