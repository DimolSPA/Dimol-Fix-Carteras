

Create Procedure Update_Gestor_Cartera_Anexo(@gsa_codemp integer, @gsa_sucid integer, @gsa_gesid integer, @gsa_ctcid numeric (15),
															@gsa_gesid2 integer, @gsa_porcom decimal (10,4), @gsa_porcomgp decimal (10,4)) as
  UPDATE gestor_cartera_anexo  
     SET gsa_porcom = @gsa_porcom,
         gsa_porcomgp = @gsa_porcomgp    
   WHERE ( gestor_cartera_anexo.gsa_codemp = @gsa_codemp ) AND  
         ( gestor_cartera_anexo.gsa_sucid = @gsa_sucid ) AND  
         ( gestor_cartera_anexo.gsa_gesid = @gsa_gesid ) AND  
         ( gestor_cartera_anexo.gsa_ctcid = @gsa_ctcid ) AND  
         ( gestor_cartera_anexo.gsa_gesid2 = @gsa_gesid2 )
