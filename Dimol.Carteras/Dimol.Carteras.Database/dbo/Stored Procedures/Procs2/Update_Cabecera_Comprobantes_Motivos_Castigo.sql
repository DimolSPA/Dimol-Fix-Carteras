

Create Procedure Update_Cabecera_Comprobantes_Motivos_Castigo(@cbm_codemp integer, @cbm_sucid integer, @cbm_tpcid integer, 
																					@cbm_numero numeric (15), @cbm_tmcid integer) as
  UPDATE cabacera_comprobantes_motivos_castigo  
     SET cbm_codemp = @cbm_codemp,   
         cbm_sucid = @cbm_sucid,   
         cbm_tpcid = @cbm_tpcid,   
         cbm_numero = @cbm_numero,   
         cbm_tmcid = @cbm_tmcid  
   WHERE ( cabacera_comprobantes_motivos_castigo.cbm_codemp = @cbm_codemp ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_sucid = @cbm_sucid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tpcid = @cbm_tpcid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_numero = @cbm_numero ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tmcid = @cbm_tmcid )
