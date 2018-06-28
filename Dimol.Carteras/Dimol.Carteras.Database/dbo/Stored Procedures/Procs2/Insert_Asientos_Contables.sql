

Create procedure Insert_Asientos_Contables(@ast_codemp integer, @ast_anio integer, @ast_tipo char(1),
														@ast_numero integer, @ast_mes smallint, @ast_fecperiodo datetime, @ast_glosa text) as
  INSERT INTO asientos_contables  
         ( ast_codemp,   
           ast_anio,   
           ast_tipo,   
           ast_numero,   
           ast_numfin,   
           ast_mes,   
           ast_fecemision,   
           ast_fecperiodo,   
           ast_estado,   
           ast_glosa,   
           ast_tot_debe,   
           ast_tot_haber )  
  VALUES ( @ast_codemp,   
           @ast_anio,   
           @ast_tipo,   
           @ast_numero,   
           @ast_numero,   
           @ast_mes,   
           getdate(),   
           @ast_fecperiodo,   
           'V',   
           @ast_glosa,   
           0,   
           0 )
