

Create Procedure Trae_Tipos_Cpbt_NotPerfil(@tpc_codemp integer, @pfc_prfid integer, @clb_tipcpbtdoc char(1), @tci_idid integer) as
  SELECT tipos_cpbtdoc.tpc_tpcid,   
         tipos_cpbtdoc_idiomas.tci_nombre  
    FROM tipos_cpbtdoc,   
         tipos_cpbtdoc_idiomas,   
         clasificacion_cpbtdoc  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid ) and  
         ( clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid ) and  
         ( ( tipos_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc.tpc_tpcid not in (  SELECT perfiles_comprobantes.pfc_tpcid  
                                               FROM perfiles_comprobantes  
                                              WHERE ( perfiles_comprobantes.pfc_codemp = @tpc_codemp ) AND  
                                                    ( perfiles_comprobantes.pfc_prfid = @pfc_prfid )   
                                                     )) AND  
         ( clasificacion_cpbtdoc.clb_tipcpbtdoc = @clb_tipcpbtdoc ) AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid ) AND  
         ( clasificacion_cpbtdoc.clb_cartcli = 'N' )   
         )   

union

 SELECT tipos_cpbtdoc.tpc_tpcid,   
         tipos_cpbtdoc_idiomas.tci_nombre  
    FROM tipos_cpbtdoc,   
         tipos_cpbtdoc_idiomas,   
         clasificacion_cpbtdoc  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid ) and  
         ( clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid ) and  
         ( ( tipos_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc.tpc_tpcid not in (  SELECT perfiles_comprobantes.pfc_tpcid  
                                               FROM perfiles_comprobantes  
                                              WHERE ( perfiles_comprobantes.pfc_codemp = @tpc_codemp ) AND  
                                                    ( perfiles_comprobantes.pfc_prfid = @pfc_prfid )   
                                                     )) AND  
         ( clasificacion_cpbtdoc.clb_tipcpbtdoc = @clb_tipcpbtdoc ) AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid ) AND  
         ( clasificacion_cpbtdoc.clb_cartcli = 'S' and clb_selcpbt = 'S' )   
         )   

union

 SELECT tipos_cpbtdoc.tpc_tpcid,   
         tipos_cpbtdoc_idiomas.tci_nombre  
    FROM tipos_cpbtdoc,   
         tipos_cpbtdoc_idiomas,   
         clasificacion_cpbtdoc  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid ) and  
         ( clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid ) and  
         ( ( tipos_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc.tpc_tpcid not in (  SELECT perfiles_comprobantes.pfc_tpcid  
                                               FROM perfiles_comprobantes  
                                              WHERE ( perfiles_comprobantes.pfc_codemp = @tpc_codemp ) AND  
                                                    ( perfiles_comprobantes.pfc_prfid = @pfc_prfid )   
                                                     )) AND  
         ( clasificacion_cpbtdoc.clb_tipcpbtdoc = @clb_tipcpbtdoc ) AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid ) AND  
         ( clasificacion_cpbtdoc.clb_cartcli = 'S' and clb_selapl = 'S' )   
         )   

union

 SELECT tipos_cpbtdoc.tpc_tpcid,   
         tipos_cpbtdoc_idiomas.tci_nombre  
    FROM tipos_cpbtdoc,   
         tipos_cpbtdoc_idiomas,   
         clasificacion_cpbtdoc  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid ) and  
         ( clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid ) and  
         ( ( tipos_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc.tpc_tpcid not in (  SELECT perfiles_comprobantes.pfc_tpcid  
                                               FROM perfiles_comprobantes  
                                              WHERE ( perfiles_comprobantes.pfc_codemp = @tpc_codemp ) AND  
                                                    ( perfiles_comprobantes.pfc_prfid = @pfc_prfid )   
                                                     )) AND  
         ( clasificacion_cpbtdoc.clb_tipcpbtdoc = @clb_tipcpbtdoc ) AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid ) AND  
         ( clasificacion_cpbtdoc.clb_cartcli = 'S' and clb_selapl = 'N' and clb_aplica = 'S' )   
         )   

ORDER BY tipos_cpbtdoc_idiomas.tci_nombre ASC
