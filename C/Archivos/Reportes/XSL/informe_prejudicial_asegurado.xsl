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
												<xsl:value-of select="/InformePrejudicial/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/InformePrejudicial/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/InformePrejudicial/Encabezado/Giro"/>
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
												<xsl:value-of select="/InformePrejudicial/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/InformePrejudicial/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/InformePrejudicial/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/InformePrejudicial/Encabezado/Telefono"/>
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
									<fo:block font-size="5.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="/InformePrejudicial/FechaReporte"/>
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
						<xsl:with-param name="titulo" select="'RESUMEN CARTERA CLIENTE PREJUDICIAL POR ASEGURADO'"/>
						<xsl:with-param name="cliente" select="/InformePrejudicial/Titulo/Cliente"/>
						<xsl:with-param name="rutCliente" select="/InformePrejudicial/FechaLarga"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="listaAsegurados">
						<xsl:with-param name="asegurado" select="/InformePrejudicial/lstAsegurados"/>
					</xsl:call-template>
					<fo:block font-size="6.0pt" font-family="calibri" padding-top="3px">
					<fo:table>
						<fo:table-column column-width="120px"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="10%"/>

						<fo:table-body>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" >
									<fo:block>
										TOTAL FINAL
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" color="blue" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right">
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" font-style="italic" >
									<fo:block>
										TIPO CAMBIO UF: <xsl:value-of select="/InformePrejudicial/CambioUF"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" color="blue" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right">
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" font-style="italic" >
									<fo:block>
										TIPO CAMBIO DOLAR: <xsl:value-of select="/InformePrejudicial/CambioDolar"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" color="blue" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right">
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
					<fo:block id="UltimaPagina"/>
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
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

	<xsl:template name="tabla">
		<xsl:param name="filas"/>

		<xsl:for-each select="$filas/InformePrejudicialDeudor">
			<fo:block>
					<xsl:if test="lstAsegurados/InformePrejudicialAsegurado/RutSubCartera != ''">
						<fo:block font-size="7.0pt" font-family="calibri" padding-after="1.0pt" font-weight="bold" font-style="italic"  color="green" white-space-collapse="false">
							<xsl:text>ASEGURADO: </xsl:text><xsl:value-of select="lstAsegurados/InformePrejudicialAsegurado/SubCartera"/>
						</fo:block>
					</xsl:if>
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center" 
							  border-style="solid" border-width="1" border-color="#000" keep-together.within-column="always" >

					<fo:table font-size="6.0pt" margin="1mm">
						<fo:table-column column-width="50%"/>
						<fo:table-column column-width="50%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell padding="1px" text-align="left" font-weight="bold" >
									<fo:block>
										<xsl:text>NOMBRE: </xsl:text><xsl:value-of select="NombreFantasia"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="1px" text-align="left">
									<fo:block>
										<xsl:text>DIRECCION: </xsl:text><xsl:value-of select="Direccion"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell  padding="1px" text-align="left" font-weight="bold" >
									<fo:block>
										<xsl:text>RUT: </xsl:text><xsl:value-of select="RutDeudorFormateado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="1px" text-align="left">
									<fo:block>
										<xsl:value-of select="Ciudad"/><xsl:text>, </xsl:text><xsl:value-of select="Comuna"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="1px" text-align="left">
									<fo:block>
										<xsl:text>ULTIMA GESTION: </xsl:text><xsl:value-of select="UltimaGestion"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="1px" text-align="left">
									<fo:block>
										<xsl:text>GESTION: </xsl:text><xsl:value-of select="Gestion"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-weight="bold" >
								<fo:table-cell padding="1px" text-align="left" number-columns-spanned="2">
									<fo:block>
										<xsl:text >RESUMEN: </xsl:text>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row >
								<fo:table-cell padding="1px" text-align="left" number-columns-spanned="2">
									<fo:block>
										<xsl:value-of select="Resumen"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>

				<xsl:for-each select="lstAsegurados/InformePrejudicialAsegurado">
				<xsl:if test="lstDetallesPesos != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding="2mm" font-weight="bold" text-align="left" color="brown" >
						<xsl:text>NEGOCIO:</xsl:text><xsl:value-of select="Negocio"/>
				</fo:block>
				<fo:block font-size="6.0pt" font-family="calibri" padding-after="2.0pt">
				<fo:block font-size="6.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="14%"/>

						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">NUMERO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">TIPO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" keep-together="" >F. ING.</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >F. VEN.</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONEDA</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">CAPITAL</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS</fo:block>
								</fo:table-cell>
								
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >ASIGNADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >ABONO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >SALDO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="left">
									<fo:block font-weight="bold" >ULT. ESTADO</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$filas/InformePrejudicialDeudor != ''">
									<xsl:for-each select="lstDetallesPesos/InformePrejudicialDetalle">
										<fo:table-row font-size="5.0pt">
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Numero"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="TipoDocumento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaIngreso"/>
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
													<xsl:value-of select="Capital"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Gasto"/>
												</fo:block>
											</fo:table-cell>
											
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Asignado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right" color="blue">
												<fo:block>
													<xsl:value-of select="Abono"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="UltimoEstado"/>
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
							<fo:table-row font-size="5.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" border-before-style="solid" border-before-width="1pt">
									<fo:block  font-weight="bold" font-style="italic" color="brown">TOTAL NEGOCIO:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px"  border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px"  border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic">
										<xsl:value-of select="TotalesPesos/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="TotalesPesos/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px" border-before-style="solid" border-before-width="1pt">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				</fo:block>
				</xsl:if>
				
				<xsl:if test="lstDetallesDolares != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" >
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  white-space-collapse="false" >
						<xsl:text>    </xsl:text><xsl:value-of select="'DOLAR'"/>
						
				</fo:block>
				<fo:block font-size="6.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-column column-width="5%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">TIPO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">NUMERO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" keep-together="" >FECHA EMISION</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >FECHA VENC.</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >DIAS VENC.</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >SALDO</fo:block>
								</fo:table-cell>
								
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >INTERESES</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >HONORARIOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >TOTAL</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >ESTADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MOTIVO</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$filas/InformePrejudicialDeudor != ''">
									<xsl:for-each select="lstDetallesDolares/LiquidacionDuraDetalle">
										<fo:table-row font-size="6.0pt">
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="TipoDocumento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Numero"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaEmision"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaVencimiento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="DiasVencido"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Monto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="Intereses"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="blue">
												<fo:block>
													<xsl:value-of select="Honorarios"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Gasto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Total"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Estado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Motivo"/>
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
										<fo:table-cell>
											<fo:block/>
										</fo:table-cell>
										<fo:table-cell>
											<fo:block/>
										</fo:table-cell>
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							<fo:table-row font-size="6.0pt">
								<fo:table-cell text-align="center" border-width="1px" padding="2px" number-columns-spanned="5">
									<fo:block  font-weight="bold" font-style="italic" color="blue">SUB-TOTAL DOLAR:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesDolar/Monto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesDolar/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="red">
										<xsl:value-of select="TotalesDolar/Intereses"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="TotalesDolar/Honorarios"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesDolar/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesDolar/Total"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				</fo:block>
				
				</xsl:if>
				
				<xsl:if test="lstDetallesUF != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  white-space-collapse="false" >
						<xsl:text>    </xsl:text><xsl:value-of select="'UF'"/>
						
				</fo:block>
				<fo:block font-size="6.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-column column-width="5%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="8%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">TIPO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">NUMERO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" keep-together="" >FECHA EMISION</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >FECHA VENC.</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >DIAS VENC.</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >SALDO</fo:block>
								</fo:table-cell>
								
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >INTERESES</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >HONORARIOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >TOTAL</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >ESTADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MOTIVO</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$filas/InformePrejudicialDeudor != ''">
									<xsl:for-each select="lstDetallesUF/LiquidacionDuraDetalle">
										<fo:table-row font-size="6.0pt">
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="TipoDocumento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Numero"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaEmision"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaVencimiento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="DiasVencido"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Monto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="Intereses"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="blue">
												<fo:block>
													<xsl:value-of select="Honorarios"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Gasto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Total"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Estado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Motivo"/>
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
										<fo:table-cell>
											<fo:block/>
										</fo:table-cell>
										<fo:table-cell>
											<fo:block/>
										</fo:table-cell>
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							<fo:table-row font-size="6.0pt">
								<fo:table-cell text-align="center" border-width="1px" padding="2px" number-columns-spanned="5">
									<fo:block  font-weight="bold" font-style="italic" color="blue">SUB-TOTAL UF:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesUF/Monto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesUF/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="red">
										<xsl:value-of select="TotalesUF/Intereses"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="TotalesUF/Honorarios"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesUF/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesUF/Total"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				</fo:block>
				</xsl:if>
				<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="14%"/>
						<fo:table-body>
							<fo:table-row font-size="5.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" font-size="6.0pt" 
								font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="green" 
								white-space-collapse="false" border-before-style="solid" border-before-width="1pt">
									<fo:block  font-weight="bold" font-style="italic" >TOTAL <xsl:value-of select="SubCartera"/></fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="Totales/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px" border-before-style="solid" border-before-width="1pt">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="7.0pt" color="brown">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="brown" white-space-collapse="false">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				</xsl:for-each>
			</fo:block>
		</xsl:for-each>
		<fo:block font-size="6.0pt" font-family="calibri" padding-top="3px">
					<fo:table>
						<fo:table-column column-width="120px"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="10%"/>

						<fo:table-body>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" >
									<fo:block>
										TOTAL FINAL
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" color="blue" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
										<xsl:value-of select="/InformePrejudicial/Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right">
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" font-style="italic" >
									<fo:block>
										TIPO CAMBIO UF: <xsl:value-of select="/InformePrejudicial/CambioUF"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" color="blue" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right">
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell  padding="2px" text-align="left" font-weight="bold" font-style="italic" >
									<fo:block>
										TIPO CAMBIO DOLAR: <xsl:value-of select="/InformePrejudicial/CambioDolar"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" color="blue" font-weight="bold" >
									<fo:block>
					
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right" font-weight="bold" >
									<fo:block>

									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="2px" text-align="right">
									<fo:block>
								
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
	</xsl:template>

	<xsl:template name="listaAsegurados">
		<xsl:param name="asegurado"/>
		<xsl:for-each select="$asegurado/InformePrejudicialAsegurado">
			<fo:block font-size="7.0pt" font-family="calibri" padding-left="4px" font-weight="bold" text-align="left" color="seagreen" font-style="italic">
						<xsl:text>ASEGURADO: </xsl:text><xsl:value-of select="SubCartera"/>
			</fo:block>
			<xsl:call-template name="listaDeudores">
				<xsl:with-param name="deudor" select="lstDocumentos"/>
			</xsl:call-template>
			<fo:block font-size="6.0pt" font-family="calibri" padding-bottom="1px"  >
			<fo:table>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="14%"/>
						<fo:table-body>
							<fo:table-row font-size="5.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" font-size="5.0pt" 
								font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  
								white-space-collapse="false" border-before-style="solid" border-before-width="1pt">
									<fo:block  font-weight="bold" font-style="italic" wrap-option="wrap" color="seagreen"  >TOTAL <xsl:value-of select="SubCartera"/></fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="Totales/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px" border-before-style="solid" border-before-width="1pt">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="7.0pt" color="brown">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="brown" white-space-collapse="false">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
		</xsl:for-each>
	</xsl:template>
	
	<xsl:template name="listaDeudores">
		<xsl:param name="deudor"/>
		<xsl:for-each select="$deudor/InformePrejudicialDeudor">
		<fo:block  keep-together.within-column="always">
					<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center" 
							  border-style="solid" border-width="1" border-color="#000" >

					<fo:table font-size="6.0pt" margin="1mm">
						<fo:table-column column-width="50%"/>
						<fo:table-column column-width="50%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell padding="1px" text-align="left" font-weight="bold" >
									<fo:block>
										<xsl:text>NOMBRE: </xsl:text><xsl:value-of select="NombreFantasia"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="1px" text-align="left">
									<fo:block>
										<xsl:text>DIRECCION: </xsl:text><xsl:value-of select="Direccion"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell  padding="1px" text-align="left" font-weight="bold" >
									<fo:block>
										<xsl:text>RUT: </xsl:text><xsl:value-of select="RutDeudorFormateado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="1px" text-align="left">
									<fo:block>
										<xsl:value-of select="Ciudad"/><xsl:text>, </xsl:text><xsl:value-of select="Comuna"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="1px" text-align="left">
									<fo:block>
										<xsl:text>ULTIMA GESTION: </xsl:text><xsl:value-of select="UltimaGestion"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="1px" text-align="left">
									<fo:block>
										<xsl:text>GESTION: </xsl:text><xsl:value-of select="Gestion"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-weight="bold" >
								<fo:table-cell padding="1px" text-align="left" number-columns-spanned="2">
									<fo:block>
										<xsl:text >RESUMEN: </xsl:text>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row >
								<fo:table-cell padding="1px" text-align="left" number-columns-spanned="2">
									<fo:block>
										<xsl:value-of select="Resumen"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
			<xsl:call-template name="listaCausas">
				<xsl:with-param name="causa" select="lstNegocios"/>
			</xsl:call-template>
			<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="14%"/>
						<fo:table-body>
							<fo:table-row font-size="5.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" font-size="5.0pt" 
								font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="blue" 
								white-space-collapse="false" border-before-style="solid" border-before-width="1pt">
									<fo:block  font-weight="bold" font-style="italic" wrap-option="wrap">TOTAL <xsl:value-of select="NombreFantasia"/></fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="Totales/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px" border-before-style="solid" border-before-width="1pt">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-size="7.0pt" color="brown">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="brown" white-space-collapse="false">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
			</fo:block>
		</xsl:for-each>	
	</xsl:template>
	
	<xsl:template name="listaCausas">
		<xsl:param name="causa"/>
		<xsl:for-each select="$causa/InformePrejudicialNegocio">
	
			<xsl:call-template name="listaDocumentos">
				<xsl:with-param name="documento" select="lstDetallesPesos"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	
	<xsl:template name="listaDocumentos">
		<xsl:param name="documento"/>
		<xsl:if test=". != ''">
				<fo:block font-size="6.0pt" font-family="calibri" padding-after="2.0pt">
				<fo:block font-size="6.0pt" font-family="calibri">
					<fo:block font-size="7.0pt" font-family="calibri" padding="2mm" font-weight="bold" text-align="left" color="brown" >
							<xsl:text>NEGOCIO:</xsl:text><xsl:value-of select="Negocio"/>
					</fo:block>
					<fo:table>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="14%"/>

						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">NUMERO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">TIPO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" keep-together="" >F. ING.</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >F. VEN.</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONEDA</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold">CAPITAL</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS</fo:block>
								</fo:table-cell>
								
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >ASIGNADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >ABONO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
									<fo:block font-weight="bold" >SALDO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="left">
									<fo:block font-weight="bold" >ULT. ESTADO</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="./lstDetallesPesos/InformePrejudicialDetalle != ''">
									<xsl:for-each select="./lstDetallesPesos/InformePrejudicialDetalle">
										<fo:table-row font-size="5.0pt">
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Numero"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="TipoDocumento"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaIngreso"/>
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
													<xsl:value-of select="Capital"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Gasto"/>
												</fo:block>
											</fo:table-cell>
											
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Asignado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right" color="blue">
												<fo:block>
													<xsl:value-of select="Abono"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="UltimoEstado"/>
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
							<fo:table-row font-size="5.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="5" border-before-style="solid" border-before-width="1pt">
									<fo:block  font-weight="bold" font-style="italic" color="brown">TOTAL NEGOCIO:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px"  border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Capital"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px"  border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic">
										<xsl:value-of select="TotalesPesos/Asignado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="TotalesPesos/Abono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px" border-before-style="solid" border-before-width="1pt">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px" border-before-style="solid" border-before-width="1pt">
									<fo:block/>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				</fo:block>
				</xsl:if>
	</xsl:template>

</xsl:stylesheet>

