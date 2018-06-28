CREATE PROCEDURE [dbo].[_Insertar_Cartera_Clientes_Cpbt_Doc_Imagenes] (@cdi_codemp integer, @cdi_pclid numeric(15), @cdi_ctcid numeric(15),
@cdi_ccbid integer, @cdi_cdid integer, @cdi_tpcid integer, @cdi_ruta_archivo varchar(1000))
AS
  INSERT INTO cartera_clientes_cpbt_doc_imagenes (cdi_codemp,
  cdi_pclid,
  cdi_ctcid,
  cdi_ccbid,
  cdi_cdid,
  cdi_tpcid,
  cdi_ruta_archivo)
    VALUES (@cdi_codemp, @cdi_pclid, @cdi_ctcid, @cdi_ccbid, @cdi_cdid, @cdi_tpcid, @cdi_ruta_archivo)
