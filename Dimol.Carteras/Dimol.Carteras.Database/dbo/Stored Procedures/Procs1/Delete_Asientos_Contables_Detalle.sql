

Create Procedure Delete_Asientos_Contables_Detalle(@acd_codemp integer, @acd_anio integer, @acd_tipo char(1), @acd_numero integer, @acd_item integer) as
 DELETE FROM asientos_contables_detalle  
   WHERE ( asientos_contables_detalle.acd_codemp = @acd_codemp ) AND  
         ( asientos_contables_detalle.acd_anio = @acd_anio ) AND  
         ( asientos_contables_detalle.acd_tipo = @acd_tipo ) AND  
         ( asientos_contables_detalle.acd_numero = @acd_numero )  AND
         ( asientos_contables_detalle.acd_item = @acd_item )
