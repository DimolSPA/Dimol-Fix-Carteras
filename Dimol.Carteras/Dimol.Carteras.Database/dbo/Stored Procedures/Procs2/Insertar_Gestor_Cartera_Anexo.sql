

Create Procedure Insertar_Gestor_Cartera_Anexo(@gsa_codemp integer, @gsa_sucid integer, @gsa_gesid integer, @gsa_ctcid numeric (15),
															@gsa_gesid2 integer, @gsa_porcom decimal (10,4), @gsa_porcomgp decimal(10,4)) as
  INSERT INTO gestor_cartera_anexo  
         ( gsa_codemp,   
           gsa_sucid,   
           gsa_gesid,   
           gsa_ctcid,   
           gsa_gesid2,   
           gsa_fecasig,   
           gsa_porcom,
           gsa_porcomgp )  
  VALUES ( @gsa_codemp,   
           @gsa_sucid,   
           @gsa_gesid,   
           @gsa_ctcid,   
           @gsa_gesid2,   
           getdate(),   
           @gsa_porcom,
           @gsa_porcomgp )
