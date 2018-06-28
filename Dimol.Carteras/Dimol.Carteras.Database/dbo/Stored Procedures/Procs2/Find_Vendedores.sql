

Create Procedure Find_Vendedores(@vde_codemp integer,  @vde_vdeid integer) as
  SELECT count(vendedores.vde_vdeid)  
    FROM vendedores  
   WHERE ( vendedores.vde_codemp = @vde_codemp ) AND  
         ( vendedores.vde_vdeid = @vde_vdeid )
