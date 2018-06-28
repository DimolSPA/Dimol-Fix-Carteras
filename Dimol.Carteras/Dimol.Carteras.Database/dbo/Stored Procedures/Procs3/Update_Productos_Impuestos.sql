

Create Procedure Update_Productos_Impuestos(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_iptid smallint) as
  UPDATE productos_impuestos  
     SET pdi_codemp = @pdi_codemp,   
         pdi_prodid = @pdi_prodid,   
         pdi_iptid = @pdi_iptid  
   WHERE ( productos_impuestos.pdi_codemp = @pdi_codemp ) AND  
         ( productos_impuestos.pdi_prodid = @pdi_prodid )
