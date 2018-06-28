

Create Procedure Update_AFP(@afp_codemp integer, @afp_afpid integer, @afp_rut varchar (20), @afp_nombre varchar (20), @afp_pctid integer) as  
  UPDATE afp  
     SET afp_rut = @afp_rut,   
         afp_nombre = @afp_nombre,   
         afp_pctid = @afp_pctid  
   WHERE ( afp.afp_codemp = @afp_codemp ) AND  
         ( afp.afp_afpid = @afp_afpid )
