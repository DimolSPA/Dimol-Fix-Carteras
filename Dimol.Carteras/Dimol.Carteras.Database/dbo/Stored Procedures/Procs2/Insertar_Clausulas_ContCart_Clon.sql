

Create Procedure Insertar_Clausulas_ContCart_Clon(@clc_codemp integer, @clc_clcid integer, @clc_clcidnew integer, @clc_nombre varchar (200)) as

insert into clausulas_contcart
  SELECT clausulas_contcart.clc_codemp,   
         @clc_clcidnew,   
         @clc_nombre,   
         clausulas_contcart.clc_tipo,   
         clausulas_contcart.clc_porcmon,   
         clausulas_contcart.clc_codmon,   
         clausulas_contcart.clc_valor,   
         clausulas_contcart.clc_rango,   
         clausulas_contcart.clc_tiprango,   
         clausulas_contcart.clc_prejud,
         clc_faccap,  
         clc_facint,
         clc_fachon,
         clc_facgpre,
         clc_facgjud,
         clc_fija,
         clc_anumax
    FROM clausulas_contcart  
   WHERE ( clausulas_contcart.clc_codemp = @clc_codemp ) AND  
         ( clausulas_contcart.clc_clcid = @clc_clcid )   
           
insert into clausulas_contcart_idiomas
   SELECT clausulas_contcart_idiomas.cli_codemp,   
         @clc_clcidnew,   
         clausulas_contcart_idiomas.cli_idid,   
         @clc_nombre  
    FROM clausulas_contcart_idiomas  
   WHERE ( clausulas_contcart_idiomas.cli_codemp =@clc_codemp ) AND  
         ( clausulas_contcart_idiomas.cli_clcid =  @clc_clcid )   


insert into clausulas_contcart_rangos
  SELECT clausulas_contcart_rangos.clr_codemp,   
          @clc_clcidnew,   
         clausulas_contcart_rangos.clr_clrid,   
         clausulas_contcart_rangos.clr_regid,   
         clausulas_contcart_rangos.clr_desde,   
         clausulas_contcart_rangos.clr_hasta,   
         clausulas_contcart_rangos.clr_fecini,   
         clausulas_contcart_rangos.clr_fecfin,   
         clausulas_contcart_rangos.clr_valor  
    FROM clausulas_contcart_rangos  
   WHERE ( clausulas_contcart_rangos.clr_codemp = @clc_codemp ) AND  
         ( clausulas_contcart_rangos.clr_clcid = @clc_clcid )
