

Create Procedure Insertar_Clausulas_ContCart_Rangos(@clr_codemp integer, @clr_clcid integer, @clr_clrid integer, @clr_regid integer, @clr_desde decimal (15,2),
																	@clr_hasta decimal (15,2), @clr_fecini datetime, @clr_fecfin datetime, @clr_valor decimal(15,3)) as
  INSERT INTO clausulas_contcart_rangos  
         ( clr_codemp,   
           clr_clcid,   
           clr_clrid,   
           clr_regid,   
           clr_desde,   
           clr_hasta,   
           clr_fecini,   
           clr_fecfin,
           clr_valor )  
  VALUES ( @clr_codemp,   
           @clr_clcid,   
           @clr_clrid,   
           @clr_regid,   
           @clr_desde,   
           @clr_hasta,   
           @clr_fecini,   
           @clr_fecfin,
           @clr_valor )
