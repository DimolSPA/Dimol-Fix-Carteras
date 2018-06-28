

Create Procedure Insertar_Embalaje(@emb_codemp integer, @emb_sucid integer, @emb_pclid numeric (15), 
											@emb_embid numeric (15), @emb_tpcid integer, @emb_numero numeric (15),
											@emb_item smallint, @emb_cantidad numeric (15,2), @emb_ordencomp varchar (20),
											@emb_pcdid integer) as
  INSERT INTO embalaje  
         ( emb_codemp,   
           emb_sucid,   
           emb_pclid,   
           emb_embid,   
           emb_tpcid,   
           emb_numero,   
           emb_item,   
           emb_cantidad,   
           emb_ordencomp,   
           emb_pcdid )  
  VALUES ( @emb_codemp,   
           @emb_sucid,   
           @emb_pclid,   
           @emb_embid,   
           @emb_tpcid,   
           @emb_numero,   
           @emb_item,   
           @emb_cantidad,   
           @emb_ordencomp,   
           @emb_pcdid )
