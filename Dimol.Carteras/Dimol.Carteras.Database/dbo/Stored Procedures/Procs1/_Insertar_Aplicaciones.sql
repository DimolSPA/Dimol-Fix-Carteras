CREATE Procedure [dbo].[_Insertar_Aplicaciones](@apl_codemp integer, @apl_sucid integer, @apl_anio smallint, @apl_mes smallint,
												   @apl_tipo integer, @apl_fecing datetime, @apl_fecapl datetime, @apl_accion integer,  @apl_usrid integer) as	
												  
declare @apl_numapl integer =  (select IsNull(Max(apl_numapl)+1, 1)  from aplicaciones where apl_codemp = @apl_codemp and   apl_anio = @apl_anio	)	
declare @fecing datetime = getdate()
declare @fecapl datetime = getdate()
if @apl_fecing is not null 
begin
	set @fecing = 	@apl_fecing
end

if @apl_fecapl is not null 
begin
	set @fecapl = 	@apl_fecapl
end
						  


  INSERT INTO aplicaciones  
         ( apl_codemp,   
           apl_sucid,   
           apl_anio,   
           apl_mes,   
           apl_numapl,   
           apl_tipo,   
           apl_fecing,   
           apl_fecapl,   
           apl_accion,   
           apl_usrid )  
  VALUES ( @apl_codemp,   
           @apl_sucid,   
           @apl_anio,   
           @apl_mes,   
           @apl_numapl,   
           @apl_tipo,   
           @fecing,   
           @fecapl,   
           @apl_accion,   
           @apl_usrid )
           
           select @apl_numapl
