Create Procedure [dbo].[_Insertar_Ciudad](
		@ciu_regid integer, 
		@ciu_nombre varchar(200), 
		@ciu_codarea smallint) as  
		
declare @id int                        
set @id = (select IsNull(Max(ciu_ciuid )+1, 1) from ciudad )                    	
		
  INSERT INTO ciudad    
         ( ciu_regid,     
           ciu_ciuid,     
           ciu_nombre,     
           ciu_codarea )    
  VALUES ( @ciu_regid,     
           @id,     
           @ciu_nombre,     
           @ciu_codarea )
