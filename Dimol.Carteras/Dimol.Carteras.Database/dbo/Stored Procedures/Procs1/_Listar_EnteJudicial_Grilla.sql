-- =============================================                                  
-- Author:  Pablo Leyton                                  
-- Create date: 05-09-2014                                  
-- Description: Procedimiento para listar ente Judicial para jQgrid                                  
-- =============================================      
    
                                
CREATE PROCEDURE [dbo].[_Listar_EnteJudicial_Grilla]                                  
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
  entes_judicial.ETJ_CODEMP as codemp ,    
  entes_judicial.etj_etjid as Id,       
  provcli.PCL_RUT as Rut,    
  entes_judicial.etj_pclid as IdCliente,       
  0 as IdEmpleado,       
  entes_judicial.etj_sindico as Sindico ,       
  entes_judicial.etj_abogado as Abogado ,       
  entes_judicial.etj_procurador as Procurador ,       
  entes_judicial.etj_receptor as Receptor ,       
  provcli.pcl_nomfant as Nombre,    
  ''''  as NombreEmpleado    
  FROM entes_judicial,       
  provcli    
  WHERE  provcli.pcl_codemp = entes_judicial.etj_codemp  and      
  provcli.pcl_pclid = entes_judicial.etj_pclid  and      
  entes_judicial.etj_codemp =  ' + CONVERT(VARCHAR,@codemp) +'           
 UNION    
 SELECT     
  entes_judicial.ETJ_CODEMP ,    
  entes_judicial.etj_etjid,       
  empleados.EPL_RUT,    
  0,       
  entes_judicial.etj_emplid,       
  entes_judicial.etj_sindico ,       
  entes_judicial.etj_abogado  ,       
  entes_judicial.etj_procurador ,       
  entes_judicial.etj_receptor  ,       
  epl_nombre + ''  '' + epl_apepat ,    
  empleados.epl_nombre + '' '' + empleados.epl_apepat as nombre     
  FROM entes_judicial,       
  empleados    
  WHERE  empleados.epl_codemp = entes_judicial.etj_codemp  and      
  empleados.epl_emplid = entes_judicial.etj_emplid and     
  entes_judicial.etj_codemp =  ' + CONVERT(VARCHAR,@codemp) +'           
     
                    
 ) as tabla  ) as t                                  
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                  
                                  
if @where is not null                                  
begin                                  
set @query = @query + @where;                                  
end                                  
                                  
--select @query                                  
 exec(@query)                                   
                         
              
                                  
END
