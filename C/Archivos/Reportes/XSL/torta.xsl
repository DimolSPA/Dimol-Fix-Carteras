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
							<fo:table-column column-width="32%"/>
							<fo:table-column column-width="34%"/>
							<fo:table-column column-width="34%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block>
											<fo:external-graphic content-width="scale-to-fit" src="url('file:///C:/Archivos/Reportes/XSL/logo-reporte.svg')" width="100%" content-height="100%" scaling="non-uniform" />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/TortaAgrupada/Encabezado/Telefono"/>
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
						<!--<xsl:with-param name="rutCliente" select="'COMERCIAL AGRORAMA LTDA.'"/>-->
						<xsl:with-param name="rutCliente" select="/TortaAgrupada/Titulo/Cliente"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/TortaAgrupada/lstDocumentos"/>
					</xsl:call-template>
					<xsl:call-template name="tablaRanking">
						<xsl:with-param name="rows" select="/TortaAgrupada/lstRanking"/>
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
				<fo:block margin-left="17mm" font-size="5.0pt" font-family="calibri" padding-after="2.0pt"
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
					<fo:table display-align="center">
						<fo:table-column column-width="17mm"/>
						<fo:table-column column-width="25%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<!--<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>-->
						<fo:table-header>
							<!--<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-rows-spanned="2">
									<fo:block font-weight="bold">ESTADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center"  number-columns-spanned="2">
									<fo:block font-weight="bold">CANTIDAD</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-columns-spanned="4">
									<fo:block font-weight="bold" >TOTALES</fo:block>
								</fo:table-cell>
							</fo:table-row>-->
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>							
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">ESTADO</fo:block>
								</fo:table-cell>
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
									<fo:block font-weight="bold" >% MONTO</fo:block>
								</fo:table-cell>
								<!--<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">COMPROMISO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >SALDO</fo:block>
								</fo:table-cell>-->
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$filas/TortaCliente != ''">
									<xsl:for-each select="lstEstados/TortaEstado">
										<fo:table-row>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>										
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
													<!--<xsl:value-of select="Monto"/>-->
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right"  color="blue">
												<fo:block>
													<xsl:value-of select="Regularizado"/>%
												</fo:block>
											</fo:table-cell>
											<!--<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="Compromiso"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>-->
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
										<!--<fo:table-cell>
											<fo:block/>
										</fo:table-cell>-->
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							<fo:table-row font-size="5.0pt">
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>								
								<fo:table-cell text-align="center" border-width="1px" padding="2px" number-columns-spanned="3">
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell  text-align="right" border-width="1px" padding="2px">
									<fo:block font-weight="bold" font-style="italic">
										<!--<xsl:value-of select="SubTotal/Monto"/>-->
										<xsl:value-of select="SubTotal/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="3px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="SubTotal/Regularizado"/>%
									</fo:block>
								</fo:table-cell>
								<!--<fo:table-cell text-align="right" border-width="0px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="SubTotal/Compromiso"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding="2px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="SubTotal/Saldo"/>
									</fo:block>
								</fo:table-cell>-->
							</fo:table-row>
						</fo:table-body>

					</fo:table>
				
				</fo:block>
				</xsl:if>
				
				
				
				
			</fo:block>
		</xsl:for-each>
		<xsl:if test="/TortaAgrupada/Totales != ''">
			<fo:block font-size="7.0pt" font-family="calibri" font-weight="bold" font-style="italic" >
						<xsl:text/>						
				</fo:block>
				
				
				<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table display-align="center" keep-with-previous.within-page="always"  page-break-inside="avoid">	<fo:table-column column-width="17mm"/>						
						<fo:table-column column-width="25%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-body>
							<fo:table-row>	
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="center" number-columns-spanned="2">
									<fo:block font-weight="bold">
										RESUMEN
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>		
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>							
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="left">
									<fo:block font-weight="bold">
										TOTAL DEUDORES
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="/TortaAgrupada/Totales/Deudores"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							
							<fo:table-row>	
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="left">
									<fo:block font-weight="bold">
										TOTAL DOCUMENTOS
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="/TortaAgrupada/Totales/Documentos"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="left">
									<fo:block font-weight="bold">
										TOTAL CARTERA
									</fo:block>
								</fo:table-cell>
								<fo:table-cell border="dotted" border-width="1px" padding="2px" text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="/TortaAgrupada/Totales/Saldo"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				
				</fo:block>
				
		</xsl:if>
		<!--<xsl:if test="/TortaAgrupada/Totales != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic" >
						<xsl:text/>
						
				</fo:block>
				<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table display-align="center">
						<fo:table-column column-width="17mm"/>
						<fo:table-column column-width="25%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-body>

				

										<fo:table-row>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>											
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left" background-color="grey">
												<fo:block font-weight="bold" color="darkblue" background-color="grey">
													TOTAL RESUMEN
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" background-color="grey">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Deudores"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" background-color="grey">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Documentos"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" background-color="grey">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Monto"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" background-color="grey">
												<fo:block font-weight="bold" color="darkblue">
													<xsl:value-of select="/TortaAgrupada/Totales/Regularizado"/>100%
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
					<fo:table  keep-with-previous.within-page="always"  page-break-inside="avoid"  display-align="center">
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
				</xsl:if>-->
	</xsl:template>
	
	<xsl:template name="tablaRanking">
		<xsl:param name="rows"/>

		
			<fo:block  page-break-inside="auto">
				
				<xsl:if test="1 > 0">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic" >
						<xsl:text/>
				</fo:block>
				<fo:block font-size="5.0pt" font-family="calibri">
					<fo:table display-align="center" keep-with-previous.within-page="always" page-break-inside="avoid">
						<fo:table-column column-width="14mm"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="35%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>
					
						<fo:table-header>	
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-columns-spanned="5">
									<fo:block font-weight="bold">RANKING INCIDENCIA CARTERA</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>							
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">RUT</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">NOMBRE</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">DOCUMENTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center"  color="blue">
									<fo:block font-weight="bold">% MONTO</fo:block>
								</fo:table-cell>							
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$rows/TortaRanking != ''">
									<xsl:for-each select="$rows/TortaRanking">
										<fo:table-row>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>										
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Rut"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="Nombre"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="Documentos"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>													
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" color="blue">
												<fo:block>
													<xsl:value-of select="PorcSaldo"/>%
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
										
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							
						</fo:table-body>

					</fo:table>
				
				</fo:block>
				</xsl:if>	
		
				
			</fo:block>
						
	</xsl:template>

</xsl:stylesheet>


