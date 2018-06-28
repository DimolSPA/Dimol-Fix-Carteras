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
												<xsl:value-of select="/LiquidacionDura/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/LiquidacionDura/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/LiquidacionDura/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="27px">
											<fo:external-graphic src="url('file:///D:/Archivos/Reportes/XSL/logo-reporte.svg')" width="200px" height="80px"  />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/LiquidacionDura/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/LiquidacionDura/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/LiquidacionDura/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/LiquidacionDura/Encabezado/Telefono"/>
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
									<fo:block font-size="6.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="left">
			                  Pág.  <fo:page-number/> de <fo:page-number-citation ref-id="UltimaPagina"/> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block font-size="6.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="/LiquidacionDura/FechaReporte"/>
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
						<xsl:with-param name="titulo" select="'ESTADO DE CUENTA'"/>
						<xsl:with-param name="cliente" select="/LiquidacionDura/Titulo/Cliente"/>
						<xsl:with-param name="rutCliente" select="/LiquidacionDura/Titulo/RutCliente"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/LiquidacionDura/lstDocumentos"/>
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

		<fo:block font-size="8.0pt" font-family="Calibri" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
			<fo:block  font-size="10.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block font-size="10.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$cliente"/>
			</fo:block>
			<fo:block font-size="9.0pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:text>RUT:   </xsl:text>
				<xsl:value-of select="$rutCliente"/>
			</fo:block>
		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>

		<xsl:for-each select="$filas/LiquidacionDuraDeudor">
			<fo:block>
				<fo:block font-size="8.0pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="center">
					<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt"
			                   text-align="left" font-weight="bold" white-space="pre">
						<xsl:text>RUT : </xsl:text>
						<xsl:value-of select="RutDeudorFormateado"/>
						<xsl:text>     NOMBRE : </xsl:text>
						<xsl:value-of select="NombreFantasia"/>
					</fo:block>
					<fo:table font-size="7.0pt">
						<fo:table-column column-width="33%"/>
						<fo:table-column column-width="33%"/>
						<fo:table-column column-width="34%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>CIUDAD: </xsl:text><xsl:value-of select="Ciudad"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>COMUNA: </xsl:text><xsl:value-of select="Comuna"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>REGION: </xsl:text><xsl:value-of select="Region"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>DIRECCION: </xsl:text><xsl:value-of select="Direccion"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>TELEFONO: </xsl:text><xsl:value-of select="Telefono"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>COD. POSTAL: </xsl:text><xsl:value-of select="CodigoPostal"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>CELULAR: </xsl:text><xsl:value-of select="Celular"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>EMAIL: </xsl:text><xsl:value-of select="Email"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>FAX: </xsl:text><xsl:value-of select="Fax"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-weight="bold" >
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>TIPO CAMBIO DOLAR: </xsl:text><xsl:value-of select="/LiquidacionDura/lstDocumentos/LiquidacionDuraDeudor/CambioDolar"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>FECHA EMISION: </xsl:text><xsl:value-of select="/LiquidacionDura/FechaEmision"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>GESTOR: </xsl:text><xsl:value-of select="Gestor"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row font-weight="bold" >
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>TIPO CAMBIO UF: </xsl:text><xsl:value-of select="/LiquidacionDura/lstDocumentos/LiquidacionDuraDeudor/CambioUF"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>AREA: </xsl:text>
										<xsl:choose>
										  <xsl:when test="/LiquidacionDura/EstadoCpbt = 'V'">
											PREJUDICIAL
										  </xsl:when>
										  <xsl:otherwise>
											JUDICIAL
										  </xsl:otherwise>
										</xsl:choose>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				
				<!-- TASAS DE INTERES -->
				<fo:block font-size="8.0pt" font-family="calibri" padding-after="2.0pt" space-before="4.0pt" text-align="center">
					<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" text-align="left" font-weight="bold" white-space="pre">
						<xsl:text>Tasas de Interés:</xsl:text>
					</fo:block>
					
					<fo:table font-size="7.0pt">
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>Pesos: </xsl:text><xsl:value-of select="/LiquidacionDura/Titulo/TasaInteresPesos"/>&#160;%
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>U.F.:&#160;&#160;&#160;</xsl:text><xsl:value-of select="/LiquidacionDura/Titulo/TasaInteresUF"/>&#160;%
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>Dolar: </xsl:text><xsl:value-of select="/LiquidacionDura/Titulo/TasaInteresDolar"/>&#160;%
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" text-align="center" >
						<xsl:text>DETALLE DE CUENTA</xsl:text>
				</fo:block>
				<xsl:for-each select="$filas/LiquidacionDuraDeudor/lstAsegurados/LiquidacionDuraAsegurado">
				<xsl:if test="lstDetallesPesos != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt">
					<xsl:if test="RutSubCartera != ''">
						<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="brown" white-space-collapse="false">
							<xsl:text>DATOS ASEGURADO: </xsl:text> <xsl:value-of select="RutSubCartera"/><xsl:text>   </xsl:text><xsl:value-of select="SubCartera"/>
						</fo:block>
					</xsl:if>
					<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  white-space-collapse="false" >
							<xsl:text>   </xsl:text>
							<xsl:value-of select="'PESOS'"/>
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
									<xsl:when test="$filas/LiquidacionDuraDeudor != ''">
										<xsl:for-each select="lstDetallesPesos/LiquidacionDuraDetalle">
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
										<fo:block  font-weight="bold" font-style="italic" color="blue">SUB-TOTAL PESOS:</fo:block>
									</fo:table-cell>
									<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
										<fo:block font-weight="bold" font-style="italic" >
											<xsl:value-of select="TotalesPesos/Monto"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
										<fo:block font-weight="bold" font-style="italic" >
											<xsl:value-of select="TotalesPesos/Saldo"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
										<fo:block font-weight="bold" font-style="italic" color="red">
											<xsl:value-of select="TotalesPesos/Intereses"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
										<fo:block font-weight="bold" font-style="italic" color="blue">
											<xsl:value-of select="TotalesPesos/Honorarios"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
										<fo:block font-weight="bold" font-style="italic" >
											<xsl:value-of select="TotalesPesos/Gasto"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
										<fo:block font-weight="bold" font-style="italic" >
											<xsl:value-of select="TotalesPesos/Total"/>
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
								<xsl:when test="$filas/LiquidacionDuraDeudor != ''">
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
								<xsl:when test="$filas/LiquidacionDuraDeudor != ''">
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
						<fo:table-body>
							<fo:table-row font-size="6.0pt" color="brown">
								<fo:table-cell text-align="center" border-width="1px" padding="2px" number-columns-spanned="5" font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="brown" white-space-collapse="false">
									<fo:block  font-weight="bold" font-style="italic" >SUB-TOTAL <xsl:value-of select="SubCartera"/></fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Monto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="red">
										<xsl:value-of select="Totales/Intereses"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="Totales/Honorarios"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Gasto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Totales/Total"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
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
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
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
		<fo:block font-size="6.0pt" font-family="calibri" padding-top="30px">
					<fo:table>
						<fo:table-column column-width="90px"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell>
											<fo:block/>
										</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-columns-spanned="7" background-color = "yellow">
									<fo:block font-weight="bold" background-color = "yellow" >SUMARIO TOTAL EN PESOS</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
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
									<fo:block font-weight="bold" >GASTOS PREJUDICIALES</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS JUDICIALES</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >TOTAL EN PESOS</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<fo:table-row font-size="6.0pt">
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/Monto"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
								
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/Intereses"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="blue">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/Honorarios"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/GastoPreJudicial"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/GastoJudicial"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/Totales/Total"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				
	</xsl:template>

</xsl:stylesheet>

