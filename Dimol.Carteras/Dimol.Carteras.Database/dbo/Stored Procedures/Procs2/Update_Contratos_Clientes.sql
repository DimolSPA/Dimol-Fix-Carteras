

Create Procedure Update_Contratos_Clientes(@ctc_codemp integer, @ctc_cctid integer, @ctc_pclid numeric (15), @ctc_fecini datetime, @ctc_fecfin datetime,
										@ctc_indefinido char (1), @ctc_rut varchar (20), @ctc_nombre varchar (200),
                                        @ctc_intcli char(1), @ctc_honcli char(1)) as  
  UPDATE contratos_clientes  
     SET ctc_codemp = @ctc_codemp,   
         ctc_cctid = @ctc_cctid,   
         ctc_pclid = @ctc_pclid,   
         ctc_fecini = @ctc_fecini,   
         ctc_fecfin = @ctc_fecfin,   
         ctc_indefinido = @ctc_indefinido,   
         ctc_rut = @ctc_rut,   
         ctc_nombre = @ctc_nombre,  
         ctc_intcli = @ctc_intcli,
         ctc_honcli = @ctc_honcli
   WHERE ( contratos_clientes.ctc_codemp = @ctc_codemp ) AND  
         ( contratos_clientes.ctc_cctid = @ctc_cctid ) AND  
         ( contratos_clientes.ctc_pclid = @ctc_pclid )
