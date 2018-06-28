

Create Procedure Insertar_Contratos_Clientes(@ctc_codemp integer, @ctc_cctid integer, @ctc_pclid numeric (15), @ctc_fecini datetime, @ctc_fecfin datetime,
											 @ctc_indefinido char (1), @ctc_rut varchar (20), @ctc_nombre varchar (200),
                                             @ctc_intcli char(1), @ctc_honcli char(1)) as
  INSERT INTO contratos_clientes  
         ( ctc_codemp,   
           ctc_cctid,   
           ctc_pclid,   
           ctc_fecini,   
           ctc_fecfin,   
           ctc_indefinido,   
           ctc_rut,   
           ctc_nombre,
           ctc_intcli,
           ctc_honcli )  
  VALUES ( @ctc_codemp,   
           @ctc_cctid,   
           @ctc_pclid,   
           @ctc_fecini,   
           @ctc_fecfin,   
           @ctc_indefinido,   
           @ctc_rut,   
           @ctc_nombre,
           @ctc_intcli,
           @ctc_honcli )
