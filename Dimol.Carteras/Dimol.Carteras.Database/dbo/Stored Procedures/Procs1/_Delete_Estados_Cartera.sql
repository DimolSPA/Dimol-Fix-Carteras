Create Procedure [dbo].[_Delete_Estados_Cartera](
				@ect_codemp integer, 
				@ect_estid smallint,
				@eci_idid int      
) 
as    
  
  DELETE FROM estados_cartera_idiomas    
   WHERE ( estados_cartera_idiomas.eci_codemp = @ect_codemp ) AND    
         ( estados_cartera_idiomas.eci_estid = @ect_estid )  AND
          ( estados_cartera_idiomas.eci_idid = @eci_idid)  
  
  
  DELETE FROM estados_cartera    
   WHERE ( estados_cartera.ect_codemp = @ect_codemp ) AND    
         ( estados_cartera.ect_estid = @ect_estid )
