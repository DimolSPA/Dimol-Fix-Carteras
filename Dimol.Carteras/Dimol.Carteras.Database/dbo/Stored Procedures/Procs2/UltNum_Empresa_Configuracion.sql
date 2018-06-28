

Create Procedure UltNum_Empresa_Configuracion(@emc_codemp integer) as
  SELECT IsNull(Max(emc_emcid  )+1, 1)  
    FROM empresa_configuracion
   WHERE emc_codemp = @emc_codemp
