

Create Procedure Delete_AFP(@afp_codemp integer, @afp_afpid integer) as  
  DELETE FROM afp  
   WHERE ( afp.afp_codemp = @afp_codemp ) AND  
         ( afp.afp_afpid = @afp_afpid )
