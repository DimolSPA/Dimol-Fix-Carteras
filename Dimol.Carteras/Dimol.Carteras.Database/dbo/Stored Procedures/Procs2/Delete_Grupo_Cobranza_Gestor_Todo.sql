﻿

Create Procedure Delete_Grupo_Cobranza_Gestor_Todo(@gcg_codemp integer, @gcg_sucid integer, @gcg_gesid integer) as
 DELETE FROM grupo_cobranza_gestor  
   WHERE ( grupo_cobranza_gestor.gcg_codemp = @gcg_codemp ) AND  
         ( grupo_cobranza_gestor.gcg_sucid = @gcg_sucid ) AND  
         ( grupo_cobranza_gestor.gcg_gesid = @gcg_gesid )
