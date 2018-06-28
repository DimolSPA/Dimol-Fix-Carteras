

Create Procedure Update_Productos_CodBar(@pdt_codemp integer, @pdt_prodid integer, @pdt_conbarimg image) as
  UPDATE productos  
     SET pdt_conbarimg = @pdt_conbarimg  
   WHERE ( productos.pdt_codemp = @pdt_codemp ) AND  
         ( productos.pdt_prodid = @pdt_prodid )
