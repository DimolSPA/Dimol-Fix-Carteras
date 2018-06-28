

  Create Procedure Insertar_Empresa_Configuracion(@emc_codemp integer, @emc_emcid integer, @emc_nombre varchar(400), @emc_valnum numeric(30,6), @emc_valtxt varchar(1000)) as
   INSERT INTO empresa_configuracion  
         ( emc_codemp,   
           emc_emcid,   
           emc_nombre,   
           emc_valnum,   
           emc_valtxt )  
  VALUES ( @emc_codemp,   
           @emc_emcid,   
           @emc_nombre,   
           @emc_valnum,   
           @emc_valtxt )
