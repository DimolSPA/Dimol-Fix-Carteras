

Create Procedure Trae_Reporte_Hoja_Tramite_x_Deudor(@rol_codemp integer, @rol_pclid integer, @idi_idid integer, @rol_ctcid integer) as
  SELECT DISTINCT view_rol_datos.rol_numero,   
         view_rol_datos.ctc_numero,   
         view_rol_datos.ctc_digito,   
         view_rol_datos.ctc_nomfant,   
         view_rol_datos.trb_nombre,   
         view_rol_datos.rol_fecrol,   
         view_rol_datos.rol_fecdem,   
         view_rol_datos.rol_total,   
         view_rol_datos.tci_nombre,   
         view_rol_datos.sbc_rut,   
         view_rol_datos.sbc_nombre,   
         view_rol_datos.mon_nombre,   
         view_rol_datos.pcc_nombre,   
         view_rol_datos.mci_nombre,   
         view_rol_datos.trb_direccion,   
         view_rol_datos.ciu_nombre,   
         materia_judicial_idiomas.mji_nombre,   
         estados_cartera_idiomas.eci_nombre,   
         rol_estados.rle_fecjud,   
         substring(rle_comentario, 1, 20000) as Comentario,
         esj_orden,
         pcl_nomfant,
         reg_nombre,
         rol_fecultgest,
         rol_fecjud,
         ctc_nombre, ctc_apepat, ctc_apemat  
    FROM view_rol_datos,   
         rol_estados,   
         materia_judicial_idiomas,   
         estados_cartera_idiomas,   
         idiomas,
         materia_judicial  
   WHERE ( view_rol_datos.rol_codemp = rol_estados.rle_codemp ) and  
         ( view_rol_datos.rol_rolid = rol_estados.rle_rolid ) and  
         ( rol_estados.rle_codemp = materia_judicial_idiomas.mji_codemp ) and  
         ( rol_estados.rle_esjid = materia_judicial_idiomas.mji_esjid ) and  
         ( materia_judicial_idiomas.mji_codemp = esj_codemp ) and  
         ( materia_judicial_idiomas.mji_esjid = esj_esjid ) and  
         ( rol_estados.rle_codemp = estados_cartera_idiomas.eci_codemp ) and  
         ( rol_estados.rle_estid = estados_cartera_idiomas.eci_estid ) and  
         ( materia_judicial_idiomas.mji_idid = idiomas.idi_idid ) and  
         ( estados_cartera_idiomas.eci_idid = idiomas.idi_idid ) and  
         ( view_rol_datos.eci_idid = idiomas.idi_idid ) and  
         ( view_rol_datos.tci_idid = idiomas.idi_idid ) and  
         ( view_rol_datos.mji_idid = idiomas.idi_idid ) and  
         ( view_rol_datos.mci_idid = idiomas.idi_idid ) and  
         ( ( view_rol_datos.rol_codemp = @rol_codemp ) AND  
         ( view_rol_datos.rol_pclid = @rol_pclid ) AND  
         ( idiomas.idi_idid = @idi_idid and rol_ctcid = @rol_ctcid )   and esj_esjid > 0
         )
