

Create Procedure Update_Centro_Costos_Idiomas(@csi_codemp integer, @csi_ccsid integer, @csi_idid integer, @csi_nombre varchar (250)) as  
  UPDATE centro_costos_idiomas  
     SET csi_nombre = @csi_nombre
   WHERE ( centro_costos_idiomas.csi_codemp = @csi_codemp ) AND  
         ( centro_costos_idiomas.csi_ccsid = @csi_ccsid ) AND  
         ( centro_costos_idiomas.csi_idid = @csi_idid )
