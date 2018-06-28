
CREATE Procedure [dbo].[_Trae_Reporte_Hoja_Tramite](@rol_codemp integer, @rol_pclid integer, @idi_idid integer, @ctc_id numeric, @rol varchar(20)) as 
--CREATE Procedure [dbo].[_Trae_Reporte_Hoja_Tramite](@rol_codemp integer, @rol_pclid integer, @idi_idid integer, @ctc_id numeric, @ect_prejud char(1)) as
BEGIN
	
	SET NOCOUNT ON;

	declare @query varchar(8000);

	set @ctc_id = isnull(@ctc_id, 0)

    set @query = 'SELECT DISTINCT rol.rol_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,
		 cartera_clientes_documentos_cpbt_doc.ctc_direccion,
         tribunales.trb_nombre,   
         rol.rol_fecrol,   
         rol.rol_fecdem,   
         rol.rol_total,   
         tipos_causa_idiomas.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,
		 cartera_clientes_documentos_cpbt_doc.ccb_codmon,		 
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.mci_nombre,   
         tribunales.trb_direccion,   
         view_datos_geograficos.ciu_nombre,   
         materia_judicial_idiomas.mji_nombre,   
         estados_cartera_idiomas.eci_nombre,   
         rol_estados.rle_fecjud,   
         substring(REPLACE(REPLACE(CONVERT(VARCHAR(8000),rle_comentario), CHAR(10), '' ''), CHAR(13),'' ''), 1, 2000) as Comentario,
         esj_orden,
         pcl_nomfant,
         reg_nombre,
         rol_fecultgest,
         rol_fecjud       
	FROM rol,  
		 tribunales, 
		 rol_documentos,   
         cartera_clientes_documentos_cpbt_doc, 
         rol_estados,   
         materia_judicial_idiomas,   
         estados_cartera_idiomas,   
         idiomas,			
         materia_judicial,			
		 view_datos_geograficos,
		 tipos_causa_idiomas			 
    WHERE ( rol.rol_codemp = tipos_causa_idiomas.tci_codemp ) and  
         ( rol.rol_tcaid = tipos_causa_idiomas.tci_tcaid ) and  
		 ( rol.ROL_CODEMP = tribunales.TRB_CODEMP) and  
		 ( rol.ROL_TRBID = tribunales.TRB_TRBID) and  
		 ( rol.rol_codemp = rol_documentos.rdc_codemp ) and  
         ( rol.rol_rolid = rol_documentos.rdc_rolid ) and  
		 ( rol.ROL_PCLID = rol_documentos.RDC_PCLID ) and  
		 ( rol.ROL_CTCID = rol_documentos.RDC_CTCID ) and  
		 ( rol.rol_codemp = rol_estados.rle_codemp ) and  
         ( rol.rol_rolid = rol_estados.rle_rolid ) and  
         ( rol_documentos.rdc_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( rol_documentos.rdc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( rol_documentos.rdc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( rol_documentos.rdc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
		 ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos.com_comid ) and  		 
         ( rol_estados.rle_codemp = materia_judicial_idiomas.mji_codemp ) and  
         ( rol_estados.rle_esjid = materia_judicial_idiomas.mji_esjid ) and  
         ( materia_judicial_idiomas.mji_codemp = materia_judicial.esj_codemp ) and  
         ( materia_judicial_idiomas.mji_esjid = materia_judicial.esj_esjid ) and  
         ( rol_estados.rle_codemp = estados_cartera_idiomas.eci_codemp ) and  
         ( rol_estados.rle_estid = estados_cartera_idiomas.eci_estid ) and  
         ( materia_judicial_idiomas.mji_idid = idiomas.idi_idid ) and  
         ( estados_cartera_idiomas.eci_idid = idiomas.idi_idid ) and  
         ( ( rol.rol_codemp = '  + CONVERT(VARCHAR, @rol_codemp) + ' ) AND ' 
		 

		-- if @ect_prejud <> 'T' 
		--	begin
		--		set @query = @query + ' ( cartera_clientes_documentos_cpbt_doc.ect_prejud = '  + CONVERT(VARCHAR, @ect_prejud) + ' ) AND '
		--	end
			
		 if @ctc_id <> 0 
			begin
				set @query = @query + '( rol.rol_ctcid = '  + CONVERT(VARCHAR, @ctc_id) + ' ) AND '
			end
			
         if @rol <> 'null' and @rol <> ''
			begin
				set @query = @query + '( rol.rol_numero = ''' +  @rol + ''' ) AND '
			end
		 
		 set @query = @query + '( rol.rol_pclid = '  + CONVERT(VARCHAR, @rol_pclid) + ' ) AND ' 
         set @query = @query + '( idiomas.idi_idid = '  + CONVERT(VARCHAR, @idi_idid) + ' ) AND '
		 set @query = @query + '( materia_judicial.esj_esjid > 0 )
         )
	order by rol_estados.rle_fecjud	' 	 

	exec(@query)
	
END
