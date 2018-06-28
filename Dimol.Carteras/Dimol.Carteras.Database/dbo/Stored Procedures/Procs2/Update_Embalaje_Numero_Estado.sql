

Create Procedure Update_Embalaje_Numero_Estado(@emb_codemp integer, @emb_sucid integer, @emb_pclid integer, @emb_embid numeric(15), @emb_embidNew numeric(15), @emb_estado char(1)) as
  UPDATE embalaje  
     SET emb_embid = @emb_embidNew,   
         emb_estado = @emb_estado  
   WHERE ( embalaje.emb_codemp = @emb_codemp ) AND  
         ( embalaje.emb_sucid = @emb_sucid ) AND  
         ( embalaje.emb_pclid = @emb_pclid ) AND  
         ( embalaje.emb_embid = @emb_embid )
