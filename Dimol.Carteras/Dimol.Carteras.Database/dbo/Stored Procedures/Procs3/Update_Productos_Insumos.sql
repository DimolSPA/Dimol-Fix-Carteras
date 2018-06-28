

Create Procedure Update_Productos_Insumos(@pti_codemp integer, @pti_prodid numeric (15), @pti_insid numeric (15),
														@pti_cantidad decimal (15,2), @pti_fecing datetime) as
  UPDATE productos_insumos  
     SET pti_codemp = @pti_codemp,   
         pti_prodid = @pti_prodid,   
         pti_insid = @pti_insid,   
         pti_cantidad = @pti_cantidad,   
         pti_fecing = @pti_fecing  
   WHERE ( productos_insumos.pti_codemp = @pti_codemp ) AND  
         ( productos_insumos.pti_prodid = @pti_prodid ) AND  
         ( productos_insumos.pti_insid = @pti_insid )
