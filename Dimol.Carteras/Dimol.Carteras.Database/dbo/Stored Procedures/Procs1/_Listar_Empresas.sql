-- =============================================                              
-- Author:  Pablo Leyton                              
-- Create date: 27-10-2014                              
-- Description: Procedimiento para listar Empresa para jQgrid                              
-- =============================================                           
    
CREATE PROCEDURE [dbo].[_Listar_Empresas]                              
(                              
     @codemp int                              
 )                              
AS                              
BEGIN                              
 SET NOCOUNT ON;                              
                              
declare @query varchar(7000);                              
 set @query = '  select * from                              
  (select * from                                  
  (    
  select   
  emp_codemp as CodEmp,  
  emp_rut as Rut,   
  emp_nombre as Nombre,   
  emp_rutrepleg as RutRepresentanteLegal,   
  emp_replegal as NombreRepresentanteLegal,   
  emp_giro as Giro ,   
  emp_logo as Logo  
  from empresa   
  where emp_codemp = ' + CONVERT(VARCHAR,@codemp) + '                       
                 
 ) as tabla  ) as t  '                           
                             
                              
                          
                              
                 
--select @query                              
 exec(@query)                               
                               
                              
END
