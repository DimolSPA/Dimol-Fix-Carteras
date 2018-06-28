

Create Procedure Update_Centro_Costos(@ccs_codemp integer, @ccs_ccsid integer, @ccs_nombre varchar (200)) as  
  UPDATE centro_costos  
     SET ccs_nombre = @ccs_nombre
   WHERE ( centro_costos.ccs_codemp = @ccs_codemp ) AND  
         ( centro_costos.ccs_ccsid = @ccs_ccsid )
