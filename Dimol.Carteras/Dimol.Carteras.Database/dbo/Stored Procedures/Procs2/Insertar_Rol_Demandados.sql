

Create Procedure Insertar_Rol_Demandados(@rld_codemp integer, @rld_rolid integer, @rld_rut varchar (20),
														@rld_nombre varchar (350), @rld_repleg char (1)) as  
  INSERT INTO rol_demandados  
         ( rld_codemp,   
           rld_rolid,   
           rld_rut,   
           rld_nombre,   
           rld_repleg )  
  VALUES ( @rld_codemp,   
           @rld_rolid,   
           @rld_rut,   
           @rld_nombre,   
           @rld_repleg )
