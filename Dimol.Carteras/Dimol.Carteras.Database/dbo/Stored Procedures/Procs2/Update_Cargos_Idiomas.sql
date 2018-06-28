

Create Procedure Update_Cargos_Idiomas(@cai_codemp integer, @cai_carid integer, @cai_idid integer, @cai_nombre varchar (80)) as  
  UPDATE cargos_idiomas  
     SET cai_nombre = @cai_nombre  
   WHERE ( cargos_idiomas.cai_codemp = @cai_codemp ) AND  
         ( cargos_idiomas.cai_carid = @cai_carid ) AND  
         ( cargos_idiomas.cai_idid = @cai_idid )
