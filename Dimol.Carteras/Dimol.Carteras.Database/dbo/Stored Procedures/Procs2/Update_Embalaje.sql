

Create Procedure Update_Embalaje(@emb_codemp integer, @emb_sucid integer, @emb_pclid numeric (15), 
											@emb_embid numeric (15), @emb_tpcid integer, @emb_numero numeric (15),
											@emb_item smallint, @emb_cantidad numeric (15,2), @emb_ordencomp varchar (20),
											@emb_pcdid integer, @emb_estado char(1)) as
   UPDATE embalaje  
     SET emb_codemp = @emb_codemp,   
         emb_sucid = @emb_sucid,   
         emb_pclid = @emb_pclid,   
         emb_embid = @emb_embid,   
         emb_tpcid = @emb_tpcid,   
         emb_numero = @emb_numero,   
         emb_item = @emb_item,   
         emb_cantidad = @emb_cantidad,   
         emb_ordencomp = @emb_ordencomp,   
         emb_pcdid = @emb_pcdid,
         emb_estado = @emb_estado  
   WHERE ( embalaje.emb_codemp = @emb_codemp ) AND  
         ( embalaje.emb_sucid = @emb_sucid ) AND  
         ( embalaje.emb_pclid = @emb_pclid ) AND  
         ( embalaje.emb_embid = @emb_embid ) AND  
         ( embalaje.emb_tpcid = @emb_tpcid ) AND  
         ( embalaje.emb_numero = @emb_numero ) AND  
         ( embalaje.emb_item = @emb_item )
