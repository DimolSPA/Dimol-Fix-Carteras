

Create Procedure Insertar_Motivo_Cobranza(@mtc_codemp integer, @mtc_mtcid integer, @mtc_nombre varchar (80)) as 
  INSERT INTO motivo_cobranza  
         ( mtc_codemp,   
           mtc_mtcid,   
           mtc_nombre )  
  VALUES ( @mtc_codemp,   
           @mtc_mtcid,   
           @mtc_nombre )
