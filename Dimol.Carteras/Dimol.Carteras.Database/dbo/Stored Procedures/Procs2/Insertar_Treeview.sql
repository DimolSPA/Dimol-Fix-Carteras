

Create Procedure Insertar_Treeview(@trv_trvid integer, @trv_padid integer, @trv_nombre varchar(400), @trv_imagen varchar(200), @trv_ventana varchar(200),
													 @trv_orden smallint, @trv_menid integer) as


  INSERT INTO treeview  
         ( trv_trvid,   
           trv_padid,   
           trv_nombre,   
           trv_imagen,   
           trv_ventana,   
           trv_orden,   
           trv_menid )  
  VALUES ( @trv_trvid,   
           @trv_padid,   
           @trv_nombre,   
           @trv_imagen,   
           @trv_ventana,   
           @trv_orden,   
           @trv_menid )
