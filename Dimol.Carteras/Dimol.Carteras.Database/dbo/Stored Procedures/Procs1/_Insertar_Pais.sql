Create Procedure [dbo].[_Insertar_Pais](
		@nombre varchar(150), 
		@codtel smallint) 
as  

declare @id int                    
set @id = (select IsNull(Max(pai_paiid )+1, 1) from pais )                

  INSERT INTO pais    
         ( pai_paiid,     
           pai_nombre,     
           pai_codtel )    
  VALUES ( @id,     
           @nombre,     
           @codtel )
