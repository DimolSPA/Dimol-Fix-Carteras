CREATE Procedure [dbo].[_Delete_Tipos_Retiro_Entrega]
			(@codemp integer, 
			 @id integer) 
as  
  
  DELETE FROM tipos_retiro_entrega_idiomas    
   WHERE ( tri_codemp = @codemp ) AND    
         ( tri_treid = @id )   
  
  
  DELETE FROM tipos_retiro_entrega    
   WHERE ( tre_codemp = @codemp ) AND    
         ( tre_treid = @id )
