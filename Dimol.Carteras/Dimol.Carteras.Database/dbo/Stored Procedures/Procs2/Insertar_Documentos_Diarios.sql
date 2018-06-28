

CREATE Procedure [dbo].[Insertar_Documentos_Diarios](@ddi_codemp integer, @ddi_sucid integer, @ddi_anio smallint, @ddi_numdoc numeric (15),
                                                                              @ddi_tpcid integer, @ddi_tipmov char (1), @ddi_pclid numeric (15), @ddi_propio char (1),
                                                                              @ddi_bcoid integer, @ddi_ctacte varchar (40), @ddi_fecing varchar(20), @ddi_fecdoc varchar(20),
                                                                              @ddi_fecvenc varchar(20), @ddi_edcid integer, @ddi_numcta varchar (40), @ddi_monto decimal (15,2),
                                                                              @ddi_saldo decimal (15,2), @ddi_codmon integer, @ddi_tipcambio decimal (15,2), @ddi_titular char (1),
                                                                              @ddi_rutpag varchar (20), @ddi_nompag varchar (200), @ddi_ctcid numeric (15), @ddi_empleado char (1),
                                                                              @ddi_emplid integer, @ddi_custodia char (1), @ddi_docemp char (1), @ddi_pagdir char (1),
                                                                              @ddi_vecesdep smallint, @ddi_fechadep datetime, @ddi_depositar char (1), @ddi_rutdep varchar (20),
                                                                              @ddi_nomdep varchar (20), @ddi_nrodep varchar (20), @ddi_fecdep datetime, @ddi_pendiente char (1),
                                                                              @ddi_anioneg smallint, @ddi_negid integer, @ddi_comentario text) as 
 
 declare  @fecing datetime, @fecdoc datetime, @fecvenc datetime
 
 
BEGIN TRY  
     set @fecing = CONVERT(datetime, @ddi_fecing, 103) 
     if DATEPART(MONTH, GETDATE()) = DATEPART(MONTH,@fecing )
     begin
	     set @fecing = GETDATE() 
		 set @fecdoc = CONVERT(datetime, @ddi_fecdoc, 103) 
		 set @fecvenc = CONVERT(datetime, @ddi_fecvenc, 103)
     end 
     else 
     begin 
		 set @fecing = GETDATE() 
		 set @fecdoc = CONVERT(datetime, @ddi_fecdoc, 101) 
		 set @fecvenc = CONVERT(datetime, @ddi_fecvenc, 101)
     end
       
END TRY  
BEGIN CATCH  
     set @fecing = GETDATE() 
     set @fecdoc = CONVERT(datetime, @ddi_fecdoc, 101) 
     set @fecvenc = CONVERT(datetime, @ddi_fecvenc, 101)  
END CATCH 
 
 INSERT INTO documentos_diarios  
         ( ddi_codemp,   
           ddi_sucid,   
           ddi_anio,   
           ddi_numdoc,   
           ddi_tpcid,   
           ddi_tipmov,   
           ddi_pclid,   
           ddi_propio,   
           ddi_bcoid,   
           ddi_ctacte,   
           ddi_fecing,   
           ddi_fecdoc,   
           ddi_fecvenc,   
           ddi_edcid,   
           ddi_numcta,   
           ddi_monto,   
           ddi_saldo,   
           ddi_codmon,   
           ddi_tipcambio,   
           ddi_titular,   
           ddi_rutpag,   
           ddi_nompag,   
           ddi_ctcid,   
           ddi_empleado,   
           ddi_emplid,   
           ddi_custodia,   
           ddi_docemp,   
           ddi_pagdir,   
           ddi_vecesdep,   
           ddi_fechadep,   
           ddi_depositar,   
           ddi_rutdep,   
           ddi_nomdep,   
           ddi_nrodep,   
           ddi_fecdep,   
           ddi_pendiente,
           ddi_anioneg,
           ddi_negid,
           ddi_comentario )  
  VALUES ( @ddi_codemp,   
           @ddi_sucid,   
           @ddi_anio,   
           @ddi_numdoc,   
           @ddi_tpcid,   
           @ddi_tipmov,   
           @ddi_pclid,   
           @ddi_propio,   
           @ddi_bcoid,   
           @ddi_ctacte,   
           @fecing,   
           @fecdoc,   
           @fecvenc,   
           @ddi_edcid,   
           @ddi_numcta,   
           @ddi_monto,   
           @ddi_saldo,   
           @ddi_codmon,   
           @ddi_tipcambio,   
           @ddi_titular,   
           @ddi_rutpag,   
           @ddi_nompag,   
           @ddi_ctcid,   
           @ddi_empleado,   
           @ddi_emplid,   
           @ddi_custodia,   
           @ddi_docemp,   
           @ddi_pagdir,   
           @ddi_vecesdep,   
           @ddi_fechadep,   
           @ddi_depositar,   
           @ddi_rutdep,   
           @ddi_nomdep,   
           @ddi_nrodep,   
           @ddi_fecdep,   
           @ddi_pendiente,
           @ddi_anioneg,
		   @ddi_negid,
           @ddi_comentario )
