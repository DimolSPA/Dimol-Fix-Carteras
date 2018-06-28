

Create Procedure Delete_Despachos_Documentos_Estados(@dde_codemp integer, @dde_sucid integer, @dde_dpcid numeric (15),
																		@dde_tpcid integer, @dde_numero numeric (15), @dde_edpid integer,
																		@dde_fecha datetime) as
  DELETE FROM despachos_documentos_estados  
   WHERE ( despachos_documentos_estados.dde_codemp = @dde_codemp ) AND  
         ( despachos_documentos_estados.dde_sucid = @dde_sucid ) AND  
         ( despachos_documentos_estados.dde_dpcid = @dde_dpcid ) AND  
         ( despachos_documentos_estados.dde_tpcid = @dde_tpcid ) AND  
         ( despachos_documentos_estados.dde_numero = @dde_numero ) AND  
         ( despachos_documentos_estados.dde_edpid = @dde_edpid ) AND  
         ( despachos_documentos_estados.dde_fecha = @dde_fecha )
