

Create Procedure Insertar_Tipos_Transporte(@tpt_codemp integer, @tpt_tptid integer, @tpt_nombre varchar (150), @tpt_tipo smallint) as
  INSERT INTO tipos_transporte  
         ( tpt_codemp,   
           tpt_tptid,   
           tpt_nombre,   
           tpt_tipo )  
  VALUES ( @tpt_codemp,   
           @tpt_tptid,   
           @tpt_nombre,   
           @tpt_tipo )
