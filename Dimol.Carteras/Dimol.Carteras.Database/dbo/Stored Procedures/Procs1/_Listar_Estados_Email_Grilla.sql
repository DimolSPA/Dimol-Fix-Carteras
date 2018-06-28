-- =============================================                        
-- Author:  Pablo Leyton                        
-- Create date: 05-08-2014                        
-- Description: Procedimiento para listar Gestor para jQgrid                        
-- =============================================                     
              
CREATE PROCEDURE [dbo].[_Listar_Estados_Email_Grilla]                        
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
SELECT estados_cartera.ect_estid Id,   
estados_cartera_idiomas.eci_nombre Nombre
FROM estados_cartera,   
estados_cartera_idiomas with (nolock)
WHERE  estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp  and  
estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid  and  
estados_cartera.ect_codemp =' + CONVERT(VARCHAR,@codemp) +'  AND  
estados_cartera_idiomas.eci_idid = '+ CONVERT(VARCHAR,@idid ) +'  AND  
estados_cartera.ect_prejud in(''A'',''P'')                  
              
 ) as tabla  ) as t                        
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                        
                        
if @where is not null                        
begin                        
set @query = @query + @where;                        
end                        
                        
           
--select @query                        
 exec(@query)                         
                         
                        
END 