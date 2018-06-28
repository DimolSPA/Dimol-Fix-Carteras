

Create Procedure Update_Cartera_Clientes_Cpbt_Doc(@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_ccbid integer,
                                                                                        @ccb_tpcid integer, @ccb_tipcart smallint, @ccb_numero varchar (30), 
                                                                                        @ccb_fecdoc datetime, @ccb_fecvenc datetime, 
                                                                                        @ccb_codmon integer, @ccb_tipcambio decimal (10,2), @ccb_asignado decimal (15,2), @ccb_monto decimal (15,2),
                                                                                        @ccb_saldo decimal (15,2), @ccb_gastjud decimal (15,2), @ccb_gastotro decimal (15,2),@ccb_bcoid integer, @ccb_rutgir varchar (20),
                                                                                        @ccb_nomgir varchar (200), @ccb_mtcid integer, @ccb_comentario text, 
                                                                                        @ccb_codid integer, @ccb_numesp varchar (40), @ccb_numagrupa varchar (40), 
                                                                                        @ccb_cctid integer, @ccb_sbcid integer, @ccb_docori char(1), @ccb_docant char(1)) as  
  UPDATE cartera_clientes_cpbt_doc  
     SET ccb_tpcid = @ccb_tpcid,   
         ccb_tipcart = @ccb_tipcart,   
         ccb_numero = @ccb_numero,   
         ccb_fecdoc = @ccb_fecdoc,   
         ccb_fecvenc = @ccb_fecvenc,   
         ccb_codmon = @ccb_codmon,   
         ccb_tipcambio = @ccb_tipcambio,   
         ccb_asignado = @ccb_asignado,   
         ccb_monto = @ccb_monto,   
         ccb_saldo = @ccb_saldo,   
         ccb_gastjud = @ccb_gastjud,   
         ccb_gastotro = @ccb_gastotro,   
         ccb_bcoid = @ccb_bcoid,   
         ccb_rutgir = @ccb_rutgir,   
         ccb_nomgir = @ccb_nomgir,   
         ccb_mtcid = @ccb_mtcid,   
         ccb_comentario = @ccb_comentario,   
         ccb_codid = @ccb_codid,   
         ccb_numesp = @ccb_numesp,   
         ccb_numagrupa = @ccb_numagrupa,   
         ccb_cctid = @ccb_cctid,   
         ccb_sbcid = @ccb_sbcid,
         ccb_docori = @ccb_docori,
         ccb_docant = @ccb_docant  
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
