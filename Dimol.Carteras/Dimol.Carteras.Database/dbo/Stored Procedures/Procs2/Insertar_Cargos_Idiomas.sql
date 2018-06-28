

Create Procedure Insertar_Cargos_Idiomas(@cai_codemp integer, @cai_carid integer, @cai_idid integer, @cai_nombre varchar(80)) as
  INSERT INTO cargos_idiomas  
         ( cai_codemp,   
           cai_carid,   
           cai_idid,   
           cai_nombre )  
  VALUES ( @cai_codemp,   
           @cai_carid,   
           @cai_idid,   
           @cai_nombre )
