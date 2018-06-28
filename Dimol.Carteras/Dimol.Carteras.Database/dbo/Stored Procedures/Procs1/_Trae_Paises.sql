CREATE Procedure [dbo].[_Trae_Paises]          
        
as            
             
        
BEGIN      
   
 Select pai_paiid, pai_nombre 
 from pais 
 order by pai_nombre
END
