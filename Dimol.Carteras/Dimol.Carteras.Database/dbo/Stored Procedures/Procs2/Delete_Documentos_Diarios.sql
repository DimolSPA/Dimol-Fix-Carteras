

Create Procedure Delete_Documentos_Diarios(@ddi_codemp integer, @ddi_sucid integer, @ddi_anio smallint, @ddi_numdoc numeric (15)) as

  DELETE FROM documentos_diarios_imagenes  
   WHERE ( documentos_diarios_imagenes.ddd_codemp = @ddi_codemp ) AND  
         ( documentos_diarios_imagenes.ddd_sucid = @ddi_sucid ) AND  
         ( documentos_diarios_imagenes.ddd_anio = @ddi_anio ) AND  
         ( documentos_diarios_imagenes.ddd_numdoc = @ddi_numdoc ) 

  DELETE FROM documentos_diarios_estados  
   WHERE ( documentos_diarios_estados.dde_codemp = @ddi_codemp ) AND  
         ( documentos_diarios_estados.dde_sucid = @ddi_sucid ) AND  
         ( documentos_diarios_estados.dde_anio = @ddi_anio ) AND  
         ( documentos_diarios_estados.dde_numdoc = @ddi_numdoc ) 


  DELETE FROM asientos_contables_cpbtdoc_apl  
   WHERE ( asientos_contables_cpbtdoc_apl.ada_codemp = @ddi_codemp ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = @ddi_sucid ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_aniodoc = @ddi_anio ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_numdoc = @ddi_numdoc )   


DELETE FROM documentos_diarios_op  
   WHERE ( documentos_diarios_op.ddo_codemp = @ddi_codemp ) AND  
         ( documentos_diarios_op.ddo_sucid = @ddi_sucid ) AND  
         ( documentos_diarios_op.ddo_anio = @ddi_anio ) AND  
         ( documentos_diarios_op.ddo_numdoc = @ddi_numdoc ) 


  DELETE FROM documentos_diarios  
   WHERE ( documentos_diarios.ddi_codemp = @ddi_codemp ) AND  
         ( documentos_diarios.ddi_sucid = @ddi_sucid ) AND  
         ( documentos_diarios.ddi_anio = @ddi_anio ) AND  
         ( documentos_diarios.ddi_numdoc = @ddi_numdoc )
