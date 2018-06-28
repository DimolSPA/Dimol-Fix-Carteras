<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/">
		<fo:root>
			<fo:layout-master-set>
			<!-- Define the cover page. -->
				<fo:simple-page-master master-name="cover" page-height="5.5in"
                             page-width="8.5in" margin-top="0.25in"
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
				<fo:simple-page-master master-name="default-master" page-height="5.5in"
                             page-width="8.5in" margin-top="0in"
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
						<fo:conditional-page-master-reference page-position="rest" master-reference="cover"/>
						<fo:conditional-page-master-reference page-position="last" master-reference="cover"/>
						<!--<fo:conditional-page-master-reference page-position="rest" master-reference="default-master"/>
						<fo:conditional-page-master-reference page-position="last" master-reference="default-master"/>-->
					</fo:repeatable-page-master-alternatives>
				</fo:page-sequence-master>
			</fo:layout-master-set>
			
			<fo:page-sequence master-reference="allPages">
				
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
												<xsl:value-of select="/ArbolJudicial/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/ArbolJudicial/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/ArbolJudicial/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="1px">
											
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
									<fo:block margin-left="1px"  text-align="right">
											<fo:external-graphic src="url('file:///D:/Archivos/Reportes/XSL/logo-reporte.svg')"  content-height="scale-to-fit" width="90px"  scaling="uniform"  />
										</fo:block>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt"  text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/ArbolJudicial/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/ArbolJudicial/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/ArbolJudicial/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/ArbolJudicial/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
					</fo:block>					
				</fo:static-content>
				
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
			                  FECHA EMISION: <xsl:value-of select="/ArbolJudicial/FechaReporte"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:static-content>
			
				<fo:flow flow-name="xsl-region-body">
						
				
					<fo:table display-align="center">						
						<fo:table-body>
						<xsl:call-template name="title">
							<xsl:with-param name="titulo" select="'ARBOL JUDICIAL'"/>
							<xsl:with-param name="cliente" select="'Movimiento de Causas'"/>
							<!--<xsl:with-param name="cliente" select="/ArbolJudicial/Titulo/Cliente"/>
							<xsl:with-param name="rutCliente" select="/ArbolJudicial/Titulo/RutCliente"/>-->
						</xsl:call-template>
							<xsl:call-template name="tablaArbol">
								<xsl:with-param name="rows" select="/ArbolJudicial/lstCategoria"/>
							</xsl:call-template>
							<xsl:call-template name="tablaArbolAbogado">
								<xsl:with-param name="rows" select="/ArbolJudicial/lstArbolAbogado"/>
							</xsl:call-template> 
						</fo:table-body>	
					</fo:table>
					
					<fo:block id="UltimaPagina"/>
				</fo:flow>	
			</fo:page-sequence>
		</fo:root>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
		<xsl:param name="cliente"/>
		<xsl:param name="rutCliente"/>
		<fo:table-row>
			<fo:table-cell number-columns-spanned = "50" text-align="center">
				<fo:block>
					<xsl:value-of select="$titulo"/>
				</fo:block>	
			</fo:table-cell>
		</fo:table-row>
		<fo:table-row>
			<fo:table-cell number-columns-spanned = "50" text-align="center">
				<fo:block>
					<xsl:value-of select="$cliente"/>
				</fo:block>	
			</fo:table-cell>
		</fo:table-row>
		<!--<fo:table-row>
			<fo:table-cell number-columns-spanned = "7">
				<fo:block>
					RUT: <xsl:value-of select="$rutCliente"/>
				</fo:block>	
			</fo:table-cell>
		</fo:table-row>-->
		<fo:table-row>
			<fo:table-cell number-columns-spanned = "7" height="20px"><fo:block font-size="8.0pt">% Actualización: <xsl:value-of select="/ArbolJudicial/Total/PorcTotalConMov"/>%</fo:block></fo:table-cell>
		</fo:table-row>

	</xsl:template>
	
	<xsl:template name="tablaArbol">
		<xsl:param name="rows"/>
				
				<xsl:if test="1 > 0">			
					<xsl:choose>	
						<xsl:when test="$rows/ArbolJudicialCategoria != ''">
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>	
							</fo:table-row>
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count($rows/ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>													
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6"><fo:block font-size="7.0pt" text-align="center">GLOBAL CARTERA</fo:block></fo:table-cell>													
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6"><fo:block font-size="7.0pt" text-align="center">GLOBAL CARTERA</fo:block></fo:table-cell>													
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>													
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count($rows/ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="/ArbolJudicial/Total/CuentaTotal"/></fo:block></fo:table-cell>
													
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="/ArbolJudicial/Total/CuentaTotal"/></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>							
					
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count($rows/ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="/ArbolJudicial/Total/Total"/></fo:block></fo:table-cell>
													
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="/ArbolJudicial/Total/Total"/></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>
					
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count($rows/ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
													<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>							
							
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = 1 ">	
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>			
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
										</xsl:when>
										<xsl:when test="position() = count($rows/ArbolJudicialCategoria)">
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-left-style="solid" height="20px"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell height="20px" border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid" border-right-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="6" padding="1px"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="Nombre"/></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell number-columns-spanned="6" border="solid"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="Totales/CuentaTotal"/></fo:block><xsl:value-of select="Nombre"/></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="Totales/Total"/></fo:block><xsl:value-of select="Nombre"/></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell border-right-style="solid" border-after-style="solid"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-right-style="solid" height="20px"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">													
									<fo:table-cell number-columns-spanned="5" padding="2px"><fo:block font-size="5.0pt" text-align="right">CON MOVIMIENTO</fo:block></fo:table-cell>																			
									<fo:table-cell number-columns-spanned="5" padding="2px"><fo:block font-size="5.0pt" text-align="right">SIN MOVIMIENTO</fo:block></fo:table-cell>																		
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/CuentaConMov"/></fo:block></fo:table-cell>									
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/CuentaSinMov"/></fo:block></fo:table-cell>																	
								</xsl:for-each>	
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="$rows/ArbolJudicialCategoria">	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/TotalConMov"/></fo:block></fo:table-cell>												
									<fo:table-cell><fo:block></fo:block></fo:table-cell>													
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/TotalSinMov"/></fo:block></fo:table-cell>																
								</xsl:for-each>	
							</fo:table-row>	

							<fo:table-row>
								<fo:table-cell height="80px"><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
							</fo:table-row>
							
						</xsl:when>
						<xsl:otherwise>
							<fo:table-row><fo:table-cell><fo:block/></fo:table-cell></fo:table-row>
						</xsl:otherwise>
					</xsl:choose>						
							
				</xsl:if>	
								
	</xsl:template>
	
	
	<xsl:template name="tablaArbolAbogado">
		<xsl:param name="rows"/>
				
				<xsl:if test="1 > 0">	
				
				<xsl:choose>	
					<xsl:when test="$rows/ArbolJudicialAbogado != ''">				
				
					<xsl:for-each select="$rows/ArbolJudicialAbogado">			

						<fo:table-row>
							<fo:table-cell number-columns-spanned="7">
								<fo:block font-size="8.0pt">
									Abogado:
								</fo:block>	
							</fo:table-cell>

							<fo:table-cell number-columns-spanned="12" padding = "2px">
								<fo:block font-size="8.0pt">
									<xsl:value-of select="Nombre"/>
								</fo:block>	
							</fo:table-cell>
							
						</fo:table-row>
						
						
						<xsl:choose>	
							<xsl:when test="lstCateg/ArbolJudicialCategoria != ''">						
						
							<fo:table-row>
								<fo:table-cell number-columns-spanned="7">
									<fo:block font-size="8.0pt">
										% Actualización:
									</fo:block>	
								</fo:table-cell>

								<fo:table-cell number-columns-spanned="12" padding = "2px">
									<fo:block font-size="8.0pt">
										<xsl:value-of select="Tot/PorcTotalConMov"/>%
									</fo:block>	
								</fo:table-cell>
								
							</fo:table-row>
						
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>	
							</fo:table-row>
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count(../ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>													
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6"><fo:block font-size="7.0pt" text-align="center">GLOBAL CARTERA</fo:block></fo:table-cell>													
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6"><fo:block font-size="7.0pt" text-align="center">GLOBAL CARTERA</fo:block></fo:table-cell>													
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>													
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count(../ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="../../Tot/CuentaTotal"/></fo:block></fo:table-cell>
													
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="../../Tot/CuentaTotal"/></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>							
					
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count(../ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="../../Tot/Total"/></fo:block></fo:table-cell>
													
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="../../Tot/Total"/></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>
					
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = round(count(../ArbolJudicialCategoria) div 2) ">
											<xsl:choose>
												<xsl:when test="(position() mod 2) = 0 ">
													<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>
												</xsl:when>
												<xsl:otherwise>	
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
													<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
													<fo:table-cell><fo:block></fo:block></fo:table-cell>
												</xsl:otherwise>
											</xsl:choose>	
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>							
							
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<xsl:choose>
										<xsl:when test="position() = 1 ">	
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>			
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
										</xsl:when>
										<xsl:when test="position() = count(../ArbolJudicialCategoria)">
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-left-style="solid" height="20px"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
											<fo:table-cell><fo:block></fo:block></fo:table-cell>
										</xsl:when>
										<xsl:otherwise>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell height="20px" border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid" border-right-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
											<fo:table-cell border-before-style="solid"><fo:block></fo:block></fo:table-cell>
										</xsl:otherwise>
									</xsl:choose>
								</xsl:for-each>	
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="6" padding="1px"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="Nombre"/></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell number-columns-spanned="6" border="solid"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="Totales/CuentaTotal"/></fo:block><xsl:value-of select="Nombre"/></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell number-columns-spanned="6" border="solid 1px black"><fo:block font-size="7.0pt" text-align="center"><xsl:value-of select="Totales/Total"/></fo:block><xsl:value-of select="Nombre"/></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell height="20px"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell border-right-style="solid" border-after-style="solid"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-after-style="solid"><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-right-style="solid" height="20px"><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell border-right-style="solid"><fo:block></fo:block></fo:table-cell>	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
									<fo:table-cell><fo:block></fo:block></fo:table-cell>
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">													
									<fo:table-cell number-columns-spanned="5" padding="2px"><fo:block font-size="5.0pt" text-align="right">CON MOVIMIENTO</fo:block></fo:table-cell>																			
									<fo:table-cell number-columns-spanned="5" padding="2px"><fo:block font-size="5.0pt" text-align="right">SIN MOVIMIENTO</fo:block></fo:table-cell>																		
								</xsl:for-each>	
							</fo:table-row>	
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/CuentaConMov"/></fo:block></fo:table-cell>									
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/CuentaSinMov"/></fo:block></fo:table-cell>																	
								</xsl:for-each>	
							</fo:table-row>
							
							<fo:table-row>
								<xsl:for-each select="lstCateg/ArbolJudicialCategoria">	
									<fo:table-cell><fo:block></fo:block></fo:table-cell>									
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/TotalConMov"/></fo:block></fo:table-cell>												
									<fo:table-cell><fo:block></fo:block></fo:table-cell>													
									<fo:table-cell number-columns-spanned="4" border="solid"><fo:block font-size="5.0pt" text-align="center"><xsl:value-of select="Totales/TotalSinMov"/></fo:block></fo:table-cell>																
								</xsl:for-each>	
							</fo:table-row>																	
							
							<fo:table-row>
								<fo:table-cell height="110px"><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
								<fo:table-cell><fo:block></fo:block></fo:table-cell>
							</fo:table-row>
							
							</xsl:when>
							<xsl:otherwise>
								<fo:table-row><fo:table-cell><fo:block/></fo:table-cell></fo:table-row>
							</xsl:otherwise>
						</xsl:choose>	
							
					</xsl:for-each>
					
					</xsl:when>
					
					<xsl:otherwise>
						<fo:table-row><fo:table-cell><fo:block/></fo:table-cell></fo:table-row>
					</xsl:otherwise>
				</xsl:choose>
					
					
				</xsl:if>	
								
	</xsl:template>
</xsl:stylesheet>