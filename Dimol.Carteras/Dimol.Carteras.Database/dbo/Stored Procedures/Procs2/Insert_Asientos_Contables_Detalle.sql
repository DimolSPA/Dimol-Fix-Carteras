

Create Procedure Insert_Asientos_Contables_Detalle(@acd_codemp integer, @acd_anio integer, @acd_tipo char(1), @acd_numero integer,
															        @acd_item integer, @acd_pctid integer, @acd_ccsid integer, @acd_debe decimal(15,2), @acd_haber decimal(15,2),
																   @acd_exento char(1), @acd_glosa char(400)) as
   INSERT INTO asientos_contables_detalle  
         ( acd_codemp,   
           acd_anio,   
           acd_tipo,   
           acd_numero,   
           acd_item,   
           acd_pctid,   
           acd_ccsid,   
           acd_debe,   
           acd_haber,   
           acd_exento,   
           acd_glosa )  
  VALUES ( @acd_codemp,   
           @acd_anio,   
           @acd_tipo,   
           @acd_numero,   
           @acd_item,   
           @acd_pctid,   
           @acd_ccsid,   
           @acd_debe,   
           @acd_haber,   
           @acd_exento,   
           @acd_glosa )
