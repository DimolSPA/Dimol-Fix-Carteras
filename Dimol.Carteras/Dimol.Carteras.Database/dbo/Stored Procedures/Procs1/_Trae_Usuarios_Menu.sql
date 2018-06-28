CREATE Procedure [dbo].[_Trae_Usuarios_Menu](@usuario integer, @idiomas integer, @codemp integer, @menu varchar(100)) as
  SELECT treeview.trv_trvid,   
         treeview.trv_padid,   
         treeview_idiomas.tvi_idiomas, 
        CASE   trv_dimol_ventana when '' THEN '' ELSE  '/' + trv_dimol_ventana END as Ventana,  
         treeview.trv_imagen,
         men_menid 
         into #Menu
    FROM menu,   
         treeview,   
         treeview_idiomas,   
         perfiles_menu,   
         usuarios  
   WHERE ( treeview.trv_menid = menu.men_menid and tvi_idid = @idiomas ) and  
         ( treeview_idiomas.tvi_trvid = treeview.trv_trvid ) and  
         ( perfiles_menu.pfm_trvid = treeview.trv_trvid ) and  
         ( perfiles_menu.pfm_codemp = usuarios.usr_codemp ) and  
         ( perfiles_menu.pfm_prfid = usuarios.usr_prfid and usr_usrid = @usuario and pfm_codemp = @codemp )  
         and  treeview.trv_dimol_vigente = 'S'
ORDER BY menu.men_orden ASC,   
         treeview.trv_orden ASC


DECLARE @SQL varchar(600)

	
SET @SQL = 'select  * from #menu where men_menid in (' + @menu + ')'

EXEC(@SQL)
