

Create Procedure Insertar_Estados_Empleado(@eem_codemp integer, @eem_eemid integer, @eem_nombre varchar(40), @eem_accion char(1)) as
  INSERT INTO estados_empleado  
         ( eem_codemp,   
           eem_eemid,   
           eem_nombre,   
           eem_accion )  
  VALUES ( @eem_codemp,   
           @eem_eemid,   
           @eem_nombre,   
           @eem_accion )
