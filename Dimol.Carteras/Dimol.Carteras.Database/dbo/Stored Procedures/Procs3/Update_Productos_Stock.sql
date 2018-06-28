

Create Procedure Update_Productos_Stock(@pst_codemp integer, @pst_prodid numeric (15), @pst_unmlgt integer, @pst_alto decimal (15,2),
													@pst_ancho decimal (15,2), @pst_largo decimal (15,2), @pst_cubicaje decimal (15,2), @pst_unmpeso integer,
													@pst_peso decimal (15,2), @pst_unmstock integer,  @pst_orden_bodega integer,
													@pst_orden_otro integer, @pst_armado char (1), @pst_tiparm smallint, @pst_closeout char (1),
													@pst_stock_minimo decimal(15,2), @pst_stock_maximo decimal(15,2),
                                                    @pst_pack smallint, @pst_packint smallint) as
  UPDATE productos_stock  
     SET pst_codemp = @pst_codemp,   
         pst_prodid = @pst_prodid,   
         pst_unmlgt = @pst_unmlgt,   
         pst_alto = @pst_alto,   
         pst_ancho = @pst_ancho,   
         pst_largo = @pst_largo,   
         pst_cubicaje = @pst_cubicaje,   
         pst_unmpeso = @pst_unmpeso,   
         pst_peso = @pst_peso,   
         pst_unmstock = @pst_unmstock,   
         pst_orden_bodega = @pst_orden_bodega,   
         pst_orden_otro = @pst_orden_otro,   
         pst_armado = @pst_armado,   
         pst_tiparm = @pst_tiparm,   
         pst_closeout = @pst_closeout,
         pst_stock_minimo = @pst_stock_minimo,
         pst_stock_maximo = @pst_stock_maximo,
         pst_pack = @pst_pack,
         pst_packint = @pst_packint  
   WHERE ( productos_stock.pst_codemp = @pst_codemp ) AND  
         ( productos_stock.pst_prodid = @pst_prodid )
