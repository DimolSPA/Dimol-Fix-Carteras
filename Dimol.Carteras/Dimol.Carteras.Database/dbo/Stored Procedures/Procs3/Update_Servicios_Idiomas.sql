

Create Procedure Update_Servicios_Idiomas(@svi_codemp integer, @svi_sveid integer, @svi_idid integer, @svi_titulo varchar(200), @svi_texto text) as
   UPDATE servicios_idiomas  
     SET svi_titulo = @svi_titulo,   
         svi_texto = @svi_texto  
   WHERE ( servicios_idiomas.svi_codemp = @svi_codemp ) AND  
         ( servicios_idiomas.svi_sveid = @svi_sveid ) AND  
         ( servicios_idiomas.svi_idid = @svi_idid )
