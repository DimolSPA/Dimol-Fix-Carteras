
create Procedure [dbo].[_Trae_Materias_Historial_Rol]      
(      
  @codemp int,                    
  @idioma int             
  )      
     
as        
         
    
BEGIN      
SELECT materia_judicial_idiomas.mji_esjid,   
materia_judicial_idiomas.mji_nombre
FROM materia_judicial_idiomas
WHERE  materia_judicial_idiomas.mji_codemp =  @codemp
and materia_judicial_idiomas.mji_idid = @idioma  and mji_esjid > 0 
ORDER BY materia_judicial_idiomas.mji_nombre ASC 
END      

 