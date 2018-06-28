

Create Procedure Find_AFP(@afp_codemp integer, @afp_afpid integer) as
  SELECT count(afp.afp_afpid)  
    FROM afp  
   WHERE ( afp.afp_codemp = @afp_codemp ) AND  
         ( afp.afp_afpid = @afp_afpid )
