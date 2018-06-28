CREATE Procedure [dbo].[_Trae_TipoTribunal]            
(            
  @codemp int,
  @idid int                        
  )            
           
as              
               
          
BEGIN            
 select tbi_ttbid, tbi_nombre 
 from  tipos_tribunal_idiomas 
 where tbi_codemp = @codemp   
 and TBI_IDID = @idid 
 ORDER BY tbi_nombre ASC     
END
