

Create Procedure Update_Productos_SubProductos(@psp_codemp integer, @psp_prodid numeric (15), @psp_subprodid numeric (15),
																@psp_cantidad decimal (15,2), @psp_ptaid integer, @psp_fecing datetime) as
  UPDATE productos_subproductos  
     SET psp_codemp = @psp_codemp,   
         psp_prodid = @psp_prodid,   
         psp_subprodid = @psp_subprodid,   
         psp_cantidad = @psp_cantidad,   
         psp_ptaid = @psp_ptaid,   
         psp_fecing = @psp_fecing  
   WHERE ( productos_subproductos.psp_codemp = @psp_codemp ) AND  
         ( productos_subproductos.psp_prodid = @psp_prodid ) AND  
         ( productos_subproductos.psp_subprodid = @psp_subprodid )
