

Create Procedure Update_Profesores(@prf_codemp integer, @prf_prfid integer, @prf_pclid integer, @prf_emplid integer) as
  UPDATE profesores  
     SET prf_pclid = @prf_pclid,   
         prf_emplid = @prf_emplid  
   WHERE ( profesores.prf_codemp = @prf_codemp ) AND  
         ( profesores.prf_prfid = @prf_prfid )
