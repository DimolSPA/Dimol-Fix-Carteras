﻿create Procedure Delete_Aplicaciones_Cpbt_CartCli(@api_codemp integer, @api_sucid integer, @api_tpcid integer, @api_numero numeric(15), @api_pclid integer, @api_ctcid numeric(15), @api_ccbid integer) as    DELETE FROM aplicaciones_items       WHERE ( aplicaciones_items.api_codemp = @api_codemp ) AND             ( aplicaciones_items.api_sucid = @api_sucid ) AND             ( aplicaciones_items.api_tpcid = @api_tpcid ) AND             ( aplicaciones_items.api_numero = @api_numero ) AND             ( aplicaciones_items.api_pclid = @api_pclid ) AND             ( aplicaciones_items.api_ctcid = @api_ctcid ) AND             ( aplicaciones_items.api_ccbid = @api_ccbid )                