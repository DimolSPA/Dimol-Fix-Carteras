﻿create Procedure Delete_Cabacera_Comprobantes_OP(@cbo_codemp integer, @cbo_sucid integer, @cbo_tpcid integer, @cbo_numero numeric(15)) as    DELETE FROM cabacera_comprobantes_op       WHERE ( cabacera_comprobantes_op.cbo_codemp = @cbo_codemp ) AND             ( cabacera_comprobantes_op.cbo_sucid = @cbo_sucid ) AND             ( cabacera_comprobantes_op.cbo_tpcid = @cbo_tpcid ) AND             ( cabacera_comprobantes_op.cbo_numero = @cbo_numero )   