-- =============================================    
-- Author:     Pablo Leyton    
-- Create date: 02/06/2014    
-- Description:   Lista clientes    
-- =============================================    
CREATE PROCEDURE [dbo].[_Listar_Nombre_Cliente_Todos]     
   @codemp int        
AS    
BEGIN    
       -- SET NOCOUNT ON added to prevent extra result sets from    
       -- interfering with SELECT statements.    
       SET NOCOUNT ON;    
  SELECT     
 PCL_NOMFANT AS Nombre,  
 PCL_PCLID AS Id    
     
     
  FROM PROVCLI     
  WHERE PCL_CODEMP = @codemp  
  order by   PCL_NOMFANT
      
END
