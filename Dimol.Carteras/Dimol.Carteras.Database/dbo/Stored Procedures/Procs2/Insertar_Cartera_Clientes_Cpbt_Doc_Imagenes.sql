

Create Procedure Insertar_Cartera_Clientes_Cpbt_Doc_Imagenes(@cdi_codemp integer, @cdi_pclid numeric (15), @cdi_ctcid numeric (15), 
                                                                                                           @cdi_ccbid integer, @cdi_cdid integer, @cdi_imagen image, @cdi_tpcid integer) as  
INSERT INTO cartera_clientes_cpbt_doc_imagenes  
         ( cdi_codemp,   
           cdi_pclid,   
           cdi_ctcid,   
           cdi_ccbid,   
           cdi_cdid,   
           cdi_imagen,
           cdi_tpcid )  
  VALUES ( @cdi_codemp,   
           @cdi_pclid,   
           @cdi_ctcid,   
           @cdi_ccbid,   
           @cdi_cdid,   
           @cdi_imagen,
           @cdi_tpcid )
