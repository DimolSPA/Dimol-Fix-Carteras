

Create Procedure Trae_Categorias_Asociada(@scc_codemp integer, @scc_spcid integer, @cai_idiid integer, @cat_utilizacion integer) as
  SELECT categorias.cat_catid,   
         categorias_idiomas.cai_nombre,   
         supcat_categorias.scc_orden  
    FROM supcat_categorias,   
         categorias,   
         categorias_idiomas  
   WHERE ( categorias.cat_codemp = supcat_categorias.scc_codemp ) and  
         ( categorias.cat_catid = supcat_categorias.scc_catid ) and  
         ( categorias_idiomas.cai_codemp = categorias.cat_codemp ) and  
         ( categorias_idiomas.cai_catid = categorias.cat_catid ) and  
         ( ( supcat_categorias.scc_codemp = @scc_codemp ) AND  
         ( supcat_categorias.scc_spcid = @scc_spcid ) AND  
         ( categorias.cat_utilizacion in (3, @cat_utilizacion) ) AND  
         ( categorias_idiomas.cai_idiid = @cai_idiid )   
         )   
ORDER BY supcat_categorias.scc_orden ASC
