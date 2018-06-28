

create Procedure Find_Categorias(@cat_codemp integer, @cat_catid integer) as  
select count(cat_catid)
 from categorias  
 where cat_codemp = @cat_codemp and  
       cat_catid  = @cat_catid
