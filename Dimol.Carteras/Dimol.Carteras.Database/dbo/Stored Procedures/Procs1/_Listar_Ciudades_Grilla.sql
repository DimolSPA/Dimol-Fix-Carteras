-- =============================================                                            
-- Author:  Pablo Leyton                                            
-- Create date: 25-09-2014                                            
-- Description: Procedimiento para listar Ciudades para jQgrid                                            
-- =============================================                
              
                                          
CREATE PROCEDURE [dbo].[_Listar_Ciudades_Grilla]                                            
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
  ( SELECT   
 region.reg_paiid as IdPais,    
 pai_nombre as NombrePais,       
 ciudad.ciu_regid as IdRegion,       
 region.reg_nombre as NombreRegion,  
 ciudad.ciu_ciuid as IdCiudad,          
 ciudad.ciu_nombre as NombreCiudad,                     
 ciudad.ciu_codarea as CodigoArea       
FROM ciudad,region,pais    
 WHERE  region.reg_regid = ciudad.ciu_regid    
 and  pais.pai_paiid = region.reg_paiid    
 ) as tabla  ) as t                                            
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                            
                                            
if @where is not null                                            
begin                                            
set @query = @query + @where;                                            
end                                            
                                            
--select @query                                            
 exec(@query)                                             
                                   
                        
                                            
END
