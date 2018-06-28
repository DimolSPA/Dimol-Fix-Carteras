

  Create Procedure Insertar_Configuracion_Sistema(@cfs_cfsid integer, @cfs_nombre varchar (400), @cfs_valnum numeric (30,6), @cfs_valtxt varchar (1000)) as
  INSERT INTO configuracion_sistema  
         ( cfs_cfsid,   
           cfs_nombre,   
           cfs_valnum,   
           cfs_valtxt )  
  VALUES ( @cfs_cfsid,   
           @cfs_nombre,   
           @cfs_valnum,   
           @cfs_valtxt )
