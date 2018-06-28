-- =============================================                                                
-- Author:  Pablo Leyton                                                
-- Create date: 29-09-2014                                                
-- Description: Procedimiento para listar Monedas para jQgrid                                                
-- =============================================                    
                  
                                              
CREATE PROCEDURE [dbo].[_Listar_Monedas_Grilla]                                                
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
  MON_CODEMP as CodEmp,   
  MON_CODMON as Id,   
  MON_NOMBRE as Nombre,   
  MON_SIMBOLO as Simbolo,   
  MON_DEFAULT as MonedaDefault,  
  MON_PORCINT as Porcentaje,  
  MON_DECIMALES as Decimales  
 FROM monedas    
 WHERE MON_CODEMP='  + CONVERT(VARCHAR,@codemp) +'     
 ) as tabla  ) as t                                                
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                                
                                                
if @where is not null                                                
begin                                                
set @query = @query + @where;                                                
end                                                
                                                
--select @query                                                
 exec(@query)                                                 
                                       
                            
                                                
END
