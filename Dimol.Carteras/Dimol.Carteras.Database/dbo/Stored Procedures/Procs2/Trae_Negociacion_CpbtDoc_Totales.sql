

Create Procedure Trae_Negociacion_CpbtDoc_Totales(@ngd_codemp integer, @ngd_anio integer, @ngd_negid integer) as
  SELECT sum(ngd_monto_n) as TotSaldo,
                sum(ngd_gastjud_n) as TotGjud,
                sum(ngd_gastotro_n) as TotGPre,
                sum(ngd_intereses_n) as TotInte,
                sum(ngd_honorarios_n) as TotHono,
                sum(ngd_monto_n + ngd_gastjud_n +   ngd_gastotro_n +  ngd_intereses_n + ngd_honorarios_n) as Total
    FROM negociacion_cpbtdoc  
   WHERE ( ngd_codemp = @ngd_codemp ) AND  
         ( ngd_anio = @ngd_anio ) AND  
         ( ngd_negid = @ngd_negid )
