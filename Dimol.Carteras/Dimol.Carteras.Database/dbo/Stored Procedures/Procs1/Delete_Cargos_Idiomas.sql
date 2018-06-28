

Create Procedure Delete_Cargos_Idiomas(@cai_codemp integer, @cai_carid integer, @cai_idid integer) as  
  DELETE FROM cargos_idiomas  
   WHERE ( cargos_idiomas.cai_codemp = @cai_codemp ) AND  
         ( cargos_idiomas.cai_carid = @cai_carid ) AND  
         ( cargos_idiomas.cai_idid = @cai_idid )
