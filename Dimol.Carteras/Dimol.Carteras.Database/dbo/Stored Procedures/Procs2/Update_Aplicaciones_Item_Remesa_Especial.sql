﻿create Procedure Update_Aplicaciones_Item_Remesa_Especial(@api_codemp integer, @api_sucid integer, @api_anio integer, @api_numapl numeric(15), @api_item integer, @api_remesa char(1)) as     UPDATE aplicaciones_items         SET api_remesa = @api_remesa     WHERE ( aplicaciones_items.api_codemp = @api_codemp ) AND             ( aplicaciones_items.api_sucid = @api_sucid ) AND             ( aplicaciones_items.api_anio = @api_anio ) AND             ( aplicaciones_items.api_numapl = @api_numapl ) AND             ( aplicaciones_items.api_item = @api_item )                