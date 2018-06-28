

Create Procedure Delete_Negociacion_CpbtDoc(@ngd_codemp integer, @ngd_anio smallint, @ngd_negid integer, @ngd_pclid numeric (15),
														@ngd_ctcid numeric (15), @ngd_ccbid integer) as 
  DELETE FROM negociacion_cpbtdoc  
   WHERE ( negociacion_cpbtdoc.ngd_codemp = @ngd_codemp ) AND  
         ( negociacion_cpbtdoc.ngd_anio = @ngd_anio ) AND  
         ( negociacion_cpbtdoc.ngd_negid = @ngd_negid ) AND  
         ( negociacion_cpbtdoc.ngd_pclid = @ngd_pclid ) AND  
         ( negociacion_cpbtdoc.ngd_ctcid = @ngd_ctcid ) AND  
         ( negociacion_cpbtdoc.ngd_ccbid = @ngd_ccbid )
