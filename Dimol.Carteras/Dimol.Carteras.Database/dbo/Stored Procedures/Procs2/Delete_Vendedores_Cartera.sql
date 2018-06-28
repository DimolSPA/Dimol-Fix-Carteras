

Create Procedure Delete_Vendedores_Cartera(@vdc_codemp integer, @vdc_sucid integer, @vdc_vdeid integer, @vdc_pclid numeric (15)) as  
  DELETE FROM vendedores_cartera  
   WHERE ( vendedores_cartera.vdc_codemp = @vdc_codemp ) AND  
         ( vendedores_cartera.vdc_sucid = @vdc_sucid ) AND  
         ( vendedores_cartera.vdc_vdeid = @vdc_vdeid ) AND  
         ( vendedores_cartera.vdc_pclid = @vdc_pclid )
