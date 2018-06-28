-- =============================================                              
-- Author:  Pablo Leyton                              
-- Create date: 27-10-2014                              
-- Description: Procedimiento para listar empresas Configuracion para jQgrid                              
-- =============================================                           
    
create PROCEDURE [dbo].[_Listar_Empresa_Configuracion_Grilla]                              
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
 select   
 emc_codemp as CodEmp,  
 emc_emcid as Id,   
 emc_nombre as Nombre,   
 emc_valnum as ValorNumerico,   
 emc_valtxt as ValorTexto   
from empresa_configuracion   
where emc_codemp  = ' + CONVERT(VARCHAR,@codemp) + '                       
                                       
                    
 ) as tabla  ) as t                              
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                              
                              
if @where is not null                              
begin                              
set @query = @query + @where;                              
end                              
                              
                 
--select @query                              
 exec(@query)                               
                               
                              
END
