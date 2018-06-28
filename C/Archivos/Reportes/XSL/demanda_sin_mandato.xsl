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
					font-size: 12px;
					font-family: times new roman;
				}
				.footer{
					position: fixed;
					bottom: 0;
					width: 100%;
					font-size: 8px;
					font-family: times new roman;
					text-align: center;
				}
				</style>
			</head>
			<body>
				<table  class="tabla_certificado">
					<tbody>
						<tr>
							<td  style="text-align: left;">
								<strong>TIPO DE PROCEDIMIENTO:</strong>
							</td>
							<td>
								<xsl:value-of select="MateriaJudicial"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>MATERIA DEL PLEITO:</strong>
							</td>
							<td>
								<xsl:value-of select="TipoCausa"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>DEMANDANTE:</strong>
							</td>
							<td>
								<xsl:value-of select="NombreCliente"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>RUT:</strong>
							</td>
							<td>
								<xsl:value-of select="RutCliente"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>ABOGADO PATROCINANTE Y APODERADO:</strong>
							</td>
							<td>
								<xsl:value-of select="concat(NombreEmpleado, '' '', PaternoEmpleado, '' '', MaternoEmpleado)"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>RUT:</strong>
							</td>
							<td>
								<xsl:value-of select="RutEmpleado"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>MAIL:</strong>
							</td>
							<td>
								<xsl:choose>
									<xsl:when test="Cliente = ''COOPEUCH LTDA''">
											coopeuch@dimol.cl
									</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="MailEmpleado"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>FONO:</strong>
							</td>
							<td>
								<xsl:choose>
									<xsl:when test="Cliente = ''COOPEUCH LTDA''">
											(2)29406068
									</xsl:when>
									<xsl:otherwise>
											(2)<xsl:value-of select="TelefonoEmpleado"/>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>DEMANDADO:</strong>
							</td>
							<td>
								<xsl:value-of select="concat(NombreDeudor, '' '', PaternoDeudor, '' '', MaternoDeudor)"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>RUT:</strong>
							</td>
							<td>
								<xsl:value-of select="RutDeudor"/>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<strong>DOMICILIO:</strong>
							</td>
							<td>
								<xsl:value-of select="DireccionDeudor"/>
							</td>
						</tr>
						<tr>
							<td colspan="2">
								<p style="text-indent: 5em;text-align: justify;">
									<strong>En lo principal:</strong> entabla demanda ejecutiva y solicita se despache mandamiento de ejecución y embargo. <strong>En el primer otrosí:</strong> señala bienes para la traba del embargo y designa depositario provisional. 
									<strong>En el segundo otrosí:</strong> acompaña documentos y solicita custodia. <strong>En el tercer otrosí:</strong> personería. <strong>En el cuarto otrosí:</strong> patrocinio y poder.
								</p>
								<p/>
								<p align="center">
									<strong>S.J.L. <xsl:value-of select="Tribunal"/>
									</strong>
								</p>
								<p/>
								<p style="text-indent: 5em;text-align: justify;"><strong>
										<xsl:value-of select="concat(NombreEmpleado, '' '', PaternoEmpleado, '' '', MaternoEmpleado)"/>
									</strong>, abogado, Rut <xsl:value-of select="RutEmpleado"/>, 
								mandatario(a) judicial de <strong>
										<xsl:value-of select="NombreCliente"/>, <xsl:value-of select="Cliente"/>,</strong> Rut: <xsl:value-of select="RutCliente"/>, 
								sociedad del giro de su denominación, representada legalmente por su Gerente General don(ña) <xsl:value-of select="RepresentanteLegal"/>, Rut <xsl:value-of select="RutRepresentanteLegal"/>, 
								factor de comercio, todos domiciliados para estos efectos en <xsl:value-of select="DireccionEmpresa"/>, <xsl:value-of select="ComunaEmpresa"/>, a US. Respetuosamente digo:
								</p>

								<p style="text-indent: 5em;text-align: justify;">Que vengo en deducir demanda en juicio ejecutivo por obligación de dar en contra de don(ña) <strong>
										<xsl:value-of select="concat(NombreDeudor, '' '', PaternoDeudor, '' '', MaternoDeudor)"/>
									</strong>
								, Rut <strong>
										<xsl:value-of select="RutDeudor"/>
									</strong>, ignoro profesión u oficio, 
								con domicilio en <strong>
										<xsl:value-of select="DireccionDeudor"/>, COMUNA DE <xsl:value-of select="ComunaDeudor"/>
									</strong>, por las razones que a continuación expongo:
								</p>

								<p style="text-indent: 5em;text-align: justify;">Don(ña) <strong>
										<xsl:value-of select="NombreFantasiaDeudor"/>
									</strong>, suscribió a la orden de, <strong>
										<xsl:value-of select="concat(NombreCliente, '' '', PaternoCliente, '' '', MaternoCliente)"/>
									</strong>
								, un pagaré en el que consta que con fecha <xsl:value-of select="FechaDemanda"/>, recibió un préstamo en dinero efectivo, por la suma de $<strong>
										<xsl:value-of select="Monto"/>.-(<xsl:value-of select="MontoDemandaEscrito"/> pesos)</strong>, que se obligó a pagar, conjuntamente con los intereses correspondientes
								, en <xsl:value-of select="Cuotas"/> cuotas mensuales, sucesivas e iguales de $<xsl:value-of select="MontoPrimeraCuota"/>.-(<xsl:value-of select="MontoPCuotaDemandaEscrito"/> pesos) cada una
								, con vencimiento los días <xsl:value-of select="DiaPago"/> de cada mes, a contar del mes de <xsl:value-of select="MesPrimeraCuota"/>, y una última de $<xsl:value-of select="MontoUltimaCuota"/>.-(<xsl:value-of select="MontoUCuotaDemandaEscrito"/> pesos), con vencimiento el día <xsl:value-of select="FechaUltimaCuota"/>.
								</p>

								<p style="text-indent: 5em;text-align: justify;">El capital adeudado devengaría una tasa de interés del <xsl:value-of select="Interes"/>% mensual calculado en base a 30 días y por el número de días efectivamente transcurridos, el que regirá a contar de la fecha de suscripción del pagaré y por todo el plazo de la obligación, incluyéndose el monto respectivo en los valores de cuota indicados, los cuales se pagarían el mismo día del vencimiento del capital.</p>
								
								<p style="text-indent: 5em;text-align: justify;">Se pactó además, que en el caso de mora o simple retardo en el pago de una cualquiera de las cuotas antes indicadas, <xsl:value-of select="Cliente"/>, estará facultada para exigir anticipadamente el saldo total adeudado como si fuera de plazo vencido, y en tal caso, todas las demás obligaciones de crédito de dinero que el deudor hubiere contraído con <xsl:value-of select="Cliente"/>, se considerarán de plazo vencido por el total adeudado y devengándose a contar de la fecha de la mora o el simple retardo, como interés moratorio, el interés máximo convencional permitido para operaciones no reajustables que rija durante el periodo de mora. El interés moratorio se aplicará sobre el total del saldo adeudado desde la mora o simple retardo  y hasta la fecha de su pago efectivo, capitalizándose los intereses conforme a la ley.</p>
								<div contenteditable="false" aria-label="Salto de página"  data-cke-display-name="pagebreak" data-cke-pagebreak="1" style="page-break-after: always" title="Salto de página"></div>
								<p style="text-indent: 5em;text-align: justify;"><xsl:value-of select="Cliente"/>,  quedó facultada para hacer exigible el pago total de la deuda o del saldo a que ésta se halle reducida, considerando la presente obligación como de plazo vencido, en caso de mora o simple retardo del deudor, en el pago de una cualquiera de las cuotas en que se divide este pagaré, sea de capital o intereses, sean consecutivas o no, sin perjuicio de los demás derechos del acreedor. Lo mismo se pactó para el caso que el obligado a su pago cayera en insolvencia, devengándose desde entonces y hasta la fecha del pago efectivo, un interés moratorio correspondiente al máximo que la ley permite estipular para este tipo de operaciones.</p>

								<p style="text-indent: 5em;text-align: justify;">Para los efectos del orden interno que el mandante necesita en el giro de sus operaciones comerciales, el documento más arriba singularizado tiene el número de préstamo <xsl:value-of select="Numero"/>.</p>

								<p style="text-indent: 5em;text-align: justify;">EL deudor se encuentra en mora en el pago de esta obligación a partir del día  <xsl:value-of select="FechaVencimiento"/> adeudando la suma de <strong>$<xsl:value-of select="Saldo"/>.- (<xsl:value-of select="MontoSaldoDemandaEscrito"/> pesos)</strong>,  (por concepto de capital) más los intereses convencionales y  penales que correspondan  a contar de la fecha de la mora y hasta la fecha del pago efectivo.</p>

								<p style="text-indent: 5em;text-align: justify;">Así las cosas, de acuerdo con lo expuesto precedentemente, el deudor se ha puesto en la posibilidad de que el acreedor haga exigible el total de lo adeudado, respecto del pagaré más arriba singularizado, en conformidad a la cláusula de aceleración pactada, <u>derecho que esta parte hace efectivo a contar de la notificación de esta demanda</u>.</p>
								<p style="text-indent: 5em;text-align: justify;">Se relevó al acreedor de la obligación de protesto.</p>
								<p style="text-indent: 5em;text-align: justify;">La firma del suscriptor se encuentra autorizada ante Notario Público, por lo que de acuerdo a lo establecido en el artículo 434 N° 4 del Código de Procedimiento Civil, el pagaré tiene mérito ejecutivo. Y siendo la deuda líquida, actualmente exigible y no prescrita, mi representada viene en hacer efectivo su derecho y en demandar el pago íntegro del crédito a través de este procedimiento ejecutivo y <strong>
										<u>a contar de su notificación</u>.</strong>
								</p>

								<p style="text-indent: 5em;text-align: justify;"><strong>POR TANTO,</strong>
								</p>
								<p style="text-align: justify;">Y en mérito de lo expuesto, documentos acompañados y lo dispuesto en el artículo 434 N° 4 del Código de Procedimiento Civil,</p>
								<p style="text-align: justify;">
									<strong>A US. PIDO:</strong> tener por interpuesta la presente demanda ejecutiva en contra de don(ña) <strong>
										<xsl:value-of select="concat(NombreDeudor, '' '', PaternoDeudor, '' '', MaternoDeudor)"/>
									</strong>
								, ya individualizado(a), ordenando se despache en su contra mandamiento de ejecución y embargo por un total de <strong>$<xsl:value-of select="Saldo"/>.- (<xsl:value-of select="MontoSaldoDemandaEscrito"/> pesos), (por concepto de capital)</strong> más los intereses convencionales y  penales convenidos que deben calcularse a partir de la fecha de la mora y hasta la fecha de pago efectivo, y las costas de la causa, declarando en definitiva que debe seguirse adelante con la ejecución hasta hacer a mi representado entero y cumplido pago del total de lo adeudado, con costas.</p>

								<div contenteditable="false" aria-label="Salto de página"  data-cke-display-name="pagebreak" data-cke-pagebreak="1" style="page-break-after: always" title="Salto de página"></div>
								
								<p style="text-align: justify;">
									<strong>PRIMER OTROSÍ:</strong> Sírvase S. S. tener presente que señalo como bienes para la traba del embargo todos los bienes muebles e inmuebles, presentes o futuros, del  deudor a quien designo depositario provisional, bajo su responsabilidad civil y criminal.</p>

								<p style="text-align: justify;">
									<strong>SEGUNDO OTROSÍ:</strong> Sírvase S. S. tener por acompañado con citación Pagaré singularizado en lo principal. A fin de evitar extravíos ruego a S.S. ordenar la custodia de este documento. </p>

								<p style="text-align: justify;">
									<strong>TERCER OTROSÍ:</strong> Sírvase S. S. tener presente que mi personería para representar a <strong>
										<xsl:value-of select="NombreCliente"/>
									</strong> consta en la escritura pública de fecha 12 de enero de 2016, otorgada en la Notaria de Santiago, ante doña Maria Soledad Santos Muñoz, Notario titular, cuya copia autorizada se encuentra registrada en la Secretaría del tribunal, la que solicito se tenga a la vista en esta causa, por acompañada con citación, por exhibida ante el Secretario del Tribunal y por acreditada dicha personería en los términos del artículo 6º y 7º del Código de Procedimiento Civil.
									</p>
								<p style="text-align: justify;">
									<strong>CUARTO OTROSÍ:</strong> Sírvase S. S. tener presente que en mi calidad de abogado asumo personalmente el patrocinio y el poder en esta causa, y actuare conforme al poder que me ha sido otorgado.</p>

								<p/>
							</td>
						</tr>
					</tbody>
				</table>
				<div style="clear:both;"/>
				<xsl:choose>
					<xsl:when test="Cliente = ''COOPEUCH LTDA''">
						<div class="footer">
							<p>Para mayor información, contáctenos al teléfono (2)29406068, al email coopeuch@dimol.cl, o personalmente en Uno sur 690 of. 618, Talca</p>
						</div>
					</xsl:when>
					<xsl:otherwise/>
				</xsl:choose>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
