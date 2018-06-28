

Create Procedure Delete_Detalle_Comprobantes_Rol(@dcr_codemp integer, @dcr_sucid integer, @dcr_tpcid integer, @dcr_numero numeric (15),
																@dcr_item smallint, @dcr_rolid integer, @dcr_subitem smallint) as  
  DELETE FROM detalle_comprobantes_rol  
   WHERE ( detalle_comprobantes_rol.dcr_codemp = @dcr_codemp ) AND  
         ( detalle_comprobantes_rol.dcr_sucid = @dcr_sucid ) AND  
         ( detalle_comprobantes_rol.dcr_tpcid = @dcr_tpcid ) AND  
         ( detalle_comprobantes_rol.dcr_numero = @dcr_numero ) AND  
         ( detalle_comprobantes_rol.dcr_item = @dcr_item ) AND  
         ( detalle_comprobantes_rol.dcr_rolid = @dcr_rolid ) AND  
         ( detalle_comprobantes_rol.dcr_subitem = @dcr_subitem )
