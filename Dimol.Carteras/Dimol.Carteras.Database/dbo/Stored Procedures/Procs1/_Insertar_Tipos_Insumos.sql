CREATE Procedure [dbo].[_Insertar_Tipos_Insumos](@codemp integer, @nombre varchar(80), @idioma integer) as
declare @tpi_tipid int

set @tpi_tipid = (select IsNull(Max(tpi_tipid)+1, 1) from TIPOS_INSUMO where tpi_codemp = @codemp)

  INSERT INTO TIPOS_INSUMO  
         ( tpi_codemp,   
           tpi_tipid,   
           tpi_nombre)  
  VALUES ( @codemp,
		   @tpi_tipid,
           @nombre)
           
  INSERT INTO TIPOS_INSUMO_IDIOMAS  
         ( tii_codemp,   
           tii_tipid,   
           tii_idid,   
           tii_nombre )  
  VALUES ( @codemp,
		   @tpi_tipid,
           @idioma,   
           @nombre )
