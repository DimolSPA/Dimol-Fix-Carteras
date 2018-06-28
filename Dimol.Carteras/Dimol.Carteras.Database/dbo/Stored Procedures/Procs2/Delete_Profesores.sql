

Create Procedure Delete_Profesores(@prf_codemp integer, @prf_prfid integer) as
  DELETE FROM profesores  
   WHERE ( profesores.prf_codemp = @prf_codemp ) AND  
         ( profesores.prf_prfid = @prf_prfid )
