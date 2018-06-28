

Create Procedure Delete_Productos_SubProductos(@psp_codemp integer, @psp_prodid numeric (15), @psp_subprodid numeric (15)) as
  DELETE FROM productos_subproductos  
   WHERE ( productos_subproductos.psp_codemp = @psp_codemp ) AND  
         ( productos_subproductos.psp_prodid = @psp_prodid ) AND  
         ( productos_subproductos.psp_subprodid = @psp_subprodid )
