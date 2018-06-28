

Create Procedure Delete_Empresa_Configuracion(@emc_codemp integer, @emc_emcid integer) as 
DELETE FROM empresa_configuracion  
   WHERE ( empresa_configuracion.emc_codemp = @emc_codemp ) AND  
         ( empresa_configuracion.emc_emcid = @emc_emcid )
