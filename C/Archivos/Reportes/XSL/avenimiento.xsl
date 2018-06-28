<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="BorradorJudicial">
		<html lang="en">
			<head>
				<meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"/>
				<title/>
				<style>
				.tabla_certificado {
					border: 0px;
					height: 100%;
					width: 100%;
					cellpadding: 0;
					cellspacing: 0;
				}
				</style>
			</head>
			<body>
				<table  class="tabla_certificado">
					<tbody>
						<tr>
							<td>
								<p>
									<strong>EN LO PRINCIPAL:</strong> Avenimiento y copias autorizadas; <strong>PRIMER</strong>
									<strong>OTROSI:</strong> Se tenga presente; <strong>SEGUNDO OTROSI:</strong> Autorización que indica</p>
								<p/>
								<p align="center">
									<strong>S.J.L. <xsl:value-of select="Tribunal"/>
									</strong>
								</p>
								<p/>
								<p style="text-indent: 5em;text-align: justify;"><strong><xsl:value-of select="concat(NombreEmpleado, '' '', PaternoEmpleado, '' '', MaternoEmpleado)"/></strong>, en mi calidad de apoderado de la ejecutante de autos, 
								<xsl:value-of select="Cliente"/>, ya individualizada en autos; 
								y don(ña) <xsl:value-of select="NombreFantasiaDeudor"/>, RUT. <xsl:value-of select="RutDeudor"/>, EMPLEADO, domiciliada en <xsl:value-of select="DireccionDeudor"/>, COMUNA DE <xsl:value-of select="ComunaDeudor"/>, 
								en autos sobre juicio ejecutivo de cobro de pagaré, caratulado "<strong><xsl:value-of select="Cliente"/> CON <xsl:value-of select="NombreFantasiaDeudor"/></strong>", 
								ROL <xsl:value-of select="Rol"/>, cuaderno principal, a US. respetuosamente decimos:</p>

								<p style="text-align: justify;">1.- Don(ña) <strong> <xsl:value-of select="NombreFantasiaDeudor"/></strong>&#160; reconoce adeudar a <xsl:value-of select="Cliente"/>. en su calidad de dueña del crédito, la suma de $<xsl:value-of select="MontoCuota"/> 
								( pesos), según liquidación efectuada con fecha <xsl:value-of select="FechaDemanda"/>, declarando el deudor no tener cargo 
								ni reclamo que formular en contra de la liquidación precedentemente señalada, dándose expresamente por requerido de pago por dicha suma.</p>

								<p style="text-align: justify;">2.- Por el presente avenimiento, se ha llegado a un acuerdo entre las partes, de forma que el deudor se obliga a pagar la suma de $<xsl:value-of select="MontoCuota"/> (<xsl:value-of select="MontoAvenimientoEscrito"/> pesos) en <xsl:value-of select="Cuotas"/> cuotas mensuales, 
								iguales y sucesivas de $<xsl:value-of select="MontoPrimeraCuota"/> (<xsl:value-of select="MontoCuotaAvenimientoEscrito"/> pesos), las que serán pagadas los días <xsl:value-of select="DiaPago"/> de cada mes, siendo el primer vencimiento el día <xsl:value-of select="FechaPrimeraCuota"/>.</p>

								<p style="text-align: justify;">3.- El pago de las cuotas será&#160; por los montos y en las fechas ya indicadas, en cualquier sucursal de la Cooperativa del Personal de la Universidad de Chile Limitada, sirviendo de suficiente constancia del pago los recibos que serán 
								emitidos por éste acreedor.</p>

								<p style="text-align: justify;">4.- INCUMPLIMIENTO. El simple retardo en el pago de cualquiera de las cuotas a que se refieren las cláusulas anteriores, dejará sin efecto las condonaciones efectuadas, facultándose al acreedor, a su arbitrio, para hacer exigible el total 
								de la deuda mencionada en el númeral 1, con sus correspondientes intereses penales, corridos desde la fecha de la liquidación que se menciona en el númeral 1, descontándose los abonos que hubiere efectuado.</p>

								<p style="text-align: justify;">Queda facultado el demandante desde ya, para continuar con la presente ejecución, en el estado en que se encuentra actualmente, hasta el entero pago de lo adeudado más intereses y costas, o iniciar un nuevo procedimiento judicial a fin de 
								obtener el pago de lo adeudado.</p>

								<p style="text-align: justify;">En el primer caso bastará que el ejecutante solicite el cumplimiento incidental del presente avenimiento ante este Tribunal, bastando para ello la sola petición del acreedor que da cuenta del incumplimiento, debiendo notificase por cédula al 
								demandado, y en el segundo caso, iniciar un nuevo proceso basado en el nuevo título ejecutivo, esto es, copias autorizadas del avenimiento judicial.</p>

								<p style="text-align: justify;">5.- EXCEPCIONES. Don(ña) <xsl:value-of select="NombreFantasiaDeudor"/> renuncia expresamente por este acto a las excepciones de cualquier tipo que pudiera oponer en este juicio, o las que eventualmente pudiera oponer en virtud de este avenimiento y su posterior 
								ejecución.</p>

								<p style="text-align: justify;">6.- COSTAS.&#160; Las costas procesales y personales son de cargo del ejecutado.</p>

								<p style="text-align: justify;">7.- GARANTIAS. Quedan vigentes los embargos, prendas y cualquier otro tipo de garantía real o personal que aseguraban el cumplimiento de la obligación que se transige en este acto, dejándose expresamente estipulado que sigen garantizando la 
								deuda pendiende señalada en el numeral 1 y siguientes del presente avenimiento.</p>

								<p style="text-align: justify;">8.- NOTIFICACION. Las partes se dan por notificadas de todas y cada una de las resoluciones dictadas en autos.</p>

								<p style="text-align: justify;"><strong>POR TANTO,</strong></p>

								<p style="text-align: justify;">
									<strong>ROGAMOS A US.</strong> Se sirva tener presente el avenimiento dispuesto en este líbelo, darle su aprobación, mandar se registre en el libro de sentencias del Tribunal y ordenar se den copias de este escrito, su resolución y de su 
									notificación por el estado diario, a la parte que lo solicite en Secretaría.</p>

								<p style="text-align: justify;">
									<strong>PRIMER OTROSI:</strong> En virtud&#160; de los establecido en el inciso segundo del art. 19 de la Ley N 19628, y en atención al pago realizado en virtud de este convenio Don(ña) <xsl:value-of select="NombreFantasiaDeudor"/>, en su calidad de 
									deudor,&#160;&#160; viene en liberar a <xsl:value-of select="Cliente"/> de la obligación de&#160; avisar al Banco de Datos respectivo donde se encuentra informada su morosidad o protesto del crédito materia de este convenio, obligándose a realizar la 
									modificación que corresponda en&#160; forma personal,&#160;&#160; para lo cual declara haber recibido a tu entera conformidad la constancia de pago que corresponde para este efecto.</p>

								<p style="text-align: justify;">
									<strong>SEGUNDO OTROSI: </strong>don(ña) <xsl:value-of select="NombreFantasiaDeudor"/> solicita a US. autorizarlo(a) para comparecer personalmente, sin patrocinio de abogado, por ser la única gestión que se realizará en este juicio.</p>

								<p/>
							</td>
						</tr>
					</tbody>
				</table>

				<div style="clear:both;"/>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>