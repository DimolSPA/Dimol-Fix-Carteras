

Create Procedure Insertar_Help(@hlp_trvid integer, @hlp_hlpid integer) as
  INSERT INTO help  
         ( hlp_trvid,   
           hlp_hlpid )  
  VALUES ( @hlp_trvid,   
           @hlp_hlpid )
