﻿CREATE PROCEDURE _Obt_Ult_Conciliacion_NumComprobante
As
Begin
	SELECT IsNull(Max(NUM_COMPROBANTE)+1, 1) NumComprobante
							FROM CONCILIACION_MOVIMIENTOS_DOCUMENTOS
End