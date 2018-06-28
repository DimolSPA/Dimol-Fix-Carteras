

Create Procedure Insertar_Servicios_Idiomas(@svi_codemp integer, @svi_sveid integer, @svi_idid integer, @svi_titulo varchar(200), @svi_texto text) as
  INSERT INTO servicios_idiomas  
         ( svi_codemp,   
           svi_sveid,   
           svi_idid,   
           svi_titulo,   
           svi_texto )  
  VALUES ( @svi_codemp,   
           @svi_sveid,   
           @svi_idid,   
           @svi_titulo,   
           @svi_texto )
