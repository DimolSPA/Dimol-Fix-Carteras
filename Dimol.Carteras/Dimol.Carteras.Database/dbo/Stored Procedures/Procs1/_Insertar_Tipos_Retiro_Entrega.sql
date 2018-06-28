CREATE Procedure [dbo].[_Insertar_Tipos_Retiro_Entrega]  
   (@codemp integer,   
    @nombre varchar (80),
    @idid smallint)   
as    
  
declare @idTipo int      
      
set @idTipo = (select IsNull(Max(tre_treid)+1, 1) from tipos_retiro_entrega where tre_codemp = @codemp)      
  
  INSERT INTO tipos_retiro_entrega      
         ( tre_codemp,       
           tre_treid,       
           tre_nombre )      
  VALUES ( @codemp,       
           @idTipo,       
           @nombre ) 
           
   
           
   INSERT INTO tipos_retiro_entrega_idiomas    
         ( tri_codemp,     
           tri_treid,     
           tri_idid,     
           tri_nombre )    
   VALUES (@codemp,       
           @idTipo, 
           @idid,      
           @nombre )
