

Create Procedure Update_Tipos_Transporte(@tpt_codemp integer, @tpt_tptid integer, @tpt_nombre varchar (150), @tpt_tipo smallint) as
  UPDATE tipos_transporte  
     SET tpt_codemp = @tpt_codemp,   
         tpt_tptid = @tpt_tptid,   
         tpt_nombre = @tpt_nombre,   
         tpt_tipo = @tpt_tipo  
   WHERE ( tipos_transporte.tpt_codemp = @tpt_codemp ) AND  
         ( tipos_transporte.tpt_tptid = @tpt_tptid )
