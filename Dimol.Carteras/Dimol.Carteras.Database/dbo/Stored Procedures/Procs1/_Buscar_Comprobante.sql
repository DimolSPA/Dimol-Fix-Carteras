-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Comprobante] (@codemp int, @sucid int, @tpcid int,  @numero int)
AS
BEGIN
	SET NOCOUNT ON;
		select [cbc_codemp]
      ,[cbc_sucid]
      ,[cbc_tpcid]
      ,[cbc_numero]
      ,[cbc_numprovcli]
      ,[cbc_pclid]
      ,[cbc_fecemi]
      ,[cbc_feccpbt]
      ,[cbc_fecvenc]
      ,[cbc_fecent]
      ,[cbc_codmon]
      ,[cbc_tipcambio]
      ,[cbc_frpid]
      ,[cbc_anio]
      ,[cbc_mes]
      ,[cbc_glosa]
      ,[cbc_porcdesc]
      ,[cbc_neto]
      ,[cbc_impuestos]
      ,[cbc_retenido]
      ,[cbc_descuentos]
      ,[cbc_final]
      ,[cbc_saldo]
      ,[cbc_ordcomp]
      ,[cbt_gastjud]
      ,[cbt_vdeid]
      ,[cbt_estado]
      ,[pcl_rut]
      ,[pcl_nombre]
      ,[pcl_apepat]
      ,[pcl_apemat]
      ,[pcl_nomfant]
      ,[fpi_nombre]
      ,[tci_nombre]
      ,[idi_nombre]
      ,[mon_nombre]
      ,[idi_idid]
      ,[cbt_tntid]
      ,[cbt_tgdid]
      ,[cbt_ttlid]
      ,[cbc_exento]
      ,[cbc_pcsid]
      ,[pcl_girid]
      ,[cbc_feccont]
      ,[tpc_codigo]
      ,[cbc_fecoc] from view_cabecera_comprobantes
		where cbc_codemp =@codemp
		and cbc_sucid =@sucid
		and cbc_tpcid =@tpcid
		and cbc_numero =@numero
END
