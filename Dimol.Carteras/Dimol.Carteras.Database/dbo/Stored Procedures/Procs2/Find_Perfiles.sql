

Create Procedure Find_Perfiles(@prf_codemp integer, @prf_prfid integer) as
  SELECT count(perfiles.prf_prfid  )
    FROM perfiles  
   WHERE ( perfiles.prf_codemp = @prf_codemp ) AND  
         ( perfiles.prf_prfid = @prf_prfid )
