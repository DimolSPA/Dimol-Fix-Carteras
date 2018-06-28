

Create  Procedure Update_Empresa_Configuracion(@emc_codemp integer, @emc_emcid integer, @emc_nombre varchar (400), @emc_valnum numeric (30,6),
                                                                                   @emc_valtxt varchar (1000)) as  
  UPDATE empresa_configuracion  
     SET emc_nombre = @emc_nombre,   
         emc_valnum = @emc_valnum,   
         emc_valtxt = @emc_valtxt  
   WHERE ( empresa_configuracion.emc_codemp = @emc_codemp ) AND  
         ( empresa_configuracion.emc_emcid = @emc_emcid )
