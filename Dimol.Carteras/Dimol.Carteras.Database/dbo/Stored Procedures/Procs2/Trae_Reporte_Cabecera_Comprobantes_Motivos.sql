

Create Procedure Trae_Reporte_Cabecera_Comprobantes_Motivos(@cbm_codemp integer, @cbm_sucid integer, @cbm_tpcid integer, @cbm_numero integer, @tmi_idid integer, @cbm_ctcid as integer) as
  SELECT tipos_motivos_castigos_idiomas.tmi_nombre  
    FROM cabacera_comprobantes_motivos_castigo,   
         tipos_motivos_castigos_idiomas  
   WHERE ( cabacera_comprobantes_motivos_castigo.cbm_codemp = tipos_motivos_castigos_idiomas.tmi_codemp ) and  
         ( cabacera_comprobantes_motivos_castigo.cbm_tmcid = tipos_motivos_castigos_idiomas.tmi_tmcid ) and  
         ( ( cabacera_comprobantes_motivos_castigo.cbm_codemp = @cbm_codemp ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_sucid = @cbm_sucid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tpcid = @cbm_tpcid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_numero = @cbm_numero ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_idid = @tmi_idid and cbm_ctcid is null )   
         )   

union


  SELECT tipos_motivos_castigos_idiomas.tmi_nombre  
    FROM cabacera_comprobantes_motivos_castigo,   
         tipos_motivos_castigos_idiomas  
   WHERE ( cabacera_comprobantes_motivos_castigo.cbm_codemp = tipos_motivos_castigos_idiomas.tmi_codemp ) and  
         ( cabacera_comprobantes_motivos_castigo.cbm_tmcid = tipos_motivos_castigos_idiomas.tmi_tmcid ) and  
         ( ( cabacera_comprobantes_motivos_castigo.cbm_codemp = @cbm_codemp ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_sucid = @cbm_sucid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tpcid = @cbm_tpcid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_numero = @cbm_numero ) AND  
         ( tipos_motivos_castigos_idiomas.tmi_idid = @tmi_idid and cbm_ctcid = @cbm_ctcid  )   
         )   

ORDER BY tipos_motivos_castigos_idiomas.tmi_nombre ASC
