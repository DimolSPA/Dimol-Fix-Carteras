-- =============================================
-- Author:		FMO
-- Create date: 23-05-2014
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Impuesto_Comprobante] (@codemp int,@sucid int, @tpcid int, @numero int,@retenido varchar(1))
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ipt_nombre Nombre,   
	ipt_retenido Retenido,   
	ipt_monto Porcentaje
	FROM cabacera_comprobantes,    
	impuestos,   
	provcli_impuestos
	WHERE  pci_codemp = cbc_codemp  and  
	pci_pclid = cbc_pclid  and  
	pci_codemp = ipt_codemp  and  
	pci_iptid = ipt_iptid  and   
	cbc_codemp =  @codemp and
	cbc_sucid =  @sucid and
	cbc_tpcid =  @tpcid and
	cbc_numero =  @numero

	and ipt_retenido = @retenido
			
END
