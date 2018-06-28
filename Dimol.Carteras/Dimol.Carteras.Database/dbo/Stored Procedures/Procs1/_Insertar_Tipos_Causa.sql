CREATE Procedure [dbo].[_Insertar_Tipos_Causa](
			@tca_codemp integer, 
			@tca_nombre varchar (500),
			@idid smallint
)

 as   
declare @id int        
        
set @id = (select IsNull(Max(tca_tcaid )+1, 1) from tipos_causa     
    where tca_codemp = @tca_codemp)      
  
  INSERT INTO tipos_causa    
         ( tca_codemp,     
           tca_tcaid,     
           tca_nombre )    
  VALUES ( @tca_codemp,     
           @id,     
           @tca_nombre )  
           
    INSERT INTO tipos_causa_idiomas    
         ( tci_codemp,     
           tci_tcaid,     
           tci_idid,     
           tci_nombre )    
  VALUES ( @tca_codemp,     
           @id,     
           @idid,     
           @tca_nombre )
