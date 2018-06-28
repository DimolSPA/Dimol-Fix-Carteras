CREATE Procedure [dbo].[_Insertar_TipoProvCli](@tpc_codemp integer, @idioma integer, @tpc_nombre varchar(80), @tpc_agrupa varchar(80)) as
declare @tpcid int

set @tpcid = (select IsNull(Max(tpc_tpcid)+1, 1) from TIPOS_PROVCLI where tpc_codemp = @tpc_codemp)

  INSERT INTO TIPOS_PROVCLI  
         ( tpc_codemp,   
           tpc_tpcid,   
           tpc_nombre,
		   tpc_agrupa)  
  VALUES ( @tpc_codemp,
		   @tpcid,
           @tpc_nombre,
		   @tpc_agrupa)
           
  INSERT INTO TIPOS_PROVCLI_IDIOMAS  
         ( tpi_codemp,   
           tpi_tpcid,   
           tpi_idid,   
           tpi_nombre )  
  VALUES ( @tpc_codemp,
           @tpcid,
		   @idioma,
           @tpc_nombre )
