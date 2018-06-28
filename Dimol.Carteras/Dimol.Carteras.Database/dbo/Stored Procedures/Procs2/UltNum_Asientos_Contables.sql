

Create Procedure UltNum_Asientos_Contables(@ast_codemp integer, @ast_anio integer, @ast_tipo char(1)) as
  SELECT IsNull(Max(ast_numero)+1, 1) 
    FROM asientos_contables  
   WHERE ( asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( asientos_contables.ast_anio = @ast_anio ) AND  
         ( asientos_contables.ast_tipo = @ast_tipo )
