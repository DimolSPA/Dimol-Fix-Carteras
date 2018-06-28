

Create Procedure Delete_Gestor_Cartera_Anexo(@gsa_codemp integer, @gsa_sucid integer, @gsa_gesid integer,
															@gsa_ctcid numeric (15), @gsa_gesid2 integer) as
  DELETE FROM gestor_cartera_anexo  
   WHERE ( gestor_cartera_anexo.gsa_codemp = @gsa_codemp ) AND  
         ( gestor_cartera_anexo.gsa_sucid = @gsa_sucid ) AND  
         ( gestor_cartera_anexo.gsa_gesid = @gsa_gesid ) AND  
         ( gestor_cartera_anexo.gsa_ctcid = @gsa_ctcid ) AND  
         ( gestor_cartera_anexo.gsa_gesid2 = @gsa_gesid2 )
