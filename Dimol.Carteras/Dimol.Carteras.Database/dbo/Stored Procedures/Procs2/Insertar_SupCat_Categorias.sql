

Create Procedure Insertar_SupCat_Categorias(@scc_codemp integer, @scc_spcid integer, @scc_catid integer, @scc_orden smallint) as
  INSERT INTO supcat_categorias  
         ( scc_codemp,   
           scc_spcid,   
           scc_catid,   
           scc_orden )  
  VALUES ( @scc_codemp,   
           @scc_spcid,   
           @scc_catid,   
           @scc_orden )
