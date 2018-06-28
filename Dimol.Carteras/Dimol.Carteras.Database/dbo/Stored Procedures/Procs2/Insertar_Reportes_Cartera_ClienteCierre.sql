

Create Procedure Insertar_Reportes_Cartera_ClienteCierre(@rcc_codemp integer, @rcc_pclid integer, @rcc_reporte varchar(300)) as
  INSERT INTO reportes_cartera_cliente_cierre  
         ( rcc_codemp,   
           rcc_pclid,   
           rcc_fecha,   
           rcc_reporte )  
  VALUES ( @rcc_codemp,   
           @rcc_pclid,   
           getdate(),   
           @rcc_reporte )
