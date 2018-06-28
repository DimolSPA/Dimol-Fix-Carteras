

CREATE Procedure [dbo].[Trae_Configuracion_Empresa](@emc_codemp integer, @emc_emcid integer) as 
  SELECT empresa_configuracion.emc_valnum,   
         empresa_configuracion.emc_valtxt  
    FROM empresa_configuracion   with (nolock)  
   WHERE ( empresa_configuracion.emc_codemp = @emc_codemp ) AND  
         ( empresa_configuracion.emc_emcid = @emc_emcid )
