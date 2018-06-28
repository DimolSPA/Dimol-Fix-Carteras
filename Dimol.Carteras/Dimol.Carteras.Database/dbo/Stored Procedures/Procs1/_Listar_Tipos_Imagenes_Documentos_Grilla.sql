-- =============================================          
-- Author:  Pablo Leyton          
-- Create date: 10-06-2014          
-- Description: Procedimiento para listar tipos imagenes documentos para jQgrid          
-- =============================================          
CREATE PROCEDURE [dbo].[_Listar_Tipos_Imagenes_Documentos_Grilla]          
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
  (   SELECT distinct [TPC_CODEMP] Codemp,        
     TPC_NOMBRE Nombre,        
     TPC_TPCID Id         
    FROM [dbo].[Tipos_Imagenes_CpbtDoc]   
    WHERE  TPC_CODEMP='  + CONVERT(VARCHAR,@codemp) +'            
 ) as tabla  ) as t          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
          
if @where is not null          
begin          
set @query = @query + @where;          
end          
    
--select @query          
 exec(@query)           
           
          
END
