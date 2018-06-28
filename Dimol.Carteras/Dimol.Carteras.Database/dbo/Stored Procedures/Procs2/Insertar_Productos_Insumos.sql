

Create Procedure Insertar_Productos_Insumos(@pti_codemp integer, @pti_prodid numeric (15), @pti_insid numeric (15),
														@pti_cantidad decimal (15,2), @pti_fecing datetime) as
  INSERT INTO productos_insumos  
         ( pti_codemp,   
           pti_prodid,   
           pti_insid,   
           pti_cantidad,   
           pti_fecing )  
  VALUES ( @pti_codemp,   
           @pti_prodid,   
           @pti_insid,   
           @pti_cantidad,   
           @pti_fecing )
