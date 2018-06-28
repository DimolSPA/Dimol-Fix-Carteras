-- =============================================                                  
-- Author:  Pablo Leyton                                                
-- Create date: 26-09-2014                                                
-- Description: Procedimiento para cantidad comunas para jQgrid                                  
-- =============================================                       
                    
                               
CREATE PROCEDURE [dbo].[_Listar_Comunas_Grilla_Count]                                        
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
                          
set @query = '  select count(*) count from                                  
    (select *,ROW_NUMBER() OVER (ORDER BY IdComuna asc )  as row from                                       
    ( SELECT     
  pais.pai_paiid as IdPais,       
  pais.pai_nombre as NombrePais,       
  region.reg_regid as IdRegion,       
  region.reg_nombre as NombreRegion,       
  comuna.com_ciuid as IdCiudad,       
  ciudad.ciu_nombre as NombreCiudad,       
  comuna.com_comid as IdComuna,       
  comuna.com_nombre as NombreComuna,       
  comuna.com_codpost as CodigoPostal    
 FROM comuna,       
 ciudad,       
 region,       
 pais    
 WHERE ( ciudad.ciu_ciuid = comuna.com_ciuid ) and      
 ( region.reg_regid = ciudad.ciu_regid ) and      
 ( pais.pai_paiid = region.reg_paiid )                               
  ) as tabla  ) as t                                  
  where  row > 0'                          
                                  
if @where is not null                                  
begin                                  
set @query = @query + @where;                                  
end                                  
                                  
--select @query                                  
 exec(@query)                                   
                                   
                                  
END
