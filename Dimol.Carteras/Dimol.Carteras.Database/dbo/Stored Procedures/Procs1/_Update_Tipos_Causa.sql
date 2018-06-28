Create Procedure [dbo].[_Update_Tipos_Causa](
			@tca_codemp integer, 
			@tca_tcaid integer, 
			@tca_nombre varchar (500),
			@idid int
) 

as    
  UPDATE tipos_causa    
     SET   
         tca_nombre = @tca_nombre    
   WHERE ( tipos_causa.tca_codemp = @tca_codemp ) AND    
         ( tipos_causa.tca_tcaid = @tca_tcaid )  
         
    UPDATE tipos_causa_idiomas    
     SET  
         tci_nombre = @tca_nombre    
   WHERE ( tipos_causa_idiomas.tci_codemp = @tca_codemp ) AND    
         ( tipos_causa_idiomas.tci_tcaid = @tca_tcaid ) AND    
         ( tipos_causa_idiomas.tci_idid = @idid )
