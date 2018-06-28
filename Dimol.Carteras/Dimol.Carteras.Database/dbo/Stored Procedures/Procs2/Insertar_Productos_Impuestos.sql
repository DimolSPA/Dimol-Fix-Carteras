

Create Procedure Insertar_Productos_Impuestos(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_iptid smallint) as
  INSERT INTO productos_impuestos  
         ( pdi_codemp,   
           pdi_prodid,   
           pdi_iptid )  
  VALUES ( @pdi_codemp,   
           @pdi_prodid,   
           @pdi_iptid )
