

Create Procedure UltNum_Importacion(@imp_codemp integer) as
  SELECT IsNull(Max(imp_impid)+1, 1)
    FROM importacion  
   WHERE ( importacion.imp_codemp = @imp_codemp )
