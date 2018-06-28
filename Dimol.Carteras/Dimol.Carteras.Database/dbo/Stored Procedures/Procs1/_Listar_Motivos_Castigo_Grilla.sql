-- =============================================        
-- Author:  Pablo Leyton        
-- Create date: 09-06-2014        
-- Description: Procedimiento para listar Motivo Castigo para jQgrid        
-- =============================================        
CREATE PROCEDURE [dbo].[_Listar_Motivos_Castigo_Grilla]        
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
  (   SELECT distinct [TMC_TMCID] Codemp,      
    TMC_NOMBRE Nombre,      
    TMC_TMCID Id       
   FROM [dbo].[tipos_motivos_castigos]      
   WHERE  TMC_CODEMP='  + CONVERT(VARCHAR,@codemp) +'         
 ) as tabla  ) as t        
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)        
        
if @where is not null        
begin        
set @query = @query + @where;        
end        
        
        
    
--select @query        
 exec(@query)         
         
        
END
