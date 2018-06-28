

Create Procedure Update_Negociacion_CpbtDoc(@ngd_codemp integer, @ngd_anio smallint, @ngd_negid integer, @ngd_pclid numeric (15), @ngd_ctcid numeric (15), 
															@ngd_ccbid integer,  @ngd_monto_n decimal (15,2), @ngd_intereses_n decimal (15,2), @ngd_honorarios_n decimal (15,2),
															@ngd_gastjud_n decimal (15,2), @ngd_gastotro_n decimal (15,2) ) as 
  UPDATE negociacion_cpbtdoc  
     SET  ngd_monto_n = @ngd_monto_n,   
         ngd_intereses_n = @ngd_intereses_n,   
         ngd_honorarios_n = @ngd_honorarios_n,
         ngd_gastjud_n = @ngd_gastjud_n,
         ngd_gastotro_n = @ngd_gastotro_n      
   WHERE ( negociacion_cpbtdoc.ngd_codemp = @ngd_codemp ) AND  
         ( negociacion_cpbtdoc.ngd_anio = @ngd_anio ) AND  
         ( negociacion_cpbtdoc.ngd_negid = @ngd_negid ) AND  
         ( negociacion_cpbtdoc.ngd_pclid = @ngd_pclid ) AND  
         ( negociacion_cpbtdoc.ngd_ctcid = @ngd_ctcid ) AND  
         ( negociacion_cpbtdoc.ngd_ccbid = @ngd_ccbid )
