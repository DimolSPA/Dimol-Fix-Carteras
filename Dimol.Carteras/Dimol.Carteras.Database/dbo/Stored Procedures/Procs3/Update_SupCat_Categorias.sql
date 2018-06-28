

Create Procedure Update_SupCat_Categorias(@scc_codemp integer, @scc_spcid integer, @scc_catid integer, @scc_orden smallint) as
   UPDATE supcat_categorias  
     SET scc_orden = @scc_orden  
   WHERE ( supcat_categorias.scc_codemp = @scc_codemp ) AND  
         ( supcat_categorias.scc_spcid = @scc_spcid ) AND  
         ( supcat_categorias.scc_catid = @scc_catid )
