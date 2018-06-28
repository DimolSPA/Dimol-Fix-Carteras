

Create Procedure Insertar_Help_Idiomas(@hpi_trvid integer, @hpi_hlpid integer, @hpi_idid integer, @hpi_nombre varchar(500), @hpi_archivo varchar(500)) as
 
  INSERT INTO help_idiomas  
         ( hpi_trvid,   
           hpi_hlpid,   
           hpi_idid,   
           hpi_nombre,   
           hpi_archivo )  
  VALUES ( @hpi_trvid,   
           @hpi_hlpid,   
           @hpi_idid,   
           @hpi_nombre,   
           @hpi_archivo )
