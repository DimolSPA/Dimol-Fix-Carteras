

Create Procedure Trae_Perfil_Menu_NoAsignado(@codemp integer, @perfil integer, @idiomas integer, @menu as varchar(100)) as
  SELECT treeview.trv_trvid,   
         treeview.trv_padid,   
         treeview_idiomas.tvi_idiomas, 
        CASE   trv_ventana when '' THEN '' ELSE '' END as Ventana,  
         treeview.trv_imagen,
         men_orden,
         trv_orden,
         men_menid  
         into #Tree  
    FROM menu,   
         treeview,   
         treeview_idiomas
   WHERE ( treeview.trv_menid = menu.men_menid and tvi_idid = @idiomas ) and  
         ( treeview_idiomas.tvi_trvid = treeview.trv_trvid ) and  
         ( trv_trvid not in (SELECT perfiles_menu.pfm_trvid FROM perfiles_menu, treeview WHERE  treeview.trv_trvid = perfiles_menu.pfm_trvid  and   treeview.trv_padid is not null   and pfm_codemp = @codemp and pfm_prfid = @perfil))



insert into #Tree 
  SELECT treeview.trv_trvid,   
         treeview.trv_padid,   
         treeview_idiomas.tvi_idiomas, 
        CASE   trv_ventana when '' THEN '' ELSE '' END as Ventana,  
         treeview.trv_imagen,
         men_orden,
         trv_orden,
         men_menid  
    FROM menu,   
         treeview,   
         treeview_idiomas
   WHERE ( treeview.trv_menid = menu.men_menid and tvi_idid = @idiomas ) and  
         ( treeview_idiomas.tvi_trvid = treeview.trv_trvid ) and  
         ( trv_trvid in (SELECT trv_padid FROM perfiles_menu, treeview WHERE  treeview.trv_trvid = perfiles_menu.pfm_trvid  and   treeview.trv_padid is not null   and pfm_codemp = @codemp and pfm_prfid = @perfil)) 


DECLARE @SQL varchar(600)

	


SET @SQL = 'select distinct * from #Tree where men_menid in (' + @menu + ') ORDER BY men_orden ASC,     trv_orden ASC '

EXEC(@SQL)
