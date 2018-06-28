Create Procedure [dbo].[_Insertar_Tipos_Tribunal](
			@ttb_codemp integer, 
			@ttb_nombre varchar (50),
			@idid integer 
) 
as    
declare @id int          
          
set @id = (select IsNull(Max(ttb_ttbid )+1, 1) from tipos_tribunal       
    where ttb_codemp = @ttb_codemp)        

  INSERT INTO tipos_tribunal    
         ( ttb_codemp,     
           ttb_ttbid,     
           ttb_nombre )    
  VALUES ( @ttb_codemp,     
           @id,     
           @ttb_nombre )  
           
           
    INSERT INTO tipos_tribunal_idiomas    
         ( tbi_codemp,     
           tbi_ttbid,     
           tbi_idid,     
           tbi_nombre )    
  VALUES ( @ttb_codemp,     
           @id,     
           @idid,     
           @ttb_nombre )
