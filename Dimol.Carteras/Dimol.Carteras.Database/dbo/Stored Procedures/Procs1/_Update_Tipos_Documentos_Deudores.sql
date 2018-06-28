-- =============================================        
-- Author:  Pablo Leyton        
-- Create date: 11-06-2014        
-- Description: Procedimiento actualiza Tipo imagenes documentos jQgrid        
-- =============================================       

Create Procedure [dbo].[_Update_Tipos_Documentos_Deudores](
			@tdd_codemp integer, 
			@tdd_tddid integer, 
			@tdd_nombre varchar(150), 
			@tdd_tipo char(1),
			@tdi_idid int) 
as  
  UPDATE tipos_documentos_deudores    
     SET tdd_nombre = @tdd_nombre,     
         tdd_tipo = @tdd_tipo    
   WHERE ( tdd_codemp = @tdd_codemp ) AND    
         ( tdd_tddid = @tdd_tddid )  
         
   UPDATE tipos_documentos_deudores_idiomas    
     SET tdi_nombre = @tdd_nombre    
   WHERE ( tdi_codemp = @tdd_codemp ) AND    
         ( tdi_tddid = @tdd_tddid ) AND    
         ( tdi_idid = @tdi_idid )
