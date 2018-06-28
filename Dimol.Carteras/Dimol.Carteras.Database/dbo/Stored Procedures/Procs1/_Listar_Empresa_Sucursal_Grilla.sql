-- =============================================                                  
-- Author:  Pablo Leyton                                  
-- Create date: 27-10-2014                                  
-- Description: Procedimiento para listar empresas Sucursales para jQgrid                                  
-- =============================================                               
        
CREATE PROCEDURE [dbo].[_Listar_Empresa_Sucursal_Grilla]                                  
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
 esu_codemp as CodEmp ,      
 esu_sucid as Id,       
 esu_nombre as Nombre,   
 esu_comid as IdComuna,  
 Comuna=(select top 1 COM_NOMBRE  from COMUNA where COM_COMID =empresa_sucursal.esu_comid), 
 esu_direccion as Direccion,  
 esu_telefono as Telefono,  
 esu_fax as Fax,  
 ESU_MAIL as Email,  
 esu_css as Css,        
 esu_matriz  as matriz        
 from empresa_sucursal       
 where esu_codemp  = ' + CONVERT(VARCHAR,@codemp) + '                           
                                           
                        
 ) as tabla  ) as t                                  
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                                  
                                  
if @where is not null                                  
begin                                  
set @query = @query + @where;                                  
end                                  
                                  
                     
--select @query                                  
 exec(@query)                                   
                                   
                                  
END
