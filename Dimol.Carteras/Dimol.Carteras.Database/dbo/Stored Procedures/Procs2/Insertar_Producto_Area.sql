

Create Procedure Insertar_Producto_Area(@pta_codemp integer, @pta_ptaid integer, @pta_nombre varchar (50), @pta_orden smallint) as
  INSERT INTO producto_area  
         ( pta_codemp,   
           pta_ptaid,   
           pta_nombre,   
           pta_orden )  
  VALUES ( @pta_codemp,   
           @pta_ptaid,   
           @pta_nombre,   
           @pta_orden )
