-- =============================================                      
-- Author:  Pablo Leyton                      
-- Create date: 26-08-2014                      
-- Description: Procedimiento para listar Mandatos para jQgrid                      
-- =============================================                      
CREATE PROCEDURE [dbo].[_Listar_Mandatos_Grilla]                      
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
  (  SELECT       
  provcli.pcl_codemp as codemp,      
  provcli.pcl_pclid as IdCliente,         
  provcli_mandato.pcm_notid as IdNotaria,         
  provcli_mandato.pcm_numrep as NumeroRepertorio,         
  provcli.pcl_rut as RutCliente,         
  provcli.pcl_nomfant as NombreCliente,         
  notarias.not_nomnot as NombreNotaria,      
  pcm_fecasig as FechaAsignacion ,    
  --getdate() as FechaAsignacion ,  
  PCM_FECVENC as FechaVencimiento,  
  PCM_INDEFINIDO as Indefinido     
  FROM provcli,         
  provcli_mandato,         
  notarias      
 WHERE  provcli.pcl_codemp = provcli_mandato.pcm_codemp  and        
  provcli.pcl_pclid = provcli_mandato.pcm_pclid  and        
  provcli_mandato.pcm_codemp = notarias.not_codemp  and        
  provcli_mandato.pcm_notid = notarias.not_notid  and        
  provcli.pcl_codemp =  '  + CONVERT(VARCHAR,@codemp) +'            
 ) as tabla  ) as t                      
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                      
                      
if @where is not null                      
begin                      
set @query = @query + @where;                      
end                      
                      
--select @query                      
 exec(@query)                       
             
  
                      
END
