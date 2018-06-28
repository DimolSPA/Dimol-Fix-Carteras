

Create Procedure Delete_Cabecera_Comprobantes_Estados(@cbe_codemp integer, @cbe_sucid integer, @cbe_tpcid integer,
																		@cbe_numero numeric (15), @cbe_estado char (1), @cbe_fecha datetime) as  
  DELETE FROM cabacera_comprobantes_estados  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbe_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbe_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbe_tpcid ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbe_numero ) AND  
         ( cabacera_comprobantes_estados.cbe_estado = @cbe_estado ) AND  
         ( cabacera_comprobantes_estados.cbe_fecha = @cbe_fecha )
