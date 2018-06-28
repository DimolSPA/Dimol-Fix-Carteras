

Create Procedure Find_Tipos_Contrato(@tic_codemp integer, @tic_ticid integer) as
  SELECT count(tipos_contrato.tic_ticid)  
    FROM tipos_contrato  
   WHERE ( tipos_contrato.tic_codemp = @tic_codemp ) AND  
         ( tipos_contrato.tic_ticid = @tic_ticid )
