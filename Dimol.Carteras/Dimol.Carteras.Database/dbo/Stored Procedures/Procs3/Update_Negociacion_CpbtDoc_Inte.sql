

Create Procedure Update_Negociacion_CpbtDoc_Inte(@ngd_codemp integer, @ngd_anio smallint, @ngd_negid numeric(15), @ngp_intereses_n decimal(15,2)) as
  UPDATE negociacion_cpbtdoc  
     SET ngd_intereses_n = @ngp_intereses_n  
   WHERE ( negociacion_cpbtdoc.ngd_codemp = @ngd_codemp ) AND  
         ( negociacion_cpbtdoc.ngd_anio = @ngd_anio ) AND  
         ( negociacion_cpbtdoc.ngd_negid = @ngd_negid )
