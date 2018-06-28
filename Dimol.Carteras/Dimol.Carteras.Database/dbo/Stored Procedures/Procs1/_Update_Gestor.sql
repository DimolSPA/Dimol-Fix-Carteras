CREATE Procedure [dbo].[_Update_Gestor](  
    @ges_codemp integer,   
    @ges_sucid integer,   
    @ges_gesid integer,   
    @ges_nombre varchar (500),    
    @ges_telefono integer,   
    @ges_email varchar (100),   
    @ges_tipcart smallint,   
    @ges_comki decimal (8,4),    
    @ges_comhon decimal (8,4),   
    @ges_emplid integer,   
    @ges_remoto char(1),   
    @ges_estado char(1),    
    @ges_comjki decimal(8,4),   
    @ges_comjhon decimal(8,4),
    @ges_grupoid int)   
  
as    

	update	grupo_cobranza_gestor set 
		GCG_GRCID = @ges_grupoid
	WHERE  GCG_CODEMP=@ges_codemp
	AND GCG_SUCID = @ges_sucid
	AND GCG_GESID = @ges_gesid



  UPDATE gestor      
     SET ges_nombre = @ges_nombre,       
         ges_telefono = @ges_telefono,       
         ges_email = @ges_email,       
         ges_tipcart = @ges_tipcart,       
         ges_comki = @ges_comki,       
         ges_comhon = @ges_comhon,       
         ges_emplid = @ges_emplid,    
         ges_remoto = @ges_remoto,    
         ges_estado = @ges_estado,    
         ges_comjki = @ges_comjki,       
         ges_comjhon = @ges_comjhon      
   WHERE ( gestor.ges_codemp = @ges_codemp ) AND      
         ( gestor.ges_sucid = @ges_sucid ) AND      
         ( gestor.ges_gesid = @ges_gesid )
