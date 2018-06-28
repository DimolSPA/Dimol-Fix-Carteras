

Create Procedure Trae_Reporte_Documentos_Pendientes(@ddi_codemp integer, @ddi_sucid integer, @ddi_ctcid integer, @idioma integer, @ddi_fecing datetime) as
  SELECT pcl_rut,
			pcl_nomfant,
                ctc_rut,
                ctc_nomfant,
                tci_nombre, 
                ddi_numcta,
                ddi_fecvenc,
                mon_nombre,
                ddi_monto, 
                bco_nombre,
                ddi_tipcambio,
                ddi_rutpag,
                ddi_nompag
    FROM view_documentos_diarios  
   WHERE  ddi_codemp = @ddi_codemp  AND  
          ddi_sucid = @ddi_sucid  AND  
          ddi_ctcid = @ddi_ctcid  AND  
          datepart(year,ddi_fecing) = datepart(year,@ddi_fecing)  AND  
          datepart(month,ddi_fecing) = datepart(month,@ddi_fecing)  AND  
          datepart(day,ddi_fecing) = datepart(day,@ddi_fecing)  AND  
          tci_idid = @idioma and
          edi_idiid = @idioma and
          convert(varchar, ddi_anio) + '_' + convert(varchar, ddi_numdoc) not in  ( SELECT convert(varchar, api_aniodoc) + '_' + convert(varchar, api_numdoc)  
                                                                              FROM aplicaciones,   
                                                                                   aplicaciones_items  
                                                                             WHERE  api_codemp = apl_codemp  and  
                                                                                    api_sucid = apl_sucid  and  
                                                                                    api_anio = apl_anio  and  
                                                                                    api_numapl = apl_numapl  and  
                                                                                     apl_codemp = @ddi_codemp  AND  
                                                                                    apl_sucid = @ddi_sucid  AND  
                                                                                    datepart(year,apl_fecing) = datepart(year,@ddi_fecing)  AND  
                                                                                    datepart(month,apl_fecing) = datepart(month,@ddi_fecing)  AND  
                                                                                    datepart(day,apl_fecing) =  datepart(day,@ddi_fecing)  and  api_aniodoc is not null     )
