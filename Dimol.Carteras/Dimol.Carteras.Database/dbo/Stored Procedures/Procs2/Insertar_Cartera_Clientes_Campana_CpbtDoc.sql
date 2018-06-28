

Create Procedure Insertar_Cartera_Clientes_Campana_CpbtDoc(@ccd_codemp integer, @ccd_sucid integer, @ccd_cccid integer, @ccd_pclid numeric (15), 
																				@ccd_ctcid numeric (15), @ccd_ccbid integer,  @ccd_estid smallint) as
  INSERT INTO cartera_clientes_campana_cpbtdoc  
         ( ccd_codemp,   
           ccd_sucid,   
           ccd_cccid,   
           ccd_pclid,   
           ccd_ctcid,   
           ccd_ccbid,   
           ccd_estado,   
           ccd_estid )  
  VALUES ( @ccd_codemp,   
           @ccd_sucid,   
           @ccd_cccid,   
           @ccd_pclid,   
           @ccd_ctcid,   
           @ccd_ccbid,   
           'N',   
           @ccd_estid )
