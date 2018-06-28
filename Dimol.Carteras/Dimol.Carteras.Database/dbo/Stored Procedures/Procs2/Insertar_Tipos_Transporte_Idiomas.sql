

Create Procedure Insertar_Tipos_Transporte_Idiomas(@tti_codemp integer, @tti_tptid integer, @tti_idid integer, @tti_nombre varchar (200)) as
  INSERT INTO tipos_transporte_idiomas  
         ( tti_codemp,   
           tti_tptid,   
           tti_idid,   
           tti_nombre )  
  VALUES ( @tti_codemp,   
           @tti_tptid,   
           @tti_idid,   
           @tti_nombre )
