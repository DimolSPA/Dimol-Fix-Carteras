

Create Procedure Insertar_AFP(@afp_codemp integer, @afp_afpid integer,@afp_rut varchar(20),  @afp_nombre varchar(20), @afp_pctid integer) as
   INSERT INTO afp  
         ( afp_codemp,   
           afp_afpid,   
           afp_rut,   
           afp_nombre,   
           afp_pctid )  
  VALUES ( @afp_codemp,   
           @afp_afpid,   
           @afp_rut,   
           @afp_nombre,   
           @afp_pctid )
