

Create Procedure Delete_Configuracion_Sistema(@cfs_cfsid integer) as 
  DELETE FROM configuracion_sistema  
   WHERE configuracion_sistema.cfs_cfsid = @cfs_cfsid
