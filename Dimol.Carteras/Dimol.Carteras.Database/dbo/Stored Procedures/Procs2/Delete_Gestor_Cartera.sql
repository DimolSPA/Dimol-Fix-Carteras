

Create Procedure Delete_Gestor_Cartera(@gsc_codemp integer, @gsc_sucid integer, @gsc_gesid integer, @gsc_ctcid numeric (15)) as
  DELETE FROM gestor_cartera  
   WHERE ( gestor_cartera.gsc_codemp = @gsc_codemp ) AND  
         ( gestor_cartera.gsc_sucid = @gsc_sucid ) AND  
         ( gestor_cartera.gsc_gesid = @gsc_gesid ) AND  
         ( gestor_cartera.gsc_ctcid = @gsc_ctcid )
