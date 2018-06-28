

Create Procedure Trae_Categorias_NotAsociada(@scc_codemp integer, @scc_spcid integer, @cai_idiid integer, @cat_utilizacion integer) as
  SELECT categorias.cat_catid,   
         categorias_idiomas.cai_nombre,   
         categorias.cat_utilizacion  
    FROM categorias,   
         categorias_idiomas  
   WHERE ( categorias_idiomas.cai_codemp = categorias.cat_codemp ) and  
         ( categorias_idiomas.cai_catid = categorias.cat_catid ) and  
         ( ( categorias.cat_codemp = @scc_codemp ) AND  
         ( categorias_idiomas.cai_idiid = @cai_idiid ) AND  
         ( categorias.cat_utilizacion in (3, @cat_utilizacion) ) AND  
         ( categorias.cat_catid not in (  SELECT supcat_categorias.scc_catid  
                                            FROM supcat_categorias  
                                           WHERE ( supcat_categorias.scc_codemp = @scc_codemp ) AND  
                                                 ( supcat_categorias.scc_spcid = @scc_spcid )   
                                                  )) )   
ORDER BY categorias_idiomas.cai_nombre ASC
