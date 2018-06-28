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
				<fo:simple-page-master master-name="cover" page-height="8.5in"
                             page-width="5.5in" margin-top="0.25in"
                             margin-bottom="0.25in" margin-left="0.25in"
                             margin-right="0.25in">
					<fo:region-body margin-top="0.25in" margin-bottom="0.25in"/>
				</fo:simple-page-master>

				<!-- Define a body (or default) page. -->
				<fo:simple-page-master master-name="default-master" page-height="8.5in"
                             page-width="5.5in" margin-top="0.25in"
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
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												AGRORAMA
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												76.064.419-6
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												COM. DE PROD. AGRICOLAS
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="7px">
											<fo:external-graphic src="url('file:///D:/logo-agrorama2.png')" width="200px" height="80px"  />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												LAS CONDES
												<xsl:text>, </xsl:text>
												REG. METROPOLITANA
												<xsl:text>, </xsl:text>
												CHILE
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												56 2 2425 3867
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
									<fo:block font-size="5.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="left">
			                  Pág.  <fo:page-number/> de <fo:page-number-citation ref-id="UltimaPagina"/> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block font-size="6.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="/TortaAgrupada/FechaReporte"/>
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
						<xsl:with-param name="titulo" select="'RESUMEN DE CARTERA'"/>
						<xsl:with-param name="cliente" select="/TortaAgrupada/FechaEmision"/>
						<xsl:with-param name="rutCliente" select="'COMERCIAL AGRORAMA LTDA.'"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/TortaAgrupada/lstDocumentos"/>
					</xsl:call-template>

					<fo:block id="UltimaPagina"/>
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
		<xsl:param name="cliente"/>
		<xsl:param name="rutCliente"/>

		<fo:block font-size="7.0pt" font-family="Calibri" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
			<fo:block  font-size="7.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block font-size="5.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
							  <xsl:text>AL DIA   </xsl:text>
				<xsl:value-of select="$cliente"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$rutCliente"/>
			</fo:block>
		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>

		<xsl:for-each select="$filas/TortaCliente">
			<fo:block  page-break-inside="auto">
				<fo:block font-size="5.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="center">
					<fo:block font-size="5.0pt" font-family="calibri" padding-after="2.0pt"
			                   text-align="left" font-weight="bold" white-space="pre">
						<xsl:text>GRUPO DE ESTADOS : </xsl:text>
						<xsl:value-of select="Agrupa"/>
					</fo:block>
				</fo:block>
				<xsl:if test="1 > 0">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic" >
						<xsl:text/>
				</fo:block>
				<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="25%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-rows-spanned="2">
									<fo:block font-weight="bold">ESTADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center"  number-columns-spanned="2">
									<fo:block font-weight="bold">CANTIDAD</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-columns-spanned="4">
									<fo:block font-weight="bold" >TOTALES</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">DEUDORES</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >DOCUMENTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center"  color="blue">
									<fo:block font-weight="bold" >REGULARIZADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">COMPROMISO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >SALDO</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$filas/TortaCliente != ''">
									<xsl:for-each select="lstEstados/TortaEstado">
										<fo:table-row>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Estado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Deudores"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Documentos"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Monto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right"  color="blue">
												<fo:block>
													<xsl:value-of select="Regularizado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="Compromiso"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
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
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							<fo:table-row font-size="5.0pt">
								<fo:table-cell text-align="center" border-width="1px" padding="2px" number-columns-spanned="3">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell  text-align="right" border-width="1px" padding="2px">
									<fo:block font-weight="bold" font-style="italic">
										<xsl:value-of select="SubTotal/Monto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="3px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="SubTotal/Regularizado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="0px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="SubTotal/Compromiso"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="SubTotal/Saldo"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>

					</fo:table>
				
				</fo:block>
				</xsl:if>
				
				
				
				
			</fo:block>
		</xsl:for-each>
		<xsl:if test="/TortaAgrupada/Totales != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic" >
						<xsl:text/>
						
				</fo:block>
				<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="25%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-body>

				

										<fo:table-row  background-color="grey">
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block font-weight="bold" color="darkblue" background-color="grey">
													TOTAL RESUMEN
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Deudores"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Documentos"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Monto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Regularizado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Compromiso"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Saldo"/>
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
				
						
								

						</fo:table-body>
					</fo:table>
				
				</fo:block>
				</xsl:if>
				<xsl:if test="/TortaAgrupada/Porcentajes != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" padding-top="0.2in" font-weight="bold" font-style="italic" >
						<xsl:text/>
				</fo:block>
				<fo:block font-size="6.0pt" font-family="calibri">
					<fo:table  keep-with-previous.within-page="always"  page-break-inside="avoid" >
						<fo:table-column column-width="20mm"/>
						<fo:table-column column-width="30%"/>
						<fo:table-column column-width="20%"/>
						<fo:table-column column-width="20%"/>

						<fo:table-header>
							<fo:table-row>
							<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-columns-spanned="3">
									<fo:block font-weight="bold">RESUMEN FINAL</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">TITULO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >%</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="/TortaAgrupada/Porcentajes != ''">
									<xsl:for-each select="/TortaAgrupada/Porcentajes/TortaPorcentaje">
										<fo:table-row>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Titulo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Monto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Porcentaje"/>
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
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							
						</fo:table-body>

					</fo:table>
				
				</fo:block>
				</xsl:if>
	</xsl:template>

</xsl:stylesheet>


