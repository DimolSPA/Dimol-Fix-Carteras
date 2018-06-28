

Create Procedure Update_Negociacion_Pagos(@ngp_codemp integer, @ngp_anio smallint, @ngp_negid integer, @ngp_ngpid smallint, @ngp_frpid integer,
														@ngp_fechas datetime, @ngp_monto decimal (15,2), @ngp_pagdir char(1), @ngp_pagcli char(1),
														@ngp_codmon integer, @ngp_tipcambio decimal(15,2)) as 
  UPDATE negociacion_pagos  
     SET  ngp_frpid = @ngp_frpid,   
         ngp_fechas = @ngp_fechas,   
         ngp_monto = @ngp_monto,
         ngp_pagdir = @ngp_pagdir,
         ngp_pagcli = @ngp_pagcli,
         ngp_codmon  = @ngp_codmon,
         ngp_tipcambio = @ngp_tipcambio
   WHERE ( negociacion_pagos.ngp_codemp = @ngp_codemp ) AND  
         ( negociacion_pagos.ngp_anio = @ngp_anio ) AND  
         ( negociacion_pagos.ngp_negid = @ngp_negid ) AND  
         ( negociacion_pagos.ngp_ngpid = @ngp_ngpid )
