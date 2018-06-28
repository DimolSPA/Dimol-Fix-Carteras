-- =============================================              
-- Author:  Pablo Leyton              
-- Create date: 20-08-2014              
-- Description: Procedimiento para listar Materia Judicial para jQgrid              
-- =============================================              
CREATE PROCEDURE [dbo].[_Listar_Materia_Judicial_Grilla_Count]              
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
  (   SELECT   distinct  COUNT (ESJ_ESJID) as Count             
  from materia_judicial        
   WHERE  ESJ_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'               
 ) as tabla  ) as t              
  where  row >=0 '           
              
if @where is not null              
begin              
set @query = @query + @where;              
end              
              
--select @query              
 exec(@query)               
               
              
END
