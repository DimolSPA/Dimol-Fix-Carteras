

Create Procedure Trae_Reporte_Negociacion_Forma_Pagos(@ngp_codemp integer, @ngp_anio smallint, @neg_negid integer, @fpi_idid smallint) as
  SELECT formas_pago_idiomas.fpi_nombre,   
         negociacion_pagos.ngp_fechas,   
         negociacion_pagos.ngp_monto, ngp_pagdir,ngp_pagcli, mon_nombre, ngp_tipcambio   
    FROM negociacion_pagos,   
         formas_pago_idiomas, monedas  
   WHERE ( negociacion_pagos.ngp_codemp = formas_pago_idiomas.fpi_codemp ) and  
         ( negociacion_pagos.ngp_frpid = formas_pago_idiomas.fpi_frpid ) and  
         ( ( negociacion_pagos.ngp_codemp = @ngp_codemp ) AND  
         ( negociacion_pagos.ngp_anio = @ngp_anio ) AND  
         ( negociacion_pagos.ngp_negid = @neg_negid ) AND  
         ( formas_pago_idiomas.fpi_idid = @fpi_idid )   AND
         ( ngp_codemp = mon_codemp) AND
         ( ngp_codmon = mon_codmon) 
         )   
ORDER BY negociacion_pagos.ngp_fechas ASC
