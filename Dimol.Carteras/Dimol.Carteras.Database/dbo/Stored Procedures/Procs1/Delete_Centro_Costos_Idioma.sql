

Create Procedure Delete_Centro_Costos_Idioma(@csi_codemp integer, @csi_ccsid integer, @csi_idid integer) as 
  DELETE FROM centro_costos_idiomas  
   WHERE ( centro_costos_idiomas.csi_codemp = @csi_codemp ) AND  
         ( centro_costos_idiomas.csi_ccsid = @csi_ccsid ) AND  
         ( centro_costos_idiomas.csi_idid = @csi_idid )
