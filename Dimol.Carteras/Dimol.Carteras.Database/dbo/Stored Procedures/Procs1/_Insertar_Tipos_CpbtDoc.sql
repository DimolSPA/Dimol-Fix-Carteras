CREATE Procedure [dbo].[_Insertar_Tipos_CpbtDoc](@tpc_codemp integer, @tci_tpcid integer, @tpc_clbid varchar(800), @tpc_nombre varchar(800), @tpc_talonario char(1), @tpc_ultnum integer, @tpc_lineas integer, @tpc_codigo varchar(800), @tpc_tipdig varchar(800)) as
declare @tpcid int

set @tpcid = (select IsNull(Max(tpc_tpcid)+1, 1) from tipos_cpbtdoc where tpc_codemp = @tpc_codemp)

  INSERT INTO tipos_cpbtdoc  
         ( tpc_codemp,   
           tpc_tpcid,   
           tpc_clbid,   
           tpc_nombre,
		   tpc_talonario,
		   tpc_ultnum,
		   tpc_lineas,
		   tpc_codigo,
		   tpc_tipdig )  
  VALUES ( @tpc_codemp,
		   @tpcid,
           @tpc_clbid,   
           @tpc_nombre,
		   @tpc_talonario,
		   @tpc_ultnum,
		   @tpc_lineas,
		   @tpc_codigo,
		   @tpc_tipdig )

  INSERT INTO TIPOS_CPBTDOC_IDIOMAS  
         ( tci_codemp,   
           tci_tpcid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tpc_codemp,
		   @tpcid,
           @tci_tpcid,   
           @tpc_nombre
		    )
