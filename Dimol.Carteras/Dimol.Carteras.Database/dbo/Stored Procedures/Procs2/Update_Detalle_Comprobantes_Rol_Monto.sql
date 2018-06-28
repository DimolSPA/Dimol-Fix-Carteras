

Create Procedure Update_Detalle_Comprobantes_Rol_Monto(@dcr_codemp integer, @dcr_sucid integer, @dcr_tpcid integer, @dcr_numero numeric (15),
																	@dcr_item smallint,  @dcr_monto decimal (15,2)) as
  UPDATE detalle_comprobantes_rol  
     SET  dcr_monto = @dcr_monto  
   WHERE ( detalle_comprobantes_rol.dcr_codemp = @dcr_codemp ) AND  
         ( detalle_comprobantes_rol.dcr_sucid = @dcr_sucid ) AND  
         ( detalle_comprobantes_rol.dcr_tpcid = @dcr_tpcid ) AND  
         ( detalle_comprobantes_rol.dcr_numero = @dcr_numero ) AND  
         ( detalle_comprobantes_rol.dcr_item = @dcr_item )
