

Create Procedure Insertar_Productos_Imagenes(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_pdiid integer,  
															@pdi_imagen image, @pdi_default char (1), @pdi_orden smallint) as
  INSERT INTO productos_imagenes  
         ( pdi_codemp,   
           pdi_prodid,   
           pdi_pdiid,   
           pdi_imagen,   
           pdi_default,   
           pdi_orden )  
  VALUES ( @pdi_codemp,   
           @pdi_prodid,   
           @pdi_pdiid,   
           @pdi_imagen,   
           @pdi_default,   
           @pdi_orden )
