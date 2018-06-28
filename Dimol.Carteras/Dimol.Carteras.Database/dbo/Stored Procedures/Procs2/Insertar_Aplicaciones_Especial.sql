

Create Procedure Insertar_Aplicaciones_Especial(@apl_codemp integer, @apl_sucid integer, @apl_anio smallint, @apl_mes smallint,
												  @apl_numapl integer, @apl_tipo integer, @apl_fecing datetime, @apl_fecapl datetime, @apl_accion integer,  @apl_usrid integer) as	
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
           @apl_fecing,   
           @apl_fecapl,   
           @apl_accion,   
           @apl_usrid )
