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
				<fo:simple-page-master master-name="cover" page-height="11in"
                             page-width="8.5in" margin-top="0.3125in"
                             margin-bottom="0.3125in" margin-left="0.3125in"
                             margin-right="0.3125in">
					<fo:region-body margin-top="0.25in" margin-bottom="0.25in"/>
				</fo:simple-page-master>

				<!-- Define a body (or default) page. -->
				<fo:simple-page-master master-name="default-master" page-height="11in"
                             page-width="8.5in" margin-top="0.3125in"
                             margin-bottom="0.3125in" margin-left="0.3125in"
                             margin-right="0.3125in">

					<!-- Central part of page -->
					<fo:region-body column-count="1" margin-top="0.625in"
                        margin-bottom="0.3125in"/>

					<!-- Header -->
					<fo:region-before extent="0.625in" />

					<!-- Footer -->
					<fo:region-after extent="0.0625in"/>
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
							<fo:table-column column-width="30%"/>
							<fo:table-column column-width="40%"/>
							<fo:table-column column-width="30%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="6.25pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Giro"/>
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
										<fo:block font-size="6.25pt" font-family="serif" padding-after="2.0pt"  text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/TrekkingCartera/Encabezado/Telefono"/>
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
									<fo:block font-size="6.25pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="left">
			                  Pág.  <fo:page-number/> de <fo:page-number-citation ref-id="UltimaPagina"/> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block font-size="7.5pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="/TrekkingCartera/FechaReporte"/>
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
						<xsl:with-param name="titulo" select="'REPORTE DE TRACKING CARTERA'"/>
						<xsl:with-param name="cliente" select="/TrekkingCartera/FechaEmision"/>
						<xsl:with-param name="rutCliente" select="/TrekkingCartera/Titulo/Cliente"/>						
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/TrekkingCartera/lstGestores"/>
					</xsl:call-template>
					<xsl:call-template name="tablaRanking">
						<xsl:with-param name="rows" select="/TrekkingCartera/lstRanking"/>
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

		<fo:block font-size="8.75pt" font-family="Calibri" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
			<fo:block  font-size="8.75pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block font-size="6.25pt" font-family="Calibri" padding-after="2.0pt"
			                   text-align="center">
							  <xsl:text>AL DIA   </xsl:text>							  
				<xsl:value-of select="$cliente"/>				
			</fo:block>	
			<xsl:if test ="/TrekkingCartera/Pclid != 0">
				<fo:block font-size="7.0pt" font-family="Calibri" padding-after="2.0pt"
								   text-align="center">
					<xsl:value-of select="$rutCliente"/>
				</fo:block>	
			</xsl:if>			
		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>

		<xsl:for-each select="$filas/TrekkingCarteraGestor">
			<fo:block margin-bottom="20px">
				<fo:block font-size="6.25pt" font-family="calibri" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="center">
					<fo:table display-align="center" keep-with-previous.within-page="always" page-break-inside="avoid">
						<fo:table-column column-width="20%"/>
						<fo:table-column column-width="20%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell text-align="left" color="blue">
									<fo:block font-weight="bold">GESTOR:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" color="blue">
									<fo:block font-weight="bold">
										<xsl:value-of select="NombreGestor"/>
									</fo:block>	
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell text-align="left">
									<fo:block font-weight="bold">Nº DEUDORES ASIGNADOS:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="TotalGestor/NumCasos"/>
									</fo:block>	
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell text-align="left">
									<fo:block font-weight="bold">MONTO ASIGNADO:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="TotalGestor/Saldo"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell text-align="left">
									<fo:block font-weight="bold">TRACKING CASOS:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="TotalGestor/PorcCasos"/>%
									</fo:block>	
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell text-align="left">
									<fo:block font-weight="bold">TRACKING MONTO:</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right">
									<fo:block font-weight="bold">
										<xsl:value-of select="TotalGestor/PorcSaldo"/>%
									</fo:block>	
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:block>
				
				<fo:block font-size="8.75pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic" >
						<xsl:text/>
				</fo:block>
				<fo:block font-size="4.75pt" font-family="calibri">
					<fo:table display-align="center" keep-with-previous.within-page="always" page-break-inside="avoid">
						<fo:table-column column-width="19%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="1%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="1%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="1%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="1%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="1%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-header>							
							<fo:table-row>						
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">Cliente</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">TOTAL Nº CASOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONTO TOTAL</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >0 - 7 DIAS (Nº CASOS)</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">8 - 15 DIAS (Nº CASOS)</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">16 - 30 DIAS (Nº CASOS)</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">SOBRE 30 DIAS (Nº CASOS)</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >% Act. Sobre El Cliente</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="lstClientes/TrekkingCarteraCliente != ''">
									<xsl:for-each select="lstClientes/TrekkingCarteraCliente">
										<fo:table-row>									
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="NombreCliente"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="TotalCasos"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="TotalSaldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>													
													<xsl:value-of select="CasosBloque1"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="SaldoBloque1"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="CasosBloque2"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="SaldoBloque2"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="CasosBloque3"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="SaldoBloque3"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="CasosBloque4"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="SaldoBloque4"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell>
												<fo:block/>
											</fo:table-cell>											
											<fo:table-cell border="solid" border-width="1px" text-align="right">
												<fo:block>	
													
														<xsl:choose>
															<xsl:when test="Flag = 1">
																<fo:inline font-weight="bold" background-color="red" border="solid">&#160;&#160;&#160;</fo:inline>
															</xsl:when>
															<xsl:when test="Flag = 2">
																<fo:inline font-weight="bold" background-color="yellow" border="solid">&#160;&#160;&#160;</fo:inline>
															</xsl:when>
															<xsl:when test="Flag = 3">
																<fo:inline font-weight="bold" background-color="green" border="solid">&#160;&#160;&#160;</fo:inline>
															</xsl:when>
															<xsl:otherwise>
																<fo:inline font-weight="bold">&#160;&#160;&#160;</fo:inline>
															</xsl:otherwise>
														</xsl:choose>	
														<fo:inline><xsl:value-of select="Actualiza"/>%</fo:inline>
													
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
									<fo:block font-weight="bold">Total general</fo:block>
								</fo:table-cell>								
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic">
										<xsl:value-of select="TotalGestor/NumCasos"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic">
										<xsl:value-of select="TotalGestor/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>								
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso1/NumCasos"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso1/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>								
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso2/NumCasos"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso2/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>								
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso3/NumCasos"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso3/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>								
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso4/NumCasos"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="2px" text-align="right">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="Caso4/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block/>
								</fo:table-cell>								
								<fo:table-cell text-align="right">
									<fo:block font-weight="bold" font-style="italic" >	
										<xsl:choose>
											<xsl:when test="TotalGestor/Flag = 1">
												<fo:inline font-weight="bold" background-color="red" border="solid">&#160;&#160;&#160;</fo:inline>
											</xsl:when>
											<xsl:when test="TotalGestor/Flag = 2">
												<fo:inline font-weight="bold" background-color="yellow" border="solid">&#160;&#160;&#160;</fo:inline>
											</xsl:when>
											<xsl:when test="TotalGestor/Flag = 3">
												<fo:inline font-weight="bold" background-color="green" border="solid">&#160;&#160;&#160;</fo:inline>
											</xsl:when>
											<xsl:otherwise>
												<fo:inline font-weight="bold">&#160;&#160;&#160;</fo:inline>
											</xsl:otherwise>
										</xsl:choose>
										<fo:inline><xsl:value-of select="TotalGestor/PorcCasos"/>%</fo:inline>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>

					</fo:table>
				
				</fo:block>
							
			</fo:block>
		</xsl:for-each>
		
		
	</xsl:template>
	
	<xsl:template name="tablaRanking">
		<xsl:param name="rows"/>

		
			<fo:block  page-break-inside="auto">
				
				<xsl:if test="$rows/TrekkingCarteraRanking/lstDeudores/TrekkingCarteraDeudor != ''">
				<fo:block font-size="8.75pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic" >
						<xsl:text/>
				</fo:block>
				<fo:block font-size="6.25pt" font-family="calibri">
					<fo:table display-align="center">
						<!--<fo:table-column column-width="15mm"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="35%"/>
						<fo:table-column column-width="12%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-column column-width="10%"/>-->
					
						<fo:table-header>	
							<fo:table-row>
								
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center" number-columns-spanned="6">
									<fo:block font-weight="bold">RANKING INCIDENCIA CARTERA</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">CLIENTE</fo:block>
								</fo:table-cell>								
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">RUT DEUDOR</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">NOMBRE DEUDOR</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">Nº DOCUMENTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">SALDO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">CATEGORIA</fo:block>
								</fo:table-cell>							
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$rows/TrekkingCarteraRanking/lstDeudores/TrekkingCarteraDeudor != ''">
									<xsl:for-each select="$rows/TrekkingCarteraRanking/lstDeudores/TrekkingCarteraDeudor">
										<fo:table-row>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="NombreCliente"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="RutDeudor"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<xsl:value-of select="NombreDeudor"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="NumDocs"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>													
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="Categoria"/>
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
							
						</fo:table-body>

					</fo:table>
				
				</fo:block>
				</xsl:if>	
		
				
			</fo:block>
						
	</xsl:template>

</xsl:stylesheet>


