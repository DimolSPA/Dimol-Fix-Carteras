CREATE PROC [dbo].[_Listar_Codigo_Carga_Grilla]      
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
      
declare @query varchar(7000);          
            
set @query = '  select * from          
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from              
  (   
   select * from (
   SELECT       
   provcli_codigo_carga.PCC_CODEMP AS CodEmp,      
   provcli.pcl_pclid As Pclid,        
   provcli_codigo_carga.pcc_codid As correlativo,         
   provcli.pcl_nomfant AS NombreCliente,       
   provcli_codigo_carga.pcc_codigo AS Codigo,        
   provcli_codigo_carga.pcc_nombre As Nombre      
 FROM provcli, provcli_codigo_carga      
 WHERE  provcli_codigo_carga.pcc_codemp = provcli.pcl_codemp        
 and  provcli_codigo_carga.pcc_pclid = provcli.pcl_pclid      
 and  provcli_codigo_carga.PCC_CODEMP='  + CONVERT(VARCHAR,@codemp) +'
  ) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
          
        
          
--select @query          
exec(@query) 