

Create Procedure Delete_Negociacion_Pagos(@ngp_codemp integer, @ngp_anio smallint, @ngp_negid integer, @ngp_ngpid smallint) as 
  DELETE FROM negociacion_pagos  
   WHERE ( negociacion_pagos.ngp_codemp = @ngp_codemp ) AND  
         ( negociacion_pagos.ngp_anio = @ngp_anio ) AND  
         ( negociacion_pagos.ngp_negid = @ngp_negid ) AND  
         ( negociacion_pagos.ngp_ngpid = @ngp_ngpid )
