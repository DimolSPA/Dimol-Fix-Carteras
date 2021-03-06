﻿

Create Procedure Insertar_Cabecera_Comprobantes_Especial(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric (15),
																@cbc_numprovcli varchar (20), @cbc_pclid numeric (15), @cbc_feccpbt datetime,
																@cbc_fecvenc datetime, @cbc_fecent datetime, @cbc_codmon integer, @cbc_tipcambio decimal (15,2),
																@cbc_frpid integer, @cbc_anio integer, @cbc_mes numeric (2), @cbc_glosa varchar (250), 
																@cbc_porcdesc decimal (8,2),  @cbc_ordcomp varchar (20), @cbt_gastjud char (1), 
																@cbt_vdeid integer, @cbc_fecemi datetime, @cbc_pcsid integer) as
  INSERT INTO cabacera_comprobantes  
         ( cbc_codemp,   
           cbc_sucid,   
           cbc_tpcid,   
           cbc_numero,   
           cbc_numprovcli,   
           cbc_pclid,   
           cbc_fecemi,   
           cbc_feccpbt,   
           cbc_fecvenc,   
           cbc_fecent,   
           cbc_codmon,   
           cbc_tipcambio,   
           cbc_frpid,   
           cbc_anio,   
           cbc_mes,   
           cbc_glosa,   
           cbc_porcdesc,   
           cbc_neto,   
           cbc_impuestos,   
           cbc_retenido,   
           cbc_descuentos,   
           cbc_final,   
           cbc_saldo,   
           cbc_ordcomp,   
           cbt_gastjud,   
           cbt_vdeid,   
           cbt_estado,
           cbc_pcsid )  
  VALUES ( @cbc_codemp,   
           @cbc_sucid,   
           @cbc_tpcid,   
           @cbc_numero,   
           @cbc_numprovcli,   
           @cbc_pclid,   
           @cbc_fecemi,   
           @cbc_feccpbt,   
           @cbc_fecvenc,   
           @cbc_fecent,   
           @cbc_codmon,   
           @cbc_tipcambio,   
           @cbc_frpid,   
           @cbc_anio,   
           @cbc_mes,   
           @cbc_glosa,   
           @cbc_porcdesc,   
           0,   
           0,   
           0,   
           0,   
           0,   
           0,   
           @cbc_ordcomp,   
           @cbt_gastjud,   
           @cbt_vdeid,   
           'E',
           @cbc_pcsid )
