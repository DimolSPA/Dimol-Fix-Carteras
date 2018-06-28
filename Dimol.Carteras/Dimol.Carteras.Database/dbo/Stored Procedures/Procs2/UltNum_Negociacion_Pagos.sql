

Create Procedure UltNum_Negociacion_Pagos(@ngp_codemp integer, @ngp_anio smallint, @ngp_negid integer) as
  SELECT IsNull(Max(ngp_ngpid)+1, 1)
    FROM negociacion_pagos  
   WHERE ( negociacion_pagos.ngp_codemp = @ngp_codemp ) AND  
         ( negociacion_pagos.ngp_anio = @ngp_anio ) AND  
         ( negociacion_pagos.ngp_negid = @ngp_negid )
