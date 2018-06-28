

Create Procedure Insertar_Detalle_Comprobantes_Rol(@dcr_codemp integer, @dcr_sucid integer, @dcr_tpcid integer, @dcr_numero numeric (15),
																	@dcr_item smallint, @dcr_rolid integer, @dcr_subitem smallint, @dcr_monto decimal (15,2)) as
  INSERT INTO detalle_comprobantes_rol  
         ( dcr_codemp,   
           dcr_sucid,   
           dcr_tpcid,   
           dcr_numero,   
           dcr_item,   
           dcr_rolid,   
           dcr_subitem,   
           dcr_monto )  
  VALUES ( @dcr_codemp,   
           @dcr_sucid,   
           @dcr_tpcid,   
           @dcr_numero,   
           @dcr_item,   
           @dcr_rolid,   
           @dcr_subitem,   
           @dcr_monto )
