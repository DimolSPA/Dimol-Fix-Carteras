﻿

Create Procedure Update_Negociacion_CpbtDoc_Hono_Anula(@ngd_codemp integer, @ngd_anio integer, @ngd_negid integer) as
   UPDATE negociacion_cpbtdoc  
     SET ngd_honorarios_n = ngd_honorarios
   WHERE ( negociacion_cpbtdoc.ngd_codemp = @ngd_codemp ) AND  
         ( negociacion_cpbtdoc.ngd_anio = @ngd_anio ) AND  
         ( negociacion_cpbtdoc.ngd_negid = @ngd_negid )