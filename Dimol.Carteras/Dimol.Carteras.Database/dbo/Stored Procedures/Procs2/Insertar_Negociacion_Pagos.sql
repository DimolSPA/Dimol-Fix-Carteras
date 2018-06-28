

Create Procedure Insertar_Negociacion_Pagos(@ngp_codemp integer, @ngp_anio smallint, @ngp_negid integer, @ngp_ngpid smallint, @ngp_frpid integer, 
														@ngp_fechas datetime, @ngp_monto decimal (15,2), @ngp_pagdir char(1), @ngp_pagcli char(1),
														@ngp_codmon integer, @ngp_tipcambio decimal(15,2)) as 
    INSERT INTO negociacion_pagos  
         ( ngp_codemp,   
           ngp_anio,   
           ngp_negid,   
           ngp_ngpid,   
           ngp_frpid,   
           ngp_fechas,   
           ngp_monto,
           ngp_pagdir,
           ngp_pagcli,
		ngp_codmon,
		ngp_tipcambio  )  
  VALUES ( @ngp_codemp,   
           @ngp_anio,   
           @ngp_negid,   
           @ngp_ngpid,   
           @ngp_frpid,   
           @ngp_fechas,   
           @ngp_monto,
           @ngp_pagdir,
           @ngp_pagcli,
		@ngp_codmon,
		@ngp_tipcambio )
