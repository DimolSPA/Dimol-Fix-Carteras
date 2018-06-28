<?xml version="1.0" encoding="UTF-8"?>

<!-- This stylesheet queries the contents of some of the other examples and
     generates a fairly simple multimedia catalog. It is meant to show how
     some simple formatting can be accomplished with formatting objects.

     See minimal-catalog.xsl for an example of an unformatted result. -->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
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
					<!-- Central part of page -->
					<fo:region-body column-count="1" margin-top="0.5in"
                        margin-bottom="0.25in"/>
					<!-- Header -->
					<fo:region-before extent="0.5in" region-name="xsl-region-before-cover"  />
					<!-- Footer -->
					<fo:region-after extent="0.05in"/>
				</fo:simple-page-master>

				<!-- Define a body (or default) page. -->
				<fo:simple-page-master master-name="default-master" page-height="8.5in"
                             page-width="5.5in" margin-top="0in"
                             margin-bottom="0.25in" margin-left="0.25in"
                             margin-right="0.25in">
					<!-- Central part of page -->
					<fo:region-body column-count="1" margin-top="0.5in"
                        margin-bottom="0.25in"/>
					<!-- Header -->
					<fo:region-before extent="0in" />
					<!-- Footer -->
					<fo:region-after extent="0.05in"/>
				</fo:simple-page-master>
				<fo:page-sequence-master master-name="allPages">
					<fo:repeatable-page-master-alternatives>
						<fo:conditional-page-master-reference page-position="only" master-reference="cover"/>
						<fo:conditional-page-master-reference page-position="first" master-reference="cover"/>
						<fo:conditional-page-master-reference page-position="rest" master-reference="default-master"/>
						<fo:conditional-page-master-reference page-position="last" master-reference="default-master"/>
					</fo:repeatable-page-master-alternatives>
				</fo:page-sequence-master>
		</fo:layout-master-set>

			<!-- The data to be diplayed in the pages, cover page first -->
			<fo:page-sequence master-reference="allPages">
				<!-- Define the contents of the header. -->
				
				<fo:static-content flow-name="xsl-region-before-cover">
					<fo:block>
						<fo:table>
							<fo:table-column column-width="30%"/>
							<fo:table-column column-width="40%"/>
							<fo:table-column column-width="30%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/InformeJudicial/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/InformeJudicial/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/InformeJudicial/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="1px">

										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="1px"  text-align="center">
											<fo:external-graphic src="url('file:///D:/Archivos/Reportes/XSL/logo-reporte.svg')"  content-height="scale-to-fit" width="90px"  scaling="uniform"  />
										</fo:block>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt"  text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/InformeJudicial/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/InformeJudicial/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/InformeJudicial/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/InformeJudicial/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
					</fo:block>
					<fo:retrieve-marker retrieve-class-name="subHeader" retrieve-position="first-starting-within-page"/>
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
									<fo:block font-size="5.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="/InformeJudicial/FechaReporte"/>
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
						<xsl:with-param name="titulo" select="'INFORME JUDICIAL'"/>
						<xsl:with-param name="cliente" select="/InformeJudicial/Titulo/Cliente"/>
						<xsl:with-param name="rutCliente" select="/InformeJudicial/FechaLarga"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="codigoCarga">
						<xsl:with-param name="codigo" select="/InformeJudicial/lstCodigoCarga"/>
					</xsl:call-template>
					<fo:block font-size="6.0pt" font-family="calibri" padding-top="3px">
					<fo:table>
						<fo:table-column column-width="50%"/>
						<fo:table-column column-width="50%"/>

						<fo:table-body>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" >
									<fo:block>
										TOTAL DOCUMENTOS DEMANDADOS <xsl:value-of select="/InformeJudicial/Totales/Cantidad"/>
									</fo:block>
								</fo:table-cell>
							
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										TOTAL SALDO CAPITAL <xsl:value-of select="/InformeJudicial/Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" font-style="italic" >
									<fo:block>
										
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right"  >
									<fo:block>
										TOTAL SALDO GASTOS <xsl:value-of select="/InformeJudicial/Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" font-style="italic" >
									<fo:block>
										
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										TOTAL FINAL <xsl:value-of select="/InformeJudicial/Totales/Total"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
					<fo:block id="UltimaPagina"/>
				</fo:flow>
			</fo:page-sequence>

			<!-- Body page -->
		
			
		</fo:root>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
		<xsl:param name="codigo"/>
		<xsl:param name="cliente"/>
		<xsl:param name="rutCliente"/>

		<fo:block font-size="8.0pt" font-family="Calibri" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
			<fo:block  font-size="10.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block font-size="11.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$cliente"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:text>Al día </xsl:text>
				<xsl:value-of select="$rutCliente"/>
			</fo:block>
		</fo:block>
	</xsl:template>
	
	<xsl:template name="codigoCarga">
		<xsl:param name="codigo"/>
		<xsl:for-each select="$codigo/InformeJudicialCodigoCarga">
			<xsl:call-template name="listaAsegurados">
				<xsl:with-param name="asegurado" select="lstAsegurados"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	
	<xsl:template name="listaAsegurados">
		<xsl:param name="asegurado"/>
		<xsl:for-each select="$asegurado/InformeJudicialAsegurado">
			<xsl:call-template name="listaDeudores">
				<xsl:with-param name="deudor" select="lstDeudores"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="listaDeudores">
		<xsl:param name="deudor"/>
		<xsl:for-each select="$deudor/InformeJudicialDeudor">
			<fo:table keep-together="always">
				<fo:table-column column-width="100%"/>
					<fo:table-body>
						<fo:table-row font-size="6.0pt" keep-together="always">
							<fo:table-cell >
								<fo:block font-size="7.0pt" font-family="calibri" padding-left="8px" font-weight="bold" text-align="left" color="firebrick" font-style="italic" keep-together.within-column="always">
									<xsl:text>RUT: </xsl:text><xsl:value-of select="RutDeudorFormateado"/><xsl:text>  NOMBRE: </xsl:text><xsl:value-of select="NombreFantasia"/>
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row font-size="6.0pt">
							<fo:table-cell >
								<xsl:call-template name="listaCausas">
									<xsl:with-param name="causa" select="lstCausas"/>
								</xsl:call-template>	
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row font-size="6.0pt" >
							<fo:table-cell  >
								<fo:block font-size="6.0pt" font-family="calibri" padding-bottom="1px">
									<xsl:if test="Totales/NroHijos > 1">
											<fo:table>
												<fo:table-column column-width="60%"/>
												<fo:table-column column-width="40%"/>
												<fo:table-body>
													<fo:table-row font-size="6.0pt" >
														<fo:table-cell  padding="2px" text-align="left" font-weight="bold"  color="firebrick"  border-before-style="solid" border-before-width="1pt">
															<fo:block>
																<xsl:value-of select="Totales/Cantidad"/> Documentos para el RUT <xsl:value-of select="RutDeudorFormateado"/>   
															</fo:block>
														</fo:table-cell>
														<fo:table-cell  padding="2px" text-align="right" font-weight="bold" color="firebrick"  border-before-style="solid" border-before-width="1pt">
															<fo:block>
																Saldo Capital <xsl:value-of select="Totales/Capital"/>
															</fo:block>
														</fo:table-cell>
													</fo:table-row>
													<fo:table-row font-size="6.0pt">
														<fo:table-cell  padding="2px" text-align="left" font-weight="bold"  color="firebrick"  >
															<fo:block>
															</fo:block>
														</fo:table-cell>
														<fo:table-cell  padding="2px" text-align="right" font-weight="bold" color="firebrick"  >
															<fo:block>
																Gastos <xsl:value-of select="Totales/Gasto"/>
															</fo:block>
														</fo:table-cell>
													</fo:table-row>
													<fo:table-row font-size="6.0pt">
														<fo:table-cell  padding="2px" text-align="left" font-weight="bold"  color="firebrick"  >
															<fo:block> 
															</fo:block>
														</fo:table-cell>
														<fo:table-cell  padding="2px" text-align="right" font-weight="bold" color="firebrick"  >
															<fo:block>
																Total RUT <xsl:value-of select="Totales/Total"/>
															</fo:block>
														</fo:table-cell>
													</fo:table-row>
												</fo:table-body>
											</fo:table>
											</xsl:if>
										</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
			</fo:table>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="listaCausas">
		<xsl:param name="causa"/>
		<xsl:for-each select="$causa/InformeJudicialCausa">
			<fo:table keep-together="always">
				<fo:table-column column-width="100%"/>
					<fo:table-body>
						<fo:table-row font-size="6.0pt">
							<fo:table-cell >
										<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt"
											  space-before="2.0pt" text-align="center"  widows="4" orphans="4" 
											  border-style="solid" border-width="1" border-color="#000" keep-together.within-column="always">
												<fo:table font-size="6.0pt" margin="1mm" widows="4" orphans="4" >
													<fo:table-column column-width="10%"/>
													<fo:table-column column-width="53%"/>
													<fo:table-column column-width="18%"/>
													<fo:table-column column-width="20%"/>
													<fo:table-body>
														<fo:table-row>
															<fo:table-cell padding="1px" text-align="left" font-weight="bold" color="dodgerblue" >
																<fo:block>
																	<xsl:text>CAUSA: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell padding="1px" text-align="left" font-weight="bold" color="dodgerblue">
																<fo:block>
																	<xsl:value-of select="Causa"/>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell padding="1px" text-align="left">
																<fo:block>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell padding="1px" text-align="left">
																<fo:block>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row>
															<fo:table-cell  padding="1px" text-align="left" font-weight="bold" >
																<fo:block>
																	<xsl:text>JUZGADO: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="Juzgado"/>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left" font-weight="bold" >
																<fo:block>
																	<xsl:text>NUMERO DE ROL: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="NumeroRol"/>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row>
															<fo:table-cell  padding="1px" text-align="left" font-weight="bold" >
																<fo:block>
																	<xsl:text>MATERIA: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="Materia"/>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left" font-weight="bold" >
																<fo:block>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row>
															<fo:table-cell padding="1px" text-align="left" font-weight="bold" >
																<fo:block>
																	<xsl:text>ESTADO: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="Estado"/>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:text>ULTIMA GESTION: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="UltimaGestion"/>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row>
															<fo:table-cell padding="1px" text-align="left">
																<fo:block>
																	<xsl:text>REGION: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="Region"/>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:text>CIUDAD: </xsl:text>
																</fo:block>
															</fo:table-cell>
															<fo:table-cell  padding="1px" text-align="left">
																<fo:block>
																	<xsl:value-of select="Ciudad"/>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row font-weight="bold" >
															<fo:table-cell padding="1px" text-align="left" >
																<fo:block>
																	<xsl:text >RESUMEN: </xsl:text>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row >
															<fo:table-cell padding="1px" text-align="left" number-columns-spanned="4">
																<fo:block>
																	<xsl:value-of select="Resumen"/>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
													</fo:table-body>
												</fo:table>
											</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row font-size="6.0pt" >
							<fo:table-cell >
								<xsl:call-template name="listaDocumentos">
									<xsl:with-param name="documento" select="lstDetallesPesos"/>
								</xsl:call-template>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
			</fo:table>
			<xsl:if test="position() mod 4 = 0">
				<fo:block page-break-after="always"></fo:block>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	
	<xsl:template name="listaDocumentos">
		<xsl:param name="documento"/>
		<xsl:variable name="rowcount" select="0" />
		<xsl:if test=". != ''">
				<fo:block font-size="6.0pt" font-family="calibri" padding-after="2.0pt" widows="4" orphans="4">
				<fo:block font-size="6.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="27%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="11%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">DOCUMENTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">NUMERO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >F. VEN.</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONEDA</fo:block>
								</fo:table-cell>						
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >TIP. CAMB.</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">CAPITAL</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >TOTAL</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$documento/InformeJudicialDetalle != ''">
									<xsl:for-each select="$documento/InformeJudicialDetalle">
									<xsl:variable name="i" select="position()" />
									
										<fo:table-row font-size="5.0pt">
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="TipoDocumento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Numero"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaVencimiento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="Moneda"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="TipoCambio"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Capital"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Gasto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right" >
												<fo:block>
													<xsl:value-of select="Total"/>
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
										<xsl:if test="position() mod 40 = 0">
											<fo:table-row>
												<fo:table-cell>
													<fo:block page-break-after="always"></fo:block>
												</fo:table-cell>
											</fo:table-row>
										</xsl:if>
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
							<fo:table-row font-size="6.0pt" >
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="4" border-before-style="solid" border-before-width="1pt">
									<fo:block  font-weight="bold"><xsl:value-of select="Totales/Cantidad"/> Documentos por Causa</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px"  border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
			
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic">
				
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt" number-columns-spanned="2">
									<fo:block font-weight="bold" >
										Saldo Capital <xsl:value-of select="Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt" >
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="4" >
									<fo:block  font-weight="bold"></fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px"  >
									<fo:block font-weight="bold" font-style="italic" >
			
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" >
									<fo:block font-weight="bold" font-style="italic">
				
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px"  number-columns-spanned="2">
									<fo:block  font-weight="bold">
										Gastos <xsl:value-of select="Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="4" >
									<fo:block  font-weight="bold"></fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px"  >
									<fo:block font-weight="bold" font-style="italic" >
			
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" >
									<fo:block font-weight="bold" font-style="italic">
				
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" number-columns-spanned="2">
									<fo:block font-weight="bold" >
										Total <xsl:value-of select="Totales/Total"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				</fo:block>
			</xsl:if>
	</xsl:template>
</xsl:stylesheet>