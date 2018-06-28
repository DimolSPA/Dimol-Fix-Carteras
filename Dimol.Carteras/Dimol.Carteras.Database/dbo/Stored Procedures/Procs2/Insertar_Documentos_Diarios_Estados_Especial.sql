

Create Procedure Insertar_Documentos_Diarios_Estados_Especial(@dde_codemp integer, @dde_sucid integer, @dde_anio smallint, @dde_numdoc numeric (15),
                                                                                              @dde_edcid integer, @dde_fecproc datetime,  @dde_usrid integer, @dde_comentario text) as 
  INSERT INTO documentos_diarios_estados  
         ( dde_codemp,   
           dde_sucid,   
           dde_anio,   
           dde_numdoc,   
           dde_edcid,   
           dde_fecproc,   
           dde_usrid,   
           dde_comentario )  
  VALUES ( @dde_codemp,   
           @dde_sucid,   
           @dde_anio,   
           @dde_numdoc,   
           @dde_edcid,   
           getdate(),   
           @dde_usrid,   
           @dde_comentario )
