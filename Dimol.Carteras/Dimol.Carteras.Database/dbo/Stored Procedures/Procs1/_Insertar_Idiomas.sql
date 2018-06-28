CREATE Procedure [dbo].[_Insertar_Idiomas](  
   @idi_nombre varchar(60), 
   @idi_idisrc varchar(20)
   )   
as      
declare @id int                  
set @id = (select IsNull(Max(idi_idid )+1, 1) from idiomas               
   )          
  
  INSERT INTO idiomas    
         ( idi_idid,     
           idi_nombre,     
           idi_idisrc )    
  VALUES ( @id,     
           @idi_nombre,     
           @idi_idisrc )
