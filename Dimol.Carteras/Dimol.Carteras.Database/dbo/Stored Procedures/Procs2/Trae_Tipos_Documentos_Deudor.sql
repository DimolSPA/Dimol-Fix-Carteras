

Create Procedure Trae_Tipos_Documentos_Deudor(@clb_codemp integer, @tci_idid integer) as
  SELECT tipos_cpbtdoc_idiomas.tci_tpcid,   
         tipos_cpbtdoc_idiomas.tci_nombre  
    FROM clasificacion_cpbtdoc,   
         tipos_cpbtdoc,   
         tipos_cpbtdoc_idiomas  
   WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  
          tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  
          tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp  and  
          tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid  and  
           clasificacion_cpbtdoc.clb_codemp = @clb_codemp  AND  
          clasificacion_cpbtdoc.clb_cartcli = 'S'  AND  
          clasificacion_cpbtdoc.clb_selapl = 'N'  AND  
          clasificacion_cpbtdoc.clb_aplica = 'N'  AND  
          clasificacion_cpbtdoc.clb_findeuda = 'N'    AND
          tci_idid = @tci_idid
          
          order by tci_nombre
