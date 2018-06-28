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
							<td  style="text-align: left;">
								<span style="font-size:12px">
									<span style="font-family:times new roman,times,serif">
										<strong>(&#160;&#160;)&#160;<xsl:value-of select="Tribunal"/></strong>
									</span>
								</span>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<span style="font-size:12px">
									<span style="font-family:times new roman,times,serif">
										<strong>CUADERNO PRINCIPAL</strong>
									</span>
								</span>
							</td>
						</tr>
						<tr>
							<td  style="text-align: left;">
								<span style="font-size:12px">
									<span style="font-family:times new roman,times,serif">
										<strong>ROL: C-&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;-<xsl:value-of select="Anio"/>
										</strong>
									</span>
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<p/>

								<p/>

								<p style="text-align: justify;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif">
											<strong>EN LO PRINCIPAL: </strong>EMBARGO CON FUERZA PÚBLICA; <strong>EN EL OTROSÍ:  </strong>SE OFICIE.
										</span>
									</span>
								</p>

								<p/>

								<p/>

								<p style="text-align: center;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif">
											<strong>
												<xsl:value-of select="Tribunal"/>
											</strong>
										</span>
									</span>
								</p>

								<p/>

								<p/>

								<p style="text-indent: 5em;text-align: justify;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif">
											<strong><xsl:value-of select="concat(NombreEmpleado, '' '', PaternoEmpleado, '' '', MaternoEmpleado)"/>, </strong>abogado<strong>,</strong> por la parte demandante sobre juicio ejecutivo, en autos caratulados <strong>"____________________________________________________", ROL C-_______________, </strong>a US. Respetuosamente digo:</span>
									</span>
								</p>

								<p/>

								<p style="text-indent: 5em;text-align: justify;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif">Que atendido el estado de la causa y a que a la fecha no se ha logrado trabar embargo sobre bienes de la ejecutada, vengo en solicitar se conceda el auxilio de la fuerza pública, a objeto de efectuar el embargo de bienes, debido a la existencia de oposición, como consta en el atestado receptorial, por lo que procede dar cumplimiento a dicha medida en forma forzada.</span>
									</span>
								</p>

								<p/>
								
								<p style="text-indent: 5em;text-align: justify;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif"><strong>POR TANTO,</strong></span>
									</span>
								</p>

								<p style="text-align: justify;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif"><strong>RUEGO A U.S.: </strong>Se sirva ordenar se cumpla dicho trámite, con el auxilio de la fuerza pública, con facultad de allanar y descerrajar si fuese necesario.</span>
									</span>
								</p>
								
								<p style="text-align: justify;">
									<span style="font-size:12px">
										<span style="font-family:times new roman,times,serif"><strong>EN EL OTROSÍ: </strong>A U.S. ruego se sirva ordenar se oficie a Carabineros de Chile, a fin de que preste la fuerza pública, para cumplir esta actuación judicial, con facultad de allanar y descerrajar, solo si fuese necesario.</span>
									</span>
								</p>

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