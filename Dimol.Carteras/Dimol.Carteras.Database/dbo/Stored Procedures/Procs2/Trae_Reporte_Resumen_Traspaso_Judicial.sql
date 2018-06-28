

Create Procedure Trae_Reporte_Resumen_Traspaso_Judicial(@cbc_codemp integer, @cbc_tpcid integer, @desde datetime, @hasta datetime, @idi_idid integer) as
  SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc  
    FROM view_cabecera_comprobantes  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( view_cabecera_comprobantes.cbc_feccpbt >= @desde ) AND  
         ( view_cabecera_comprobantes.cbc_feccpbt <= @hasta ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','F' ) ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid )
