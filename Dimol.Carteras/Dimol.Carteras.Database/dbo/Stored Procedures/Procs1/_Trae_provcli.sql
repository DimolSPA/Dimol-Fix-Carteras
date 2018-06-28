CREATE Procedure [dbo].[_Trae_provcli]                
(                
  @codemp int    
  )                
               
as                  
                   
              
BEGIN                
  select '-1' as Id,UPPER(ETQ_DESCRIPCION) as Nombre
  from ETIQUETAS
  where  ETQ_CODIGO='Selec'
  union 
  select pcl_pclid,PCL_NOMBRE  
  from   provcli    
 where pcl_codemp = @codemp       
 ORDER BY 1 ASC         
END
