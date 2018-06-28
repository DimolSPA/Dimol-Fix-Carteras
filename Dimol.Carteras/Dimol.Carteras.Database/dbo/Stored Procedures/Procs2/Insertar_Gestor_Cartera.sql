

Create Procedure Insertar_Gestor_Cartera(@gsc_codemp integer, @gsc_sucid integer, 
                                         @gsc_gesid integer, @gsc_ctcid numeric (15), @gsc_pclid integer) as
  INSERT INTO gestor_cartera  
         ( gsc_codemp,   
           gsc_sucid,   
           gsc_gesid,   
           gsc_ctcid,   
           gsc_fecasig,
           gsc_pclid )  
  VALUES ( @gsc_codemp,   
           @gsc_sucid,   
           @gsc_gesid,   
           @gsc_ctcid,   
           getdate(),
           @gsc_pclid )
