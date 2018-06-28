

Create Procedure Find_Centro_Costos(@ccs_codemp integer, @ccs_ccsid integer) as
  SELECT count(centro_costos.ccs_ccsid)  
    FROM centro_costos  
   WHERE ( centro_costos.ccs_codemp = @ccs_codemp ) AND  
         ( centro_costos.ccs_ccsid = @ccs_ccsid )
