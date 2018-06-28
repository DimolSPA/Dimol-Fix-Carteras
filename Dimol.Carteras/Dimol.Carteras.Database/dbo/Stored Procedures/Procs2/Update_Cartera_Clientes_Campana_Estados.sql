

Create Procedure Update_Cartera_Clientes_Campana_Estados(@ccc_codemp integer, @ccc_sucid integer, @ccc_cccid integer, @ccc_estado char(1)) as

  UPDATE cartera_clientes_campana  
     SET ccc_fecfinreal = getdate(),   
         ccc_estado = @ccc_estado 
   WHERE ( cartera_clientes_campana.ccc_codemp = @ccc_codemp ) AND  
         ( cartera_clientes_campana.ccc_sucid = @ccc_sucid ) AND  
         ( cartera_clientes_campana.ccc_cccid = @ccc_cccid )   
         

  UPDATE cartera_clientes_campana_cpbtdoc  
     SET ccd_estado = 'S'  
   WHERE ( cartera_clientes_campana_cpbtdoc.ccd_codemp =@ccc_codemp ) AND  
         ( cartera_clientes_campana_cpbtdoc.ccd_sucid =@ccc_sucid ) AND  
         ( cartera_clientes_campana_cpbtdoc.ccd_cccid = @ccc_cccid )
