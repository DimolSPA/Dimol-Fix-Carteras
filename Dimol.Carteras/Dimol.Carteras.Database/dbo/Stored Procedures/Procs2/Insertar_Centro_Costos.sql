

 Create Procedure Insertar_Centro_Costos(@ccs_codemp integer, @ccs_ccsid integer, @ccs_nombre varchar (200)) as
   INSERT INTO centro_costos  
         ( ccs_codemp,   
           ccs_ccsid,   
           ccs_nombre )  
  VALUES ( @ccs_codemp,   
           @ccs_ccsid,   
           @ccs_nombre )
