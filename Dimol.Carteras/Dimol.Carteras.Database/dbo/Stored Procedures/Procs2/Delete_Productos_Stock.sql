

Create Procedure Delete_Productos_Stock(@pst_codemp integer, @pst_prodid numeric (15)) as
  DELETE FROM productos_stock  
   WHERE ( productos_stock.pst_codemp = @pst_codemp ) AND  
         ( productos_stock.pst_prodid = @pst_prodid )
