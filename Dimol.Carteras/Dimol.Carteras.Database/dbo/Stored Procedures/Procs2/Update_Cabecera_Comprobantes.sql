

Create Procedure Update_Cabecera_Comprobantes(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric (15),
																@cbc_numprovcli varchar (20), @cbc_pclid numeric (15),  @cbc_feccpbt datetime,
																@cbc_fecvenc datetime, @cbc_fecent datetime, @cbc_codmon integer, @cbc_tipcambio decimal (15,2),
																@cbc_frpid integer, @cbc_anio integer, @cbc_mes numeric (2), @cbc_glosa varchar (250), 
																@cbc_porcdesc decimal (8,2), @cbc_ordcomp varchar (20), @cbt_gastjud char (1), 
																@cbt_vdeid integer, @cbt_tntid integer, 
                                                                @cbt_tgdid integer, @cbt_ttlid integer, @cbc_pcsid integer, 
                                                                @cbc_feccont datetime, @cbc_fecoc datetime) as
  UPDATE cabacera_comprobantes  
     SET cbc_codemp = @cbc_codemp,   
         cbc_sucid = @cbc_sucid,   
         cbc_tpcid = @cbc_tpcid,   
         cbc_numero = @cbc_numero,   
         cbc_numprovcli = @cbc_numprovcli,   
         cbc_pclid = @cbc_pclid,   
         cbc_feccpbt = @cbc_feccpbt,   
         cbc_fecvenc = @cbc_fecvenc,   
         cbc_fecent = @cbc_fecent,   
         cbc_codmon = @cbc_codmon,   
         cbc_tipcambio = @cbc_tipcambio,   
         cbc_frpid = @cbc_frpid,   
         cbc_anio = @cbc_anio,   
         cbc_mes = @cbc_mes,   
         cbc_glosa = @cbc_glosa,   
         cbc_porcdesc = @cbc_porcdesc,   
         cbc_ordcomp = @cbc_ordcomp,   
         cbt_gastjud = @cbt_gastjud,   
         cbt_vdeid = @cbt_vdeid,
         cbt_tntid = @cbt_tntid,
         cbt_tgdid = @cbt_tgdid,
         cbt_ttlid = @cbt_ttlid,
         cbc_pcsid = @cbc_pcsid,
         cbc_feccont = @cbc_feccont,
         cbc_fecoc = @cbc_fecoc
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )
