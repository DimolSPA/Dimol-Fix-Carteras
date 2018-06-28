CREATE Procedure [dbo].[_Trae_Materias]      
(      
  @codemp int,                    
  @idioma int             
  )      
     
as        
         
    
BEGIN      
     select mji_esjid, mji_nombre 
     from materia_judicial_idiomas
      where mji_codemp = @codemp 
      and mji_idid = @idioma
      order by mji_nombre
END
