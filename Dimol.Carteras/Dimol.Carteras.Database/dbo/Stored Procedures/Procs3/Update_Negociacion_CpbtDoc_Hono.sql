

Create Procedure Update_Negociacion_CpbtDoc_Hono(@ngd_codemp integer, @ngd_anio integer, @ngd_negid integer, @ngd_pclid integer, @ngd_ctcid numeric(15), @ngd_ccbid integer, @ngd_honorarios_n decimal(15,2)) as
   UPDATE negociacion_cpbtdoc  
     SET ngd_honorarios_n = @ngd_honorarios_n  
   WHERE ( negociacion_cpbtdoc.ngd_codemp = @ngd_codemp ) AND  
         ( negociacion_cpbtdoc.ngd_anio = @ngd_anio ) AND  
         ( negociacion_cpbtdoc.ngd_negid = @ngd_negid ) AND  
         ( negociacion_cpbtdoc.ngd_pclid = @ngd_pclid ) AND  
         ( negociacion_cpbtdoc.ngd_ctcid = @ngd_ctcid ) AND  
         ( negociacion_cpbtdoc.ngd_ccbid = @ngd_ccbid )
