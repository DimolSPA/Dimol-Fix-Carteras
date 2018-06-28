

Create Procedure Delete_Documentos_Diarios_Estados_Especial(@dde_codemp integer, @dde_sucid integer, @dde_anio smallint, 
                                                                                           @dde_numdoc numeric (15), @dde_edcid integer, @dde_fecproc datetime) as
  DELETE FROM documentos_diarios_estados  
   WHERE ( documentos_diarios_estados.dde_codemp = @dde_codemp ) AND  
         ( documentos_diarios_estados.dde_sucid = @dde_sucid ) AND  
         ( documentos_diarios_estados.dde_anio = @dde_anio ) AND  
         ( documentos_diarios_estados.dde_numdoc = @dde_numdoc ) AND  
         ( documentos_diarios_estados.dde_edcid = @dde_edcid ) AND  
         ( datepart(year, documentos_diarios_estados.dde_fecproc) = datepart(year,@dde_fecproc) )   and
         ( datepart(month, documentos_diarios_estados.dde_fecproc) = datepart(month,@dde_fecproc) )   and
         ( datepart(day, documentos_diarios_estados.dde_fecproc) = datepart(day,@dde_fecproc) )
