

Create Procedure Update_Asientos_Contables_NumFin(@ast_codemp integer, @ast_anio integer, @ast_tipo char(1), @ast_numero integer, @ast_numfin integer) as
  UPDATE asientos_contables  
     SET ast_numfin = ast_numfin  
   WHERE ( asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( asientos_contables.ast_anio = @ast_anio ) AND  
         ( asientos_contables.ast_tipo = @ast_tipo ) AND  
         ( asientos_contables.ast_numero = @ast_numero )
