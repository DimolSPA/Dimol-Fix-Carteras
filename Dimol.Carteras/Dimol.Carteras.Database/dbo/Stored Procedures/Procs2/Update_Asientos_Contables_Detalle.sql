

Create Procedure Update_Asientos_Contables_Detalle(@acd_codemp integer, @acd_anio integer, @acd_tipo char(1), @acd_numero integer,
															        @acd_item integer, @acd_pctid integer, @acd_ccsid integer, @acd_debe decimal(15,2), @acd_haber decimal(15,2),
																   @acd_exento char(1), @acd_glosa char(400)) as
    UPDATE asientos_contables_detalle  
     SET acd_pctid = @acd_pctid,   
         acd_ccsid = @acd_ccsid,   
         acd_debe = @acd_debe,   
         acd_haber = @acd_haber,   
         acd_exento = @acd_exento,   
         acd_glosa = @acd_glosa  
   WHERE ( asientos_contables_detalle.acd_codemp = @acd_codemp ) AND  
         ( asientos_contables_detalle.acd_anio = @acd_anio ) AND  
         ( asientos_contables_detalle.acd_tipo = @acd_tipo ) AND  
         ( asientos_contables_detalle.acd_numero = @acd_numero ) AND  
         ( asientos_contables_detalle.acd_item = @acd_item )
