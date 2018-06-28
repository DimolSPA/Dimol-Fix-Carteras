

Create Procedure Insertar_Perfiles_ListaPrecios(@plp_codemp integer, @plp_prfid integer, @plp_ltpid integer) as  
  INSERT INTO perfiles_listaprecios  
         ( plp_codemp,   
           plp_prfid,   
           plp_ltpid )  
  VALUES ( @plp_codemp,   
           @plp_prfid,   
           @plp_ltpid )
