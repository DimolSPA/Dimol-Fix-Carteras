

 Create Procedure Update_Perfiles_ListaPrecios(@plp_codemp integer, @plp_prfid integer, @plp_ltpid integer) as
  UPDATE perfiles_listaprecios  
     SET plp_ltpid = @plp_ltpid  
   WHERE ( perfiles_listaprecios.plp_codemp = @plp_codemp ) AND  
         ( perfiles_listaprecios.plp_prfid = @plp_prfid ) AND  
         ( perfiles_listaprecios.plp_ltpid = @plp_ltpid )
