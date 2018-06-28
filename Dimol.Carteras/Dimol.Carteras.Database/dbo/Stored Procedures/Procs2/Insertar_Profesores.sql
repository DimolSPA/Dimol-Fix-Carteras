

Create Procedure Insertar_Profesores(@prf_codemp integer, @prf_prfid integer, @prf_pclid integer, @prf_emplid integer) as
  INSERT INTO profesores  
         ( prf_codemp,   
           prf_prfid,   
           prf_pclid,   
           prf_emplid )  
  VALUES ( @prf_codemp,   
           @prf_prfid,   
           @prf_pclid,   
           @prf_emplid )
