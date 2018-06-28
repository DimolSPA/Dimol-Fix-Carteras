

Create Procedure Insertar_Talonario_CpbtDoc(@tac_codemp integer, @tac_tacid integer, @tac_nombre varchar(150), @tac_numero numeric(15)) as
  INSERT INTO talonario_cpbtdoc  
         ( tac_codemp,   
           tac_tacid,   
           tac_nombre,   
           tac_numero )  
  VALUES ( @tac_codemp,   
           @tac_tacid,   
           @tac_nombre,   
           @tac_numero )
