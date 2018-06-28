-- =============================================          
-- Author:  Pablo Leyton          
-- Create date: 04-06-2014          
-- Description: Procedimiento para listar TipoRetiroEntrega para jQgrid          
-- =============================================          
CREATE PROCEDURE [dbo].[_Listar_Tipo_Retiro_Entrega_Grilla_Count]          
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
  (   SELECT   distinct  COUNT (TRE_TREID) as total   
  FROM tipos_retiro_entrega 
   WHERE  TRE_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'               
 ) as tabla  ) as t          
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
          
if @where is not null          
begin          
set @query = @query + @where;          
end          
          
--select @query          
 exec(@query)           
            
END
