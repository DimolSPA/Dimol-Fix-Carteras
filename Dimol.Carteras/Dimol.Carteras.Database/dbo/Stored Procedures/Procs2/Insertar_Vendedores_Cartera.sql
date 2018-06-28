

Create Procedure Insertar_Vendedores_Cartera(@vdc_codemp integer, @vdc_sucid integer, @vdc_vdeid integer, @vdc_pclid numeric (15)) as
  INSERT INTO vendedores_cartera  
         ( vdc_codemp,   
           vdc_sucid,   
           vdc_vdeid,   
           vdc_pclid,
           vdc_fecasig )  
  VALUES ( @vdc_codemp,   
           @vdc_sucid,   
           @vdc_vdeid,   
           @vdc_pclid,
           getdate() )
