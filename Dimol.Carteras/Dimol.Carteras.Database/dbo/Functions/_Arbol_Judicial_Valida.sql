

CREATE FUNCTION [dbo].[_Arbol_Judicial_Valida] 
(@cliente varchar(400),
@rol varchar(20),
@matjud varchar(150),
@estrol varchar(350),
@codcarga varchar(120),
@abogado varchar(100),
@quiebra char(1),
@prequiebra char(1)
)
RETURNS int
AS
BEGIN

declare 
@valido INT;

if @cliente = 'COOPEUCH LTDA' and @codcarga = 'MUTUO HIPOTECARIO' and @abogado = 'FRANCISCO JAVIER'
	begin
		set @valido = 1
	end	
else
	begin
	if @quiebra = 'S'	
		begin
			set @valido = 0
		end	
	else
		begin
			if @rol =''
				begin
					set @valido = 0	
				end
			else
				begin
					if @prequiebra = 'S' and (@matjud = 'PROCEDIMIENTO CONCURSAL DE LIQUIDACION' or @matjud = 'QUIEBRA') and not (@estrol = 'A LA ESPERA DE DECLARACION DE LIQUIDACION FORZOSA' or @estrol = 'A LA ESPERA DE DECLARACIÓN DE QUIEBRA DEL DEUDOR' or @estrol = 'A LA ESPERA DE INFORMACION DEL CLIENTE' or @estrol = 'AVENIMIENTO' or @estrol = 'CAUSA SUSPENDIDA' or @estrol = 'CAUSA TERMINADA' or @estrol = 'DEUDOR CON ACUERDO DE PAGO' or (@estrol = 'SOLICITUD CASTIGO' and (@cliente = 'ASEGURADORA MAGALLANES' or @cliente = 'COPEC S. A.' or @cliente = 'CLINICA REÑACA' or @cliente = 'ORIENCOOP LTDA.')))
						begin
							set @valido = 1
						end
					else
						begin
							if @matjud = 'QUIEBRA' or @matjud = 'CONVENIO JUDICIAL PREVENTIVO' or @matjud = 'PROCEDIMIENTO CONCURSAL DE LIQUIDACION' or @matjud = 'PROCEDIMENTO CONCURSAL DE REORGANIZACION' or @estrol = 'CASTIGO JUDICIAL'
								begin
									set @valido = 0
								end
							else
								begin
								if @estrol = 'AVENIMIENTO' or @estrol = 'DEUDOR CON ACUERDO DE PAGO' or @estrol = 'CAUSA SUSPENDIDA' or @estrol = 'CAUSA TERMINADA' or @estrol = 'A LA ESPERA DE INFORMACION DEL CLIENTE' or @estrol = 'A LA ESPERA DE DECLARACIÓN DE QUIEBRA DEL DEUDOR' or @estrol = 'A LA ESPERA DE DECLARACION DE LIQUIDACION FORZOSA'
									begin	
										set @valido = 0
									end
								else
									begin
									if (@cliente = 'CLINICA REÑACA' or @cliente = 'ASEGURADORA MAGALLANES' or @cliente = 'COPEC S. A.' or @cliente = 'ORIENCOOP LTDA.') and (@estrol = 'SOLICITUD CASTIGO')
										begin	
											set @valido = 0
										end
									else
										begin
										if (@cliente = 'COOPEUCH LTDA') and (@codcarga = 'EXHORTO' or @codcarga = 'CONTRATO 2015' or @codcarga = 'TARJETA DE CRÉDITO' or @codcarga = '' or @codcarga = 'CORFO' or @codcarga = 'MYPE' or @codcarga = 'SOBRE $10 MM') 
											begin
												set @valido = 0
											end
										else
											begin
											if @cliente = 'MULTIMAQ LTDA.' 
												begin
													set @valido = 0
												end
											else
												begin
													set @valido = 1
												end
											end
										end
									end
								end
						end		
				end
		end		
	end

RETURN @valido
END

