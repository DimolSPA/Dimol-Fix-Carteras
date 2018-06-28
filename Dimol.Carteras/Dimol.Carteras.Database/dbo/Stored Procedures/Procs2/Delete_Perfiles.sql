

Create Procedure Delete_Perfiles(@prf_codemp integer, @prf_prfid integer) as  
 
DELETE FROM perfiles_idiomas  
   WHERE ( perfiles_idiomas.pfi_codemp = @prf_codemp ) AND  
         ( perfiles_idiomas.pfi_prfid = @prf_prfid ) 
         
DELETE FROM perfiles  
   WHERE ( perfiles.prf_codemp = @prf_codemp ) AND  
         ( perfiles.prf_prfid = @prf_prfid )
