

Create Procedure Update_Grupos_Cobranza(@grc_codemp integer, @grc_sucid integer, @grc_grcid integer, @grc_nombre varchar (200), @grc_emplid integer) as
  UPDATE grupos_cobranza  
     SET grc_nombre = @grc_nombre,   
         grc_emplid = @grc_emplid  
   WHERE ( grupos_cobranza.grc_codemp = @grc_codemp ) AND  
         ( grupos_cobranza.grc_sucid = @grc_sucid ) AND  
         ( grupos_cobranza.grc_grcid = @grc_grcid )
