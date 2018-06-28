

Create Procedure Update_Talonario_CpbtDoc(@tac_codemp integer, @tac_tacid integer, @tac_nombre varchar(150), @tac_numero numeric(15)) as
   UPDATE talonario_cpbtdoc  
     SET tac_nombre = @tac_nombre,   
         tac_numero = @tac_numero  
   WHERE ( talonario_cpbtdoc.tac_codemp = @tac_codemp ) AND  
         ( talonario_cpbtdoc.tac_tacid = @tac_tacid )
