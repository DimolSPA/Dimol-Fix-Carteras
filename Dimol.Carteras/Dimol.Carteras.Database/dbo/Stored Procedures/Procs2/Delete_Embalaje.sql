

Create Procedure Delete_Embalaje(@emb_codemp integer, @emb_sucid integer, @emb_pclid numeric (15),
											@emb_embid numeric (15), @emb_tpcid integer, @emb_numero numeric (15),
											@emb_item smallint) as
  DELETE FROM embalaje  
   WHERE ( embalaje.emb_codemp = @emb_codemp ) AND  
         ( embalaje.emb_sucid = @emb_sucid ) AND  
         ( embalaje.emb_pclid = @emb_pclid ) AND  
         ( embalaje.emb_embid = @emb_embid ) AND  
         ( embalaje.emb_tpcid = @emb_tpcid ) AND  
         ( embalaje.emb_numero = @emb_numero ) AND  
         ( embalaje.emb_item = @emb_item )
