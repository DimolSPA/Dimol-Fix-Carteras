create Procedure [dbo].[_Trae_Bancos]            
(            
  @codemp int
 )            
           
as              
               
          
BEGIN            
select bco_bcoid, bco_nombre 
from bancos 
where bco_codemp = @codemp
order by bco_nombre  
END
