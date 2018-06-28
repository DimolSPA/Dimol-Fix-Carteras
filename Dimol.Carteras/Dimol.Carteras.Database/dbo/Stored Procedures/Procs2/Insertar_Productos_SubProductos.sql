

Create Procedure Insertar_Productos_SubProductos(@psp_codemp integer, @psp_prodid numeric (15), @psp_subprodid numeric (15),
																@psp_cantidad decimal (15,2), @psp_ptaid integer) as
   INSERT INTO productos_subproductos  
         ( psp_codemp,   
           psp_prodid,   
           psp_subprodid,   
           psp_cantidad,   
           psp_ptaid,   
           psp_fecing )  
  VALUES ( @psp_codemp,   
           @psp_prodid,   
           @psp_subprodid,   
           @psp_cantidad,   
           @psp_ptaid,   
           getdate() )
