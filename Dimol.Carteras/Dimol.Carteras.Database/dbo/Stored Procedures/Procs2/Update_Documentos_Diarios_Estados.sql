

Create Procedure Update_Documentos_Diarios_Estados(@dde_codemp integer, @dde_sucid integer, @dde_anio smallint, @dde_numdoc numeric (15),
                                                                                            @dde_edcid integer, @dde_fecproc datetime, @dde_usrid integer, @dde_comentario text) as 
  UPDATE documentos_diarios_estados  
     SET dde_usrid = @dde_usrid,   
         dde_comentario = @dde_comentario  
   WHERE ( documentos_diarios_estados.dde_codemp = @dde_codemp ) AND  
         ( documentos_diarios_estados.dde_sucid = @dde_sucid ) AND  
         ( documentos_diarios_estados.dde_anio = @dde_anio ) AND  
         ( documentos_diarios_estados.dde_numdoc = @dde_numdoc ) AND  
         ( documentos_diarios_estados.dde_edcid = @dde_edcid ) AND  
         ( documentos_diarios_estados.dde_fecproc = @dde_fecproc )
