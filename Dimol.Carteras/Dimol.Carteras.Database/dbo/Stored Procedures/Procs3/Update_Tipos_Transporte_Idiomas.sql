

Create Procedure Update_Tipos_Transporte_Idiomas(@tti_codemp integer, @tti_tptid integer, @tti_idid integer, @tti_nombre varchar (200)) as
  UPDATE tipos_transporte_idiomas  
     SET tti_codemp = @tti_codemp,   
         tti_tptid = @tti_tptid,   
         tti_idid = @tti_idid,   
         tti_nombre = @tti_nombre  
   WHERE ( tipos_transporte_idiomas.tti_codemp = @tti_codemp ) AND  
         ( tipos_transporte_idiomas.tti_tptid = @tti_tptid ) AND  
         ( tipos_transporte_idiomas.tti_idid = @tti_idid )
