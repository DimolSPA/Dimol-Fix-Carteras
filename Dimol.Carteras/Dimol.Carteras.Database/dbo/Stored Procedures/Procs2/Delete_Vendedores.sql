

Create Procedure Delete_Vendedores(@vde_codemp integer, @vde_sucid integer, @vde_vdeid integer) as  
  DELETE FROM vendedores  
   WHERE ( vendedores.vde_codemp = @vde_codemp ) AND  
         ( vendedores.vde_sucid = @vde_sucid ) AND  
         ( vendedores.vde_vdeid = @vde_vdeid )
