-- =============================================                                    
-- Author:  Pablo Leyton                                    
-- Create date: 21-10-2014                                    
-- Description: Procedimiento para listar Busqueda empleado para jQgrid                                    
-- =============================================                                 
    
          
CREATE PROCEDURE [dbo].[_Listar_Busqueda_Empleado_Grilla]                                    
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
                                              
                                                        
set @query = '  select * from                                                          
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from                                                              
  ( select      
 EPL_CODEMP as CodEmp,    
 epl_emplid as Id,     
 epl_rut as Rut,     
 epl_nombre as Nombre,     
 epl_apepat as ApellidoPaterno,     
 epl_apemat as ApellidoMaterno,     
 epl_url_foto as Foto,    
 epl_eemid as Estado ,  
  DescripcionEstado=(select top 1 eem_nombre from estados_empleado   
  where eem_codemp=empleados.EPL_CODEMP and eem_eemid=empleados.epl_eemid)    
from empleados     
 WHERE EPL_CODEMP='  + CONVERT(VARCHAR,@codemp)         
     
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
set @query = @query + ' and epl_rut = ''%'+ @rut + '%'''    
end     
if @estado is not null    
begin    
set @query = @query + ' and epl_eemid = '+ @estado + ''    
end     
          
set @query = @query + ' ) as tabla  ) as t                                                          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                                          
                                                          
if @where is not null                                                          
begin                                                          
set @query = @query + @where;                                                          
end                                                          
                                                          
--select @query                                                          
 exec(@query)                                                           
                                                 
                                      
                                                          
END
