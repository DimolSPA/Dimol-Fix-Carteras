

Create Procedure Find_Profesores(@prf_codemp integer, @prf_pclid integer, @prf_emplid integer) as
  SELECT count(prf_codemp)  
    FROM profesores  
   WHERE ( prf_codemp = @prf_codemp ) AND  
         ( prf_pclid = @prf_pclid ) OR  
         ( prf_emplid = @prf_emplid )
