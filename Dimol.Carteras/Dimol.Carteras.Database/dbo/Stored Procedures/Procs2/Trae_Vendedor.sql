

Create Procedure Trae_Vendedor(@vde_codemp integer, @vde_sucid integer, @vde_emplid integer) as
  SELECT vendedores.vde_vdeid  
    FROM vendedores  
   WHERE ( vendedores.vde_codemp = @vde_codemp ) AND  
         ( vendedores.vde_sucid = @vde_sucid ) AND  
         ( vendedores.vde_emplid = @vde_emplid )
