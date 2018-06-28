CREATE Procedure [dbo].[_Trae_Notarias]          
(          
  @codemp int                      
  )          
         
as            
             
        
BEGIN          
  SELECT   
  notarias.not_notid as IdNotaria,     
     notarias.not_nomnot as NombreNotaria  
 FROM notarias  
 WHERE notarias.not_codemp = @codemp  
 ORDER BY notarias.not_nomnot ASC   
END
