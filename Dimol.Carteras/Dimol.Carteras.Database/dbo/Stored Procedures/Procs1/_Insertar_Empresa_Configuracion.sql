Create Procedure [dbo].[_Insertar_Empresa_Configuracion](
	@emc_codemp integer, 
	@emc_nombre varchar(400), 
	@emc_valnum numeric(30,6), 
	@emc_valtxt varchar(1000)) 

as  

declare @id int                                  
set @id = (select IsNull(Max(emc_emcid )+1, 1) from empresa_configuracion        
   where emc_codemp = @emc_codemp)                              

   INSERT INTO empresa_configuracion    
         ( emc_codemp,     
           emc_emcid,     
           emc_nombre,     
           emc_valnum,     
           emc_valtxt )    
  VALUES ( @emc_codemp,     
           @id,     
           @emc_nombre,     
           @emc_valnum,     
           @emc_valtxt )
