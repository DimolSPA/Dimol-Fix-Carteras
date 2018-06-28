

Create Procedure UltNum_Vendedores(@vde_codemp integer) as
  SELECT IsNull(Max(vde_vdeid)+1, 1) 
    FROM vendedores  
   WHERE ( vendedores.vde_codemp = @vde_codemp )
