-- =============================================          
-- Author:  Pablo Leyton          
-- Create date: 11-06-2014          
-- Description: Procedimiento Inserta Tipo imagenes documentos jQgrid          
-- =============================================          
    
CREATE Procedure [dbo].[_Insertar_tipos_documentos_deudores]      
   (      
    @codemp integer,       
    @nombre varchar (80),      
    @tipo varchar (10),      
    @idid smallint)         
as        
declare @id int        
        
set @id = (select IsNull(Max(TDD_TDDID )+1, 1) from tipos_documentos_deudores     
    where TDD_CODEMP = @codemp)        
        
  INSERT INTO tipos_documentos_deudores          
         (      
     TDD_CODEMP,      
     TDD_TDDID,       
     TDD_NOMBRE,  
     TDD_TIPO )          
    
  VALUES ( @codemp,           
           @id,           
           @nombre,  
           @tipo)        
     
     
   INSERT INTO tipos_documentos_deudores_idiomas    
 (    
  TDI_CODEMP ,    
  TDI_TDDID ,  
  TDI_IDID ,    
  TDI_NOMBRE     
 )    
 VALUES    
 (  
  @codemp,           
  @id,    
  @idid,         
  @nombre  
 )
