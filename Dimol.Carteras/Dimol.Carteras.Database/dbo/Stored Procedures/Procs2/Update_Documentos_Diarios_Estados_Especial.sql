

Create Procedure Update_Documentos_Diarios_Estados_Especial(@ddi_codemp integer, @ddi_sucid integer, @ddi_anio integer, @ddi_numdoc integer, @ddi_edcid integer, @ddi_saldo decimal(15,2)) as
  UPDATE documentos_diarios  
     SET
         ddi_edcid = @ddi_edcid, 
         ddi_saldo = @ddi_saldo
   WHERE ( documentos_diarios.ddi_codemp = @ddi_codemp ) AND  
         ( documentos_diarios.ddi_sucid = @ddi_sucid ) AND  
         ( documentos_diarios.ddi_anio = @ddi_anio ) AND  
         ( documentos_diarios.ddi_numdoc = @ddi_numdoc )
