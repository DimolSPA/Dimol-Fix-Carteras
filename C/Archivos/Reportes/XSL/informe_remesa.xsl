<?xml version="1.0" encoding="UTF-8"?>

<!-- This stylesheet queries the contents of some of the other examples and
     generates a fairly simple multimedia catalog. It is meant to show how
     some simple formatting can be accomplished with formatting objects.

     See minimal-catalog.xsl for an example of an unformatted result. -->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<!-- This variable is set by the installation procedure to the absolute
     path of the Stylus Studio examples directory on your system. 
	<xsl:variable name="file" select="document('C:\Program Files (x86)\Stylus Studio X15 Release 2 XML Enterprise Suite\examples\resumen_gestiones.xml')"/>
-->
	<xsl:template match="/">
		<fo:root>
			<fo:layout-master-set>
				<!-- Define the cover page. -->
				<fo:simple-page-master master-name="cover" page-height="5.5in"
                             page-width="8.5in" margin-top="0.25in"
                             margin-bottom="0.25in" margin-left="0.25in"
                             margin-right="0.25in">
					<fo:region-body margin-top="0.25in" margin-bottom="0.25in"/>
				</fo:simple-page-master>

				<!-- Define a body (or default) page. -->
				<fo:simple-page-master master-name="default-master" page-height="5.5in"
                             page-width="8.5in" margin-top="0.25in"
                             margin-bottom="0.25in" margin-left="0.25in"
                             margin-right="0.25in">

					<!-- Central part of page -->
					<fo:region-body column-count="1" margin-top="0.5in"
                        margin-bottom="0.25in"/>

					<!-- Header -->
					<fo:region-before extent="0.5in" />

					<!-- Footer -->
					<fo:region-after extent="0.05in"/>
				</fo:simple-page-master>
			</fo:layout-master-set>

			<!-- The data to be diplayed in the pages, cover page first -->
			<!--    <fo:page-sequence master-reference="cover">
      <fo:flow flow-name="xsl-region-body">
        <fo:block text-align="center" font-size="14.0pt">
          <xsl:text>Portada Prueba Reportes</xsl:text>
        </fo:block>
      </fo:flow>
    </fo:page-sequence>-->

			<!-- Body page -->
			<fo:page-sequence master-reference="default-master">

				<!-- Define the contents of the header. -->
				<fo:static-content flow-name="xsl-region-before">
					<fo:block>
						<fo:table>
							<fo:table-column column-width="33%"/>
							<fo:table-column column-width="34%"/>
							<fo:table-column column-width="33%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/InformeRemesa/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/InformeRemesa/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/InformeRemesa/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="27px">
											<fo:external-graphic src="url('file:///D:/Dimol/logo-reporte.svg')" width="200px" height="80px"  />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/InformeRemesa/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/InformeRemesa/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/InformeRemesa/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/InformeRemesa/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
					</fo:block>
				</fo:static-content>

				<!-- Define the contents of the footer. -->
				<fo:static-content flow-name="xsl-region-after">
					<fo:table>
						<fo:table-column column-width="50%"/>
						<fo:table-column column-width="50%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell>
									<fo:block font-size="6.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="left">
			                  Pág.  <fo:page-number/> de <fo:page-number-citation ref-id="UltimaPagina"/> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block font-size="6.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="/InformeRemesa/FechaReporte"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:static-content>

				<!-- The main contents of the body page, that is, the catalog
           entries -->
				<fo:flow flow-name="xsl-region-body">
					<xsl:call-template name="title">
						<xsl:with-param name="titulo" select="'INFORME DE REMESA'"/>
						<xsl:with-param name="cliente" select="/InformeRemesa/NombreCliente"/>
						<xsl:with-param name="numeroRemesa" select="/InformeRemesa/NumeroRemesa"/>
						<xsl:with-param name="fechaEmision" select="/InformeRemesa/FechaEmision"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/InformeRemesa/lstDocumentos"/>
					</xsl:call-template>

					<fo:block id="UltimaPagina"/>
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
		<xsl:param name="cliente"/>
		<xsl:param name="numeroRemesa"/>
		<xsl:param name="fechaEmision"/>

		<fo:block font-size="8.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="center">
			<fo:block  font-size="10.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block font-size="9.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
				<xsl:value-of select="$cliente"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="center">
				<xsl:text>NUMERO REMESA   </xsl:text><xsl:value-of select="$numeroRemesa"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="center">
				<xsl:text>FECHA EMISION   </xsl:text><xsl:value-of select="$fechaEmision"/>
			</fo:block>
		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>
		
		<xsl:for-each select="$filas/InformeRemesaDeudor">
		<fo:block  page-break-inside="avoid">
		<fo:block font-size="8.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="center">
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="center" font-weight="bold" color="blue">
				<xsl:text>RUT : </xsl:text><xsl:value-of select="RutSubCartera"/><xsl:text>     NOMBRE : </xsl:text><xsl:value-of select="SubCartera"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left" text-decoration="italic"  font-weight="bold" color="red">
				<xsl:text>DEUDOR : </xsl:text><xsl:value-of select="RutDeudorFormateado"/><xsl:text>           </xsl:text><xsl:value-of select="NombreFantasia"/>
			</fo:block>
		</fo:block>
		<fo:block font-size="6.0pt" font-family="serif">
			<fo:table>
				<fo:table-column column-width="8%"/>
				<fo:table-column />
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-header>
					<fo:table-row>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold" >FEC. PAGO</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">TIPO DOCUMENTO</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">NUMERO</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold" >CAPITAL</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">INTERES</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">HONORARIO</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold" >GASTOS</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">RECUPERADO</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">% O UF</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold" >COMISION</fo:block>
						</fo:table-cell>
						<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
							<fo:block font-weight="bold">NEGOCIO</fo:block>
						</fo:table-cell>
					</fo:table-row>
				</fo:table-header>
				<fo:table-body>
					<xsl:choose>
						<xsl:when test="$filas/InformeRemesaDeudor != ''">
							<xsl:for-each select="lstDetalles/InformeRemesaDetalle">
								<fo:table-row>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
										<fo:block>
											<xsl:value-of select="FechaPago"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
										<fo:block>
											<xsl:value-of select="TipoDocumento"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
										<fo:block>
											<xsl:value-of select="Numero"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
										<fo:block>
											<xsl:value-of select="Capital"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
										<fo:block>
											<xsl:value-of select="Interes"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
										<fo:block>
											<xsl:value-of select="Honorario"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
										<fo:block>
											<xsl:value-of select="Gastos"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
										<fo:block>
											<xsl:value-of select="Recuperado"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
										<fo:block>
											<xsl:value-of select="UF"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
										<fo:block>
											<xsl:value-of select="Comision"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
										<fo:block>
											<xsl:value-of select="Negocio"/>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</xsl:for-each>
						</xsl:when>
						<xsl:otherwise>
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</xsl:otherwise>
					</xsl:choose> 
					<fo:table-row>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell text-align="left" border-width="1px" padding="2px">
							<fo:block  font-weight="bold" color="red">SUBTOTAL:</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="red">
								<xsl:value-of select="Totales/Capital"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="red">
								<xsl:value-of select="Totales/Interes"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="red">
								<xsl:value-of select="Totales/Honorario"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell  text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="red">
								<xsl:value-of select="Totales/Gastos"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="red">
								<xsl:value-of select="Totales/Recuperado"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right">
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="red">
								<xsl:value-of select="Totales/Comision"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell text-align="left" border-width="1px" padding="2px">
							<fo:block  font-weight="bold" color="blue">SUBTOTAL:</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="blue">
								<xsl:value-of select="Totales/Capital"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="blue">
								<xsl:value-of select="Totales/Interes"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="blue">
								<xsl:value-of select="Totales/Honorario"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell  text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="blue">
								<xsl:value-of select="Totales/Gastos"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="blue">
								<xsl:value-of select="Totales/Recuperado"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right">
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold" color="blue">
								<xsl:value-of select="Totales/Comision"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
					</fo:table-row>
					

				</fo:table-body>

			</fo:table>
			
		</fo:block>
		</fo:block>
		</xsl:for-each>
		
		<fo:table font-size="6.0pt" font-family="serif">
				<fo:table-column column-width="8%"/>
				<fo:table-column />
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-column column-width="8%"/>
				<fo:table-body>
				<fo:table-row>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell text-align="left" border-width="1px" padding="2px">
							<fo:block  font-weight="bold">TOTAL:</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold">
								<xsl:value-of select="/InformeRemesa/Totales/Capital"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold">
								<xsl:value-of select="/InformeRemesa/Totales/Interes"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold">
								<xsl:value-of select="/InformeRemesa/Totales/Honorario"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell  text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold">
								<xsl:value-of select="/InformeRemesa/Totales/Gastos"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold">
								<xsl:value-of select="/InformeRemesa/Totales/Recuperado"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell text-align="right">
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell text-align="right" border-width="1px" padding="2px">
							<fo:block font-weight="bold">
								<xsl:value-of select="/InformeRemesa/Totales/Comision"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block/>
						</fo:table-cell>
					</fo:table-row>
				</fo:table-body>
		</fo:table>
		<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  text-align="left" space-after="3.0pt" space-before="4.0pt" text-decoration="underline" font-weight="bold">
				<xsl:text>COMENTARIO : </xsl:text>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  text-align="left" space-after="3.0pt" space-before="4.0pt">
				<xsl:value-of select="/InformeRemesaDeudor/lstDetalles/InformeRemesaDetalle/Comentario"/>
			</fo:block>
	</xsl:template>

</xsl:stylesheet>


