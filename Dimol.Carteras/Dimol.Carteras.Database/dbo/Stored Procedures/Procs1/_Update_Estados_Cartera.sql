CREATE Procedure [dbo].[_Update_Estados_Cartera]
(
		@ect_codemp integer, 
		@ect_estid smallint, 
		@ect_nombre varchar (300), 
		@ect_agrupa smallint,  
		@ect_utiliza char (1), 
		@ect_prejud char (1), 
		@ect_solfecha char (1), 
		@ect_genret char (1), 
		@ect_compromiso char(1),
		@eci_idid int)    

as    
  UPDATE estados_cartera    
     SET ect_nombre = @ect_nombre,     
         ect_agrupa = @ect_agrupa,     
         ect_utiliza = @ect_utiliza,     
         ect_prejud = @ect_prejud,     
         ect_solfecha = @ect_solfecha,     
         ect_genret = @ect_genret,  
         ect_compromiso =   @ect_compromiso   
   WHERE ( estados_cartera.ect_codemp = @ect_codemp ) AND    
         ( estados_cartera.ect_estid = @ect_estid )  
         
     UPDATE estados_cartera_idiomas    
     SET eci_nombre = @ect_nombre    
   WHERE ( estados_cartera_idiomas.eci_codemp = @ect_codemp ) AND    
         ( estados_cartera_idiomas.eci_estid = @ect_estid ) AND    
         ( estados_cartera_idiomas.eci_idid = @eci_idid )
