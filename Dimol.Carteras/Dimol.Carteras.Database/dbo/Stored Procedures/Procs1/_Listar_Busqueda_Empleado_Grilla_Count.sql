-- =============================================                                            
-- Author:  Pablo Leyton                                                          
-- Create date: 23-10-2014                                                          
-- Description: Procedimiento para Cantidad Busqueda_Empleado para jQgrid                                            
-- =============================================                                 
                              
                                         
CREATE PROCEDURE [dbo].[_Listar_Busqueda_Empleado_Grilla_Count]                                                  
(                                            
  @codemp int,                                            
  @idid int,                                            
  @where varchar(1000),                                            
  @sidx varchar(255),                                            
  @sord varchar(10),                                            
  @inicio int,                                            
  @limite int,      
  @nombre varchar(400) ,        
  @paterno varchar(100) ,        
  @materno varchar(100),        
  @rut varchar(20),           
  @estado varchar(1)                                             
)                                            
AS                                            
BEGIN                                            
 SET NOCOUNT ON;                                            
                                
declare @query varchar(7000);                                            

set @query = '   
 ( select * from                                            
    (select *,ROW_NUMBER() OVER (ORDER BY Count asc )  as row from                                                 
    ( SELECT    COUNT(epl_emplid) as Count                                
  from empleados         
  WHERE EPL_CODEMP= ' + CONVERT(VARCHAR,@codemp)                              
       
 if @nombre is not null        
begin        
set @query = @query + ' and epl_nombre like ''%'+ @nombre + '%'''        
end        
if @paterno is not null        
begin        
set @query = @query + ' and epl_apepat like ''%'+ @paterno + '%'''        
end         
if @materno is not null        
begin        
set @query = @query + ' and epl_apemat like ''%'+ @materno + '%'''        
end                  
if @rut is not null        
begin        
set @query = @query + ' and epl_rut like ''%'+ @rut + '%'''        
end         
if @estado is not null        
begin        
set @query = @query + ' and epl_eemid = '+ @estado + ''        
end         
       
set @query = @query + '  ) as tabla  ) as t                                            
where  row >= 0 )'                                         
 

                                            
if @where is not null                                            
begin                                            
set @query = @query + @where;                                            
end                                            
                                            
--select @query                                            
 exec(@query)                                             
                                             
                                            
END
