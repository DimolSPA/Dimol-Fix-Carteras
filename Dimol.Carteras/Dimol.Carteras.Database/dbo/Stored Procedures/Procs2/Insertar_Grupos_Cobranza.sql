

Create Procedure Insertar_Grupos_Cobranza(@grc_codemp integer, @grc_sucid integer, @grc_grcid integer, @grc_nombre varchar (200), @grc_emplid integer) as
   INSERT INTO grupos_cobranza  
         ( grc_codemp,   
           grc_sucid,   
           grc_grcid,   
           grc_nombre,   
           grc_emplid )  
  VALUES ( @grc_codemp,   
           @grc_sucid,   
           @grc_grcid,   
           @grc_nombre,   
           @grc_emplid )
