

Create Procedure Update_Deudores(@ctc_codemp integer, @ctc_ctcid numeric (15), @ctc_rut varchar (20), @ctc_numero numeric (12), 
                                                           @ctc_digito char (1), @ctc_nombre varchar (400), @ctc_apepat varchar (100), @ctc_apemat varchar (100),
                                                           @ctc_nomfant varchar (600), @ctc_comid integer, @ctc_direccion varchar (800), @ctc_partemp char (1),
                                                           @ctc_socid numeric (15), @ctc_estdir smallint, @ctc_quiebra char(1), @ctc_nacext char(1)) as  
  UPDATE deudores  
     SET ctc_rut = @ctc_rut,   
         ctc_numero = @ctc_numero,   
         ctc_digito = @ctc_digito,   
         ctc_nombre = @ctc_nombre,   
         ctc_apepat = @ctc_apepat,   
         ctc_apemat = @ctc_apemat,   
         ctc_nomfant = @ctc_nomfant,   
         ctc_comid = @ctc_comid,   
         ctc_direccion = @ctc_direccion,   
         ctc_partemp = @ctc_partemp,   
         ctc_socid = @ctc_socid,
         ctc_estdir = @ctc_estdir,
         ctc_quiebra = @ctc_quiebra,
         ctc_nacext = @ctc_nacext   
   WHERE ( deudores.ctc_codemp = @ctc_codemp ) AND  
         ( deudores.ctc_ctcid = @ctc_ctcid )
