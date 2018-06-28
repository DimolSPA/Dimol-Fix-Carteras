

Create Procedure Update_Clausulas_ContCart(@clc_codemp integer, @clc_clcid integer, @clc_nombre varchar (200), @clc_tipo smallint, @clc_porcmon char (1),
														@clc_codmon integer, @clc_valor decimal (12,3), @clc_rango char (1), @clc_tiprango smallint, @clc_prejud char(1),
														@clc_faccap char(1), @clc_facint char(1), @clc_fachon char(1),  @clc_facgpre char(1), @clc_facgjud char(1), @clc_fija char(1), @clc_anumax char(1)) as
  UPDATE clausulas_contcart  
     SET clc_codemp = @clc_codemp,   
         clc_clcid = @clc_clcid,   
         clc_nombre = @clc_nombre,   
         clc_tipo = @clc_tipo,   
         clc_porcmon = @clc_porcmon,   
         clc_codmon = @clc_codmon,   
         clc_valor = @clc_valor,   
         clc_rango = @clc_rango,   
         clc_tiprango = @clc_tiprango,  
         clc_prejud = @clc_prejud,
         clc_faccap = @clc_faccap,
         clc_facint = @clc_facint,
         clc_facgpre = @clc_facgpre,
         clc_facgjud = @clc_facgjud,
         clc_fija = @clc_fija,
         clc_anumax = @clc_anumax
   WHERE ( clausulas_contcart.clc_codemp = @clc_codemp ) AND  
         ( clausulas_contcart.clc_clcid = @clc_clcid )
