CREATE Procedure [dbo].[_Trae_Clientes]        
(        
  @codemp int                    
  )        
       
as          
           
      
BEGIN        
 SELECT 
	PCL_PCLID AS IdCliente,
	PCL_NOMBRE AS NombreCliente
 FROM provcli  
 WHERE PCL_CODEMP = @codemp
 ORDER BY PCL_NOMBRE ASC 
END
