CREATE PROCEDURE [dbo].[_Trae_Caja_Recepcion_Documentos_Criterio](
--declare
@documentoId int,
@criterioId int
)
as
BEGIN

declare @amount decimal(15,2) = 0, @IndfacturadoNoCorresponde varchar(1) = 'N',
@IndRequiereAprueba varchar(1) = 'N', @IndCriterioAplica varchar(1) ='S', 
@AplicaSimbolo varchar(2) = '%', @AplicaValor NUMERIC(3,0)= 1, 
@montoFacturar decimal(15,2) = 0, @observaciones varchar(500) ='', @IsEditable char(1) = 'N';
	set @amount = (select MONTO_INGRESO from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId);

	select 
		@IndfacturadoNoCorresponde = FACTURADO_NOCORRESPONDE, 
		@IndRequiereAprueba = REQUIERE_APRUEBA, 
		@IndCriterioAplica = CRITERIO_APLICA, 
		@AplicaSimbolo = CRITERIO_APLICA_SIMBOLO, 
		@AplicaValor = CRITERIO_APLICA_VALOR 
	from CAJA_CRITERIO_FACTURACION 
	where CRITERIO_ID = @criterioId

	if (@IndCriterioAplica = 'S')
	begin
		if (@AplicaSimbolo = '%')
		begin
			set @montoFacturar = ROUND((@amount * @AplicaValor / 100.00),0)
			set @IsEditable = 'N'
		end
		else
		begin
			if (@AplicaSimbolo = 'UF')
			begin

				--set @montoFacturar = (select MNV_VALOR from MONEDAS_VALORES where MNV_CODMON = 2 and MNV_FECHA = dateadd(day,datediff(day,1,GETDATE()),0))
				set @montoFacturar = (select top 1 MNV_VALOR from MONEDAS_VALORES where MNV_CODMON = 2 order by MNV_FECHA desc)
				set @montoFacturar =ROUND((@montoFacturar * @AplicaValor),0)
				set @IsEditable = 'N'
			end
			else
			begin
				if (@AplicaSimbolo = '$')
				begin

					set @montoFacturar = ROUND(@AplicaValor,0)
					set @IsEditable = 'N'
				end
			end
		end
	end
	if (@IndfacturadoNoCorresponde = 'S')
		begin
			set @observaciones = 'No corresponde facturar'
			set @IsEditable = 'N'
		end
	else
	begin
		if (@IndRequiereAprueba = 'S')
		begin
			set @montoFacturar = 0
			set @observaciones = ''
			set @IsEditable = 'S'
		end
	end

	select @montoFacturar MontoFacturar,@observaciones Observaciones, @IsEditable Editable
END
