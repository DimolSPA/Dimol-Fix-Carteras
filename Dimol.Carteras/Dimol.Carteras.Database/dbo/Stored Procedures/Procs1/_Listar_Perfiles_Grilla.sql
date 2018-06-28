-- =============================================                                                  
-- Author:  Pablo Leyton                                                  
-- Create date: 30-09-2014                                                  
-- Description: Procedimiento para listar Perfiles para jQgrid                                                  
-- =============================================                      
                    
                                                
CREATE PROCEDURE [dbo].[_Listar_Perfiles_Grilla]                                                  
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
  ( select   
   prf_codemp as CodEmp,   
   prf_prfid as Id,   
   prf_nombre as Nombre,   
   prf_administrador as Administrador  
 from perfiles  
 WHERE prf_codemp='  + CONVERT(VARCHAR,@codemp) +'       
 ) as tabla  ) as t                                                  
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                                  
                                                  
if @where is not null                                                  
begin                                                  
set @query = @query + @where;                                                  
end                                                  
                                                  
--select @query                                                  
 exec(@query)                                                   
                                         
                              
                                                  
END
