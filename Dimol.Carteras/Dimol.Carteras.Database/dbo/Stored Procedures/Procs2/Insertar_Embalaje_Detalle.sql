

Create Procedure Insertar_Embalaje_Detalle(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero numeric(15), @emb_pclid numeric(15), @emb_oc varchar(20), @emb_pcdid integer)  as
insert into embalaje
  SELECT detalle_comprobantes.dcc_codemp,   
         detalle_comprobantes.dcc_sucid,   
         @emb_pclid,
         @dcc_numero *-1,
         detalle_comprobantes.dcc_tpcid,   
         detalle_comprobantes.dcc_numero,   
         detalle_comprobantes.dcc_item,   
         detalle_comprobantes.dcc_cantidad,
         @emb_oc,
         @emb_pcdid,
         'E'  
    FROM detalle_comprobantes  
   WHERE ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero )
