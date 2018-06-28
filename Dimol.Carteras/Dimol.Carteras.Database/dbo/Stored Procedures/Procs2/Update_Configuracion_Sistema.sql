

Create  Procedure Update_Configuracion_Sistema(@cfs_cfsid integer, @cfs_nombre varchar (400), @cfs_valnum numeric (30,6), @cfs_valtxt varchar (1000)) as  
  UPDATE configuracion_sistema  
     SET cfs_nombre = @cfs_nombre,   
         cfs_valnum = @cfs_valnum,   
         cfs_valtxt = @cfs_valtxt  
   WHERE configuracion_sistema.cfs_cfsid = @cfs_cfsid
