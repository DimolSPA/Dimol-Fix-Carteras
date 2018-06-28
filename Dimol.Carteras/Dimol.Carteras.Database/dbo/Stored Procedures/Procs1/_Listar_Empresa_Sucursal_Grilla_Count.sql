-- =============================================                                            
-- Author:  Pablo Leyton                                                          
-- Create date: 06-10-2014                                                          
-- Description: Procedimiento para Cantidad empresa Sucursales para jQgrid                                            
-- =============================================                                 
                              
                                         
CREATE PROCEDURE [dbo].[_Listar_Empresa_Sucursal_Grilla_Count]                                                  
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
    (select *,ROW_NUMBER() OVER (ORDER BY Count asc )  as row from                                                 
    ( SELECT    COUNT(esu_sucid) as Count                                
 from empresa_sucursal             
 where esu_codemp  = ' + CONVERT(VARCHAR,@codemp) + '                              
  ) as tabla  ) as t                                            
  where  row >=0 '                                            
                                            
if @where is not null                                            
begin                                            
set @query = @query + @where;                                            
end                                            
                                            
--select @query                                            
 exec(@query)                                             
                                             
                                            
END
