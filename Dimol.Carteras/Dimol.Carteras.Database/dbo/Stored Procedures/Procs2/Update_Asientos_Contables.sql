

Create procedure Update_Asientos_Contables(@ast_codemp integer, @ast_anio integer, @ast_tipo char(1),
														  @ast_numero integer, @ast_mes smallint, @ast_fecperiodo datetime,
														  @ast_estado char(1), @ast_glosa text, @ast_debe decimal(15,2), @ast_haber decimal(15,2) ) as

  UPDATE asientos_contables  
     SET ast_mes = @ast_mes,   
         ast_fecperiodo = @ast_fecperiodo,   
         ast_estado = @ast_estado,   
         ast_glosa = @ast_glosa,   
         ast_tot_debe = @ast_debe,   
         ast_tot_haber = @ast_haber  
   WHERE ( asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( asientos_contables.ast_anio = @ast_anio ) AND  
         ( asientos_contables.ast_tipo = @ast_tipo ) AND  
         ( asientos_contables.ast_numero = @ast_numero )
