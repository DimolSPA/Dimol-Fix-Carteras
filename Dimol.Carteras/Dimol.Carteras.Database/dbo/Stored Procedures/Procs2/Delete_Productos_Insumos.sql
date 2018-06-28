

Create Procedure Delete_Productos_Insumos(@pti_codemp integer, @pti_prodid numeric (15), @pti_insid numeric (15)) as
  DELETE FROM productos_insumos  
   WHERE ( productos_insumos.pti_codemp = @pti_codemp ) AND  
         ( productos_insumos.pti_prodid = @pti_prodid ) AND  
         ( productos_insumos.pti_insid = @pti_insid )
