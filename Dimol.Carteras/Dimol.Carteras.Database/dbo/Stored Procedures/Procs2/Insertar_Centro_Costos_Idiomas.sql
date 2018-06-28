

 Create Procedure Insertar_Centro_Costos_Idiomas(@csi_codemp integer, @csi_ccsid integer, @csi_idid integer, @csi_nombre varchar (250)) as
  INSERT INTO centro_costos_idiomas  
         ( csi_codemp,   
           csi_ccsid,   
           csi_idid,   
           csi_nombre )  
  VALUES ( @csi_codemp,   
           @csi_ccsid,   
           @csi_idid,   
           @csi_nombre )
