CREATE Procedure [dbo].[_Insertar_Talonario](@tac_codemp integer, @tac_tacid integer, @tac_nombre varchar(150), @tac_numero numeric(15)) as
	
  set @tac_tacid = (select IsNull(Max(tac_tacid)+1, 1) from talonario_cpbtdoc where tac_codemp = @tac_codemp)
  INSERT INTO talonario_cpbtdoc  
         ( tac_codemp,   
           tac_tacid,   
           tac_nombre,   
           tac_numero )  
  VALUES ( @tac_codemp,   
           @tac_tacid,   
           @tac_nombre,   
           @tac_numero )
