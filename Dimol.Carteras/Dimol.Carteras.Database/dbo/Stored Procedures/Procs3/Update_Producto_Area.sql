

Create Procedure Update_Producto_Area(@pta_codemp integer, @pta_ptaid integer, @pta_nombre varchar (50), @pta_orden smallint) as
  UPDATE producto_area  
     SET pta_codemp = @pta_codemp,   
         pta_ptaid = @pta_ptaid,   
         pta_nombre = @pta_nombre,   
         pta_orden = @pta_orden  
   WHERE ( producto_area.pta_codemp = @pta_codemp ) AND  
         ( producto_area.pta_ptaid = @pta_ptaid )
