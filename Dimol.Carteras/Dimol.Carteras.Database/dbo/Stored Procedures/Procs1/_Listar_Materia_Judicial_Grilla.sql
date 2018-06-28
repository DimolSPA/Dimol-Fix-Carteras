-- =============================================            
-- Author:  Pablo Leyton            
-- Create date: 20-08-2014            
-- Description: Procedimiento para listar Materia Judicial para jQgrid            
-- =============================================            
CREATE PROCEDURE [dbo].[_Listar_Materia_Judicial_Grilla]            
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
   ESJ_CODEMP AS CODEMP ,  
   ESJ_ESJID AS ID,  
   ESJ_NOMBRE AS NOMBRE,  
   ESJ_ORDEN AS ORDEN   
  from materia_judicial      
   WHERE  ESJ_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'             
 ) as tabla  ) as t            
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)            
            
if @where is not null            
begin            
set @query = @query + @where;            
end            
            
--select @query            
 exec(@query)             
             
            
END
