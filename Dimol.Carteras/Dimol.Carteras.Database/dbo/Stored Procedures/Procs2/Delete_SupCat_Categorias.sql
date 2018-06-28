

Create Procedure Delete_SupCat_Categorias(@scc_codemp integer, @scc_spcid integer, @scc_catid integer) as
   DELETE FROM supcat_categorias  
   WHERE ( supcat_categorias.scc_codemp = @scc_codemp ) AND  
         ( supcat_categorias.scc_spcid = @scc_spcid ) AND  
         ( supcat_categorias.scc_catid = @scc_catid )
