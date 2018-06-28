

Create Procedure Delete_Centro_Costos(@ccs_codemp integer, @ccs_ccsid integer) as 
  DELETE FROM centro_costos_idiomas  
   WHERE ( centro_costos_idiomas.csi_codemp = @ccs_codemp ) AND  
         ( centro_costos_idiomas.csi_ccsid = @ccs_ccsid )   

  DELETE FROM centro_costos  
   WHERE ( centro_costos.ccs_codemp = @ccs_codemp ) AND  
         ( centro_costos.ccs_ccsid = @ccs_ccsid )
