
CREATE procedure [dbo].[_Insertar_Deudores_Estampes_Insumos] (@codemp int, @pclid numeric(15), @ctcid numeric(15), @rolid int, @path varchar(300), @nombre varchar(300), @ext varchar(5), @insid numeric(15), @item int, @tpcid int, @numero numeric(15), @fecjud datetime, @usrid int) as
		
	INSERT INTO DEUDORES_ESTAMPES (DDE_CODEMP, DDE_PCLID, DDE_CTCID, DDE_ROLID, DDE_PATH, DDE_NOMBRE, DDE_EXT, DDE_INSID, DDE_ITEM, DDE_TPCID, DDE_NUMERO, DDE_FECJUD, DDE_USRID, DDE_FECING) 
	VALUES(@codemp, @pclid, @ctcid, @rolid, @path, @nombre, @ext, @insid, @item, @tpcid, @numero, @fecjud, @usrid, getdate())

	select SCOPE_IDENTITY() DDE_DDEID
