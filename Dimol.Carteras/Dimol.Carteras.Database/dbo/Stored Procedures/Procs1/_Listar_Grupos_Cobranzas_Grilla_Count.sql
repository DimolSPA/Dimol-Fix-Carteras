-- =============================================          
-- Author:  Pablo Leyton          
-- Create date: 15-06-2014          
-- Description: Procedimiento para listar cantidad Grupos Cobranzas para jQgrid          
-- =============================================          
CREATE PROCEDURE [dbo].[_Listar_Grupos_Cobranzas_Grilla_Count]          
(          
    @codemp int,          
    @idid int,          
    @where varchar(1000),          
    @sidx varchar(255),          
    @sord varchar(10),          
    @inicio int,          
    @limite int,  
    @idsuc int          
)          
AS          
BEGIN          
  SET NOCOUNT ON;          
           
 declare @query varchar(7000);          
             
 set @query = '  select * from      
  (select *,ROW_NUMBER() OVER ( ORDER BY count asc ) as row from              
   (   SELECT DISTINCT COUNT (GRC_GRCID) as count   
  FROM grupos_cobranza,       
   empleados    
  WHERE  empleados.epl_codemp = grupos_cobranza.grc_codemp    
  and empleados.epl_emplid = grupos_cobranza.grc_emplid    
  and grupos_cobranza.grc_codemp =' + CONVERT(VARCHAR,@codemp) +'    
  and grupos_cobranza.grc_sucid =' + CONVERT(VARCHAR,@idsuc) +'    
          
  ) as tabla  ) as t          
   where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
    
 if @where is not null          
 begin          
 set @query = @query + @where;          
 end          
       
 --select @query          
  exec(@query)           
      
END
