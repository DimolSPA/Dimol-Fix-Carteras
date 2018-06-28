CREATE Procedure [dbo].[_Trae_Tipos_Ente]        
  @idioma int    
        
         
as            
       
        
BEGIN          
    SELECT '-1' as Id, 'Seleccione' as Descripcion
    UNION ALL
    SELECT  substring(etiquetas_idiomas.eti_descripcion,1,1) as Id,      
	etiquetas_idiomas.eti_descripcion  as Descripcion        
    FROM etiquetas,           
         etiquetas_idiomas          
   WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and          
         ( etiquetas_idiomas.eti_idid = @idioma )  and      
         ( etiquetas.etq_codigo IN   
         ('TipEnt1','TipEnt2','TipEnt3','TipEnt4'))      
  
END
