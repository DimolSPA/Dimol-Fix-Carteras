CREATE Procedure [dbo].[_Trae_Ciudades]     
 @idRegion  int =null       
            
as                
                 
            
BEGIN          
       
  select CIU_CIUID,CIU_NOMBRE from CIUDAD  
  where @idRegion is null or CIU_REGID=@idRegion  
  order by CIU_NOMBRE    
END
