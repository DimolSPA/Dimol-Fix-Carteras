

Create Procedure Delete_Cartera_Clientes_Cpbt_Doc_Imagenes(@cdi_codemp integer, @cdi_pclid numeric (15), @cdi_ctcid numeric (15),
                                                                                                         @cdi_ccbid integer, @cdi_cdid integer) as  
DELETE FROM cartera_clientes_cpbt_doc_imagenes  
   WHERE ( cartera_clientes_cpbt_doc_imagenes.cdi_codemp = @cdi_codemp ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_pclid = @cdi_pclid ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_ctcid = @cdi_ctcid ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_ccbid = @cdi_ccbid ) AND  
         ( cartera_clientes_cpbt_doc_imagenes.cdi_cdid = @cdi_cdid )
