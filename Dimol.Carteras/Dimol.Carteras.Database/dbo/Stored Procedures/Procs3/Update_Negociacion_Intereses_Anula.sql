

Create Procedure Update_Negociacion_Intereses_Anula(@ngd_codemp integer, @ngd_anio smallint, @ngd_negid integer) as
  UPDATE negociacion_cpbtdoc  
     SET ngd_intereses_n = ngd_intereses  
   WHERE ( negociacion_cpbtdoc.ngd_codemp = @ngd_codemp ) AND  
         ( negociacion_cpbtdoc.ngd_anio = @ngd_anio ) AND  
         ( negociacion_cpbtdoc.ngd_negid = @ngd_negid )
