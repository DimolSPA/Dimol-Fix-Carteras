

Create Procedure Update_Cartera_Clientes_Campana_CpbtDoc(@ccd_codemp integer, @ccd_sucid integer, @ccd_cccid integer, @ccd_pclid numeric (15),
						                                                                        @ccd_ctcid numeric (15), @ccd_estado char (1)) as
  UPDATE cartera_clientes_campana_cpbtdoc  
     SET ccd_estado = @ccd_estado
   WHERE ( cartera_clientes_campana_cpbtdoc.ccd_codemp = @ccd_codemp ) AND  
         ( cartera_clientes_campana_cpbtdoc.ccd_sucid = @ccd_sucid ) AND  
         ( cartera_clientes_campana_cpbtdoc.ccd_cccid = @ccd_cccid ) AND  
         ( cartera_clientes_campana_cpbtdoc.ccd_pclid = @ccd_pclid ) AND  
         ( cartera_clientes_campana_cpbtdoc.ccd_ctcid = @ccd_ctcid )
