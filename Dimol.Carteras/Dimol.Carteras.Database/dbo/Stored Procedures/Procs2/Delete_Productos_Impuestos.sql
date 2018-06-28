

Create Procedure Delete_Productos_Impuestos(@pdi_codemp integer, @pdi_prodid numeric (15)) as
  DELETE FROM productos_impuestos  
   WHERE ( productos_impuestos.pdi_codemp = @pdi_codemp ) AND  
         ( productos_impuestos.pdi_prodid = @pdi_prodid )
