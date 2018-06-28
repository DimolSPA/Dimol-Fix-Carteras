

Create Procedure Update_Clausulas_ContCart_Rangos(@clr_codemp integer, @clr_clcid integer, @clr_clrid integer, @clr_regid integer, @clr_desde decimal (15,2),
																	@clr_hasta decimal (15,2), @clr_fecini datetime, @clr_fecfin datetime, @clr_valor decimal(15,3)) as  
  UPDATE clausulas_contcart_rangos  
     SET clr_codemp = @clr_codemp,   
         clr_clcid = @clr_clcid,   
         clr_clrid = @clr_clrid,   
         clr_regid = @clr_regid,   
         clr_desde = @clr_desde,   
         clr_hasta = @clr_hasta,   
         clr_fecini = @clr_fecini,   
         clr_fecfin = @clr_fecfin,
         clr_valor = @clr_valor 
   WHERE ( clausulas_contcart_rangos.clr_codemp = @clr_codemp ) AND  
         ( clausulas_contcart_rangos.clr_clcid = @clr_clcid ) AND  
         ( clausulas_contcart_rangos.clr_clrid = @clr_clrid )
