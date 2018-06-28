CREATE PROCEDURE [dbo].[_Listar_ClasificacionDocumentos_Grilla]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT DISTINCT clb_clbid Id, clb_codemp Codemp, clb_codigo Codigo
	  ,case [CLB_TIPCPBTDOC] when ''C'' then ''Compra''
                when ''V'' then ''Venta''
                when ''T'' then ''Traspaso''
                when ''A'' then ''Ajuste''
                when ''D'' then ''Documento'' end as Tipo
	  ,clb_tipcpbtdoc TipoComprobante
	  ,clb_tipprod TipoProducto
	  ,clb_costos CostosSN
	  ,clb_selcpbt SeleccionOtroComprobanteSN
	  ,clb_cartcli CarteraClientesSN
	  ,clb_contable ContableSN
	  ,clb_selapl SeleccionaPagosSN
	  ,clb_aplica AplicaPagosSN
	  ,clb_cptoctbl Concepto
	  ,clb_findeuda FinalizaDeudaSN
	  ,clb_cancela CancelaSN
	  ,clb_libcompra TipoLibro
	  ,clb_cambiodoc CambiaDocumentoSN
	  ,clb_remesa RemesaSN
	  ,clb_tipsel TipoDocSeleccionar
	  ,clb_sinimp AnulaImpuestoSN
	  ,clb_forpag FormaPagoSN
	  ,clb_ordcomp OrdenCompraSN
	  ,cct_debhab Movimiento
	  ,cct_libcomven MostrarEnLibrosSN
	  ,cct_honorarios HonorariosSN
	  ,cct_pctid Cuenta
	  ,ccs_stock Stock
	  ,ccs_saldos SaldosSN
	  ,ccs_reserva ReservaSN
	  ,ccs_transito TransitoSN
	  FROM [dbo].[CLASIFICACION_CPBTDOC] LEFT JOIN [dbo].[CLASIFICACION_CPBTDOC_CONTABLE] ON clb_clbid = cct_clbid 
	  LEFT JOIN [dbo].[CLASIFICACION_CPBTDOC_STOCK] ON clb_clbid = ccs_clbid
	  WHERE clb_codemp = ' + CONVERT(VARCHAR,@codemp) +' 
		
    '
   set @query = @query +')as tabla ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
