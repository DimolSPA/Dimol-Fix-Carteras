Create Procedure [dbo].[_Insertar_Comuna](
			@com_ciuid integer, 
			@com_nombre varchar(200), 
			@com_codpost varchar(20)) 

as  
declare @id int                        
set @id = (select IsNull(Max(com_comid )+1, 1) from comuna )                    

  INSERT INTO comuna    
         ( com_ciuid,     
           com_comid,     
           com_nombre,     
           com_codpost )    
  VALUES ( @com_ciuid,     
           @id,     
           @com_nombre,     
           @com_codpost )
