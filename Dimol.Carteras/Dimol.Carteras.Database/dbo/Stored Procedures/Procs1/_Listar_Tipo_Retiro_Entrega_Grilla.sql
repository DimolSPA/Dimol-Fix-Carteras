-- =============================================          
-- Author:  Pablo Leyton          
-- Create date: 03-06-2014          
-- Description: Procedimiento para listar TipoRetiroEntrega para jQgrid          
-- =============================================          
CREATE PROCEDURE [dbo].[_Listar_Tipo_Retiro_Entrega_Grilla]          
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
     TRE_TREID AS Id,       
     TRE_NOMBRE AS Nombre,      
     TRE_CODEMP AS Codemp      
  FROM tipos_retiro_entrega     
   WHERE  TRE_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'           
 ) as tabla  ) as t          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          

          
if @where is not null          
begin          
set @query = @query + @where;          
end          
          
--select @query          
 exec(@query)           
           
          
END
