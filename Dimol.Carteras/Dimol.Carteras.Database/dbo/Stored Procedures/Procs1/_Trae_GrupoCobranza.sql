CREATE Procedure [dbo].[_Trae_GrupoCobranza]    
(    
  @codemp int,                  
  @sucursal int           
  )    
   
as      
       
  
BEGIN    
   SELECT 
		grc_grcid as Id, grc_nombre as Nombre     
    FROM grupos_cobranza        
     WHERE GRC_CODEMP = @codemp   and    
           GRC_SUCID =  @sucursal  
      ORDER BY grc_nombre
END
