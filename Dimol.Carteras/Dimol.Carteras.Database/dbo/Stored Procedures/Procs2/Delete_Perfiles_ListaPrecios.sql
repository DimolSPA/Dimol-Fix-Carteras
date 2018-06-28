

Create Procedure Delete_Perfiles_ListaPrecios(@plp_codemp integer, @plp_prfid integer, @plp_ltpid integer) as  
  DELETE FROM perfiles_listaprecios  
   WHERE ( perfiles_listaprecios.plp_codemp = @plp_codemp ) AND  
         ( perfiles_listaprecios.plp_prfid = @plp_prfid ) AND  
         ( perfiles_listaprecios.plp_ltpid = @plp_ltpid )
