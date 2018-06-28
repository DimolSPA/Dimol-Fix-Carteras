CREATE Procedure [dbo].[_Insertar_Clausulas_ContCart](@clc_codemp integer, @idioma integer, @clc_nombre varchar (200), @clc_tipo smallint, @clc_porcmon char (1),
														@clc_codmon integer, @clc_valor decimal (12,3), @clc_rango char (1), @clc_tiprango smallint, @clc_prejud char(1),
														@clc_faccap char(1), @clc_facint char(1), @clc_fachon char(1), @clc_facgpre char(1), 
                                                        @clc_facgjud char(1), @clc_fija char(1), @clc_anumax char(1)) as
	declare @clc_clcid int
	
	set @clc_clcid = (select IsNull(Max(clc_clcid)+1, 1) from clausulas_contcart where clc_codemp = @clc_codemp)
		
	INSERT INTO clausulas_contcart  
         ( clc_codemp,   
           clc_clcid,   
           clc_nombre,   
           clc_tipo,   
           clc_porcmon,   
           clc_codmon,   
           clc_valor,   
           clc_rango,   
           clc_tiprango,
           clc_prejud,
           clc_faccap,
           clc_facint,
           clc_fachon,
           clc_facgpre,
           clc_facgjud,
           clc_fija,
           clc_anumax)  
  VALUES ( @clc_codemp,   
           @clc_clcid,   
           @clc_nombre,   
           @clc_tipo,   
           @clc_porcmon,   
           @clc_codmon,   
           @clc_valor,   
           @clc_rango,   
           @clc_tiprango,
           @clc_prejud,
           @clc_faccap,
		   @clc_facint,
		   @clc_fachon,
 		   @clc_facgpre,
		   @clc_facgjud,
           @clc_fija,
           @clc_anumax)
  
  INSERT INTO clausulas_contcart_idiomas  
         ( cli_codemp,   
           cli_clcid,   
           cli_idid,   
           cli_nombre )  
  VALUES ( @clc_codemp,   
           @clc_clcid,   
           @idioma,   
           @clc_nombre )
