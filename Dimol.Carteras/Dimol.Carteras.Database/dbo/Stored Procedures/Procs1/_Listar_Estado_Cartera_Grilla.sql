-- =============================================              
-- Author:  Pablo Leyton              
-- Create date: 14-07-2014              
-- Description: Procedimiento para listar estados cartera para jQgrid              
-- =============================================           
    
CREATE PROCEDURE [dbo].[_Listar_Estado_Cartera_Grilla]              
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
  (   
   select * from ( Select       
    EC.ECT_CODEMP AS CODEMP,      
    EC.ECT_ESTID AS ID,      
    EC.ECT_NOMBRE AS NOMBRE,      
    EC.ECT_AGRUPA AS IDAGRUPA,  
    AGRUPA = (SELECT  Top 1 etiquetas_idiomas.eti_descripcion        
     FROM etiquetas,         
       etiquetas_idiomas        
       WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and        
       ( etiquetas_idiomas.eti_idid = ECI.ECI_IDID )  and    
       ( etiquetas.etq_codigo = ''AgrEst0'' + Convert(varchar,EC.ECT_AGRUPA))),       
    EC.ECT_UTILIZA AS UTILIZA,      
    EC.ECT_PREJUD AS PREJUD,      
    EC.ECT_SOLFECHA AS SOLFECHA,      
    EC.ECT_GENRET AS GENRET,      
    EC.ECT_COMPROMISO AS COMPROMISO    
 from estados_cartera EC, estados_cartera_idiomas ECI      
 where EC.ect_codemp = ECI.eci_codemp       
 and EC.ect_estid = ECI.eci_estid       
 and EC.ect_codemp = ' + CONVERT(VARCHAR,@codemp) +'       
 and ECI.eci_idid = ' + CONVERT(VARCHAR,@idid) +'       
) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                   
              
              
          
--select @query              
 exec(@query)               
               
              
END