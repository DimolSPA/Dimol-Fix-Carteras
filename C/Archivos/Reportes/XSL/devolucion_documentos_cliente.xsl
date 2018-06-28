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
					<fo:region-body column-count="1" margin-top="0.25in" margin-bottom="0.25in"/>

					<!-- Header -->
					<!--<fo:region-before extent="0.5in" />-->

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
				<!--<fo:static-content flow-name="xsl-region-before">
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
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block>
											<fo:external-graphic content-width="scale-to-fit" src="url('file:///C:/Archivos/Reportes/XSL/logo-reporte.svg')" width="100%" content-height="100%" scaling="non-uniform" />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
					</fo:block>					
				</fo:static-content>-->

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
			                  FECHA EMISION: <xsl:value-of select="/DevolucionDocumentosCliente/FechaReporte"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:static-content>

				<!-- The main contents of the body page, that is, the catalog
           entries -->
				<fo:flow flow-name="xsl-region-body">						
						<fo:table>
							<fo:table-column column-width="33%"/>
							<fo:table-column column-width="34%"/>
							<fo:table-column column-width="33%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block>
											<fo:external-graphic content-width="scale-to-fit" src="url('file:///C:/Archivos/Reportes/XSL/logo-reporte.svg')" width="100%" content-height="100%" scaling="non-uniform" />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/DevolucionDocumentosCliente/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
						
					<xsl:call-template name="title">			
						<xsl:with-param name="titulo" select="'DEVOLUCION DOCUMENTOS AL CLIENTE'"/>
						<xsl:with-param name="cliente" select="/DevolucionDocumentosCliente/Titulo/Cliente"/>
						<xsl:with-param name="rutDeudor" select="/DevolucionDocumentosCliente/RutUsuario"/>
						<xsl:with-param name="nombreDeudor" select="/DevolucionDocumentosCliente/NombreUsuario"/>
						<xsl:with-param name="sucursal" select="/DevolucionDocumentosCliente/SucursalCliente"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/DevolucionDocumentosCliente"/>
					</xsl:call-template>

					<fo:block id="UltimaPagina"/>
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
		<xsl:param name="cliente"/>
		<xsl:param name="rutDeudor"/>
		<xsl:param name="nombreDeudor"/>
		<xsl:param name="sucursal"/>

		<fo:block font-size="8.0pt" font-family="sans-serif" padding-after="2.0pt" padding-before="10px"
			                  space-before="4.0pt" text-align="center">
			<fo:block  font-size="10.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center" font-weight="bold">
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block font-size="9.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="2.0pt" text-align="center">
				<xsl:value-of select="$cliente"/>
			</fo:block>
			
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="left">
				<xsl:text>&#160;</xsl:text>
			</fo:block>
			
			<fo:table display-align="center">
				<fo:table-body>
					<fo:table-row>
						<fo:table-cell text-align="left">
							<fo:block>
								<xsl:text>Santiago, </xsl:text><xsl:value-of select="/DevolucionDocumentosCliente/FechaComprobante"/>
							</fo:block>
						</fo:table-cell>

						<fo:table-cell text-align="right">
							<fo:block font-weight="bold">
								<xsl:text>FOLIO: </xsl:text><xsl:value-of select="/DevolucionDocumentosCliente/Cbcnumero"/><xsl:text>-</xsl:text><xsl:value-of select="/DevolucionDocumentosCliente/NumRegistro"/>
							</fo:block>
						</fo:table-cell>
					</fo:table-row>
				</fo:table-body>
			</fo:table>
			
			<fo:block padding="4px" />
			
			<!--<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="left">
				<xsl:text>Santiago, </xsl:text><xsl:value-of select="/DevolucionDocumentosCliente/FechaComprobante"/>
				<fo:block text-align="right" font-weight="bold">
					<xsl:text>FOLIO: </xsl:text><xsl:value-of select="/DevolucionDocumentosCliente/Cbcnumero"/>
				</fo:block>
			</fo:block>-->

			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="left" font-weight="bold">
				<xsl:text>Señor(es) </xsl:text>
			</fo:block>
			<!--<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left" font-weight="bold">
				<xsl:value-of select="$nombreDeudor"/>
			</fo:block>
			
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left" font-weight="bold">
				<xsl:value-of select="$rutDeudor"/>
			</fo:block>-->
			
			<fo:table display-align="center">
				<fo:table-column column-width="70%" />
				<fo:table-column />
				<fo:table-body>
					<fo:table-row>
						<fo:table-cell text-align="left">
							<fo:block font-weight="bold">
								<xsl:value-of select="$nombreDeudor"/>
							</fo:block>
						</fo:table-cell>

						<fo:table-cell text-align="right">
							<fo:block font-weight="bold">
								<xsl:text>RUT: </xsl:text><xsl:value-of select="$rutDeudor"/>
							</fo:block>
						</fo:table-cell>
					</fo:table-row>
				</fo:table-body>
			</fo:table>
			
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left">
				<xsl:value-of select="$sucursal/Direccion"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left">
				<xsl:value-of select="$sucursal/Comuna"/>
			</fo:block>
						<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left" text-decoration="underline">
				<xsl:value-of select="$sucursal/Ciudad"/>
			</fo:block>
			<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  text-align="left" space-after="3.0pt">
				<xsl:text>De nuestra Consideración: </xsl:text>
			</fo:block>
			<fo:block padding="5px"/>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  text-align="left" space-after="3.0pt">
				<xsl:text>Se realiza la entrega de el(los) siguientes documento(s) individualizados, pertenecientes a: </xsl:text>
			</fo:block>
			

		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>
		
		<xsl:for-each select="$filas/Deudor">
			
			<fo:block font-size="6.0pt" font-family="serif">
				<fo:table display-align="center">
					<fo:table-column column-width="8%"/>
					<fo:table-column />
					<fo:table-column column-width="10%"/>
					<fo:table-column />
					<fo:table-body>
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">RUT:</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="RutDeudor"/>
								</fo:block>
							</fo:table-cell>	
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">DEUDOR:</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="NombreDeudor"/>
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<!--<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">DEUDOR</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="NombreDeudor"/>
								</fo:block>
							</fo:table-cell>	
						</fo:table-row>-->						
					</fo:table-body>
				</fo:table>
				
				<fo:block padding="2px" />
				
				<fo:table display-align="center">
					<!--<fo:table-column column-width="10%"/>
					<fo:table-column />-->
					
					<fo:table-body>						
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">MOTIVOS</fo:block>
							</fo:table-cell>
						</fo:table-row>	
						<fo:table-row>	
							<fo:table-cell padding="2px" text-align="left">
								<fo:block>
									<xsl:for-each select="lstMotivos/CabeceraMotivoCastigo">
										<fo:block>									
											<fo:external-graphic src="url('file:///C:/ArchivosSalida/check1.png')" padding-right="3px"/>
											<fo:inline padding-right="20px" padding-after="4px"><xsl:value-of select="Motivo"/></fo:inline>									
										</fo:block>
									</xsl:for-each>
								</fo:block>		
							</fo:table-cell>							
						</fo:table-row>
						
					</fo:table-body>
				</fo:table>
			</fo:block>
			
			<fo:block padding="5px" />
			
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="left">
				<xsl:text>Detalle Documentos:</xsl:text>
			</fo:block>
			
			<fo:block padding="5px" />
			
			<fo:block font-size="6.0pt" font-family="serif">
				<fo:table display-align="center">
				
					<fo:table-header>
						<fo:table-row>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold" >TIPO DE DOCTO.</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">Nº DOCTO.</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">FEC. EMISION</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">FECHA VENC.</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">MONEDA</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">MONTO</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">SALDO</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">NEGOCIO</fo:block>
							</fo:table-cell>
							<fo:table-cell border-after-style="solid" border-after-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">ASEGURADO</fo:block>
							</fo:table-cell>
							<!--<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">ANT.</fo:block>
							</fo:table-cell>-->						
							
						</fo:table-row>
					</fo:table-header>
					<fo:table-body>
						<xsl:choose>
							<xsl:when test="$filas != ''">
								<xsl:for-each select="lstDocs/DevolucionDocumentosDetalle">
									<fo:table-row>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="TipoDocumento"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="NroDocumento"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="FechaEmision"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="FechaVcto"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="Moneda"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="Monto"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="Saldo"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="Negocio"/>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
											<fo:block>
												<xsl:value-of select="Asegurado"/>
											</fo:block>
										</fo:table-cell>
										<!--<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
											<fo:block>
												<xsl:if test="DocOrigen = 'S'">
													<xsl:text>X</xsl:text>
												</xsl:if>	
											</fo:block>
										</fo:table-cell>
										<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
											<fo:block>
												<xsl:if test="DocCantidad = 'S'">
													<xsl:text>X</xsl:text>
												</xsl:if>
											</fo:block>
										</fo:table-cell>-->					
									</fo:table-row>
								</xsl:for-each>
							</xsl:when>
							<xsl:otherwise>
								<fo:table-row>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell border-after-style="dashed" border-after-width="thin" padding="2px" text-align="center">
										<fo:block/>
									</fo:table-cell>									
									<!--<fo:table-cell>
										<fo:block/>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block/>
									</fo:table-cell>-->					
								</fo:table-row>
							</xsl:otherwise>
						</xsl:choose> 
						<fo:table-row>
							<fo:table-cell border-before-style="solid" border-before-width="1pt" number-columns-spanned="3">
								<fo:block font-weight="bold">
									<xsl:value-of select="Totales/Cantidad"/> 
									   <xsl:choose>
										 <xsl:when test="Totales/Cantidad = 1">
											<xsl:text> DOCUMENTO POR DEUDOR</xsl:text>
										 </xsl:when>
										 <xsl:otherwise>
											<xsl:text> DOCUMENTOS POR DEUDOR</xsl:text>
										 </xsl:otherwise>
									   </xsl:choose>
								</fo:block>	
							</fo:table-cell>
							<!--<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>
							<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>-->
							<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>
							<!--<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>-->
							<fo:table-cell border-before-style="solid" border-before-width="1pt" padding="2px" text-align="right" number-columns-spanned="2">
								<fo:block font-weight="bold">TOTAL SALDO</fo:block>
							</fo:table-cell>
							<fo:table-cell border-before-style="solid" border-before-width="1pt" padding="2px" text-align="center">
								<fo:block font-weight="bold">
									<xsl:value-of select="Totales/Total"/>
								</fo:block>
							</fo:table-cell>		
							<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>
							<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>							
							<!--<fo:table-cell>
								<fo:block/>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block/>
							</fo:table-cell>-->							
						</fo:table-row>

					</fo:table-body>

				</fo:table>
				<!--<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								  text-align="left" space-after="3.0pt" space-before="4.0pt">
					<xsl:text>RECEPCIONADO POR: </xsl:text>
				</fo:block>
				<fo:table border="solid" border-width="1px" font-size="7.0pt">
					<fo:table-column column-width="8%"/>
					<fo:table-column />
					<fo:table-column column-width="15%"/>
					<fo:table-column column-width="15%"/>
					<fo:table-header>
						<fo:table-row>
							<fo:table-cell  padding="2px" text-align="left">
								<fo:block font-weight="bold" >RUT</fo:block>
							</fo:table-cell>
							<fo:table-cell  padding="2px" text-align="left">
								<fo:block font-weight="bold">NOMBRE </fo:block>
							</fo:table-cell>
							<fo:table-cell  padding="2px" text-align="left">
								<fo:block font-weight="bold">FECHA RECEPCION</fo:block>
							</fo:table-cell>
							<fo:table-cell  padding="2px" text-align="center">
								<fo:block font-weight="bold" >FIRMA</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-header>
					<fo:table-body>
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block  >
									<xsl:value-of select="/RecepcionDocumentos/RutUsuario"/>
								</fo:block>
							</fo:table-cell>
							<fo:table-cell  padding="2px" text-align="left">
								<fo:block >
									<xsl:value-of select="/RecepcionDocumentos/NombreUsuario"/>
								</fo:block>
							</fo:table-cell>
							<fo:table-cell  padding="2px" text-align="center">
								<fo:block >
									<xsl:value-of select="/RecepcionDocumentos/FechaRecepcion"/>
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block/>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
				</fo:table> 
				<fo:block >
					<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								  text-align="left" space-after="3.0pt" space-before="8.0pt">
						<xsl:text>ATTE. </xsl:text>
					</fo:block>
					<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt" font-weight="bold"
								  text-align="left" space-after="3.0pt" space-before="4.0pt">
						<xsl:text>DIMOL SpA</xsl:text>
					</fo:block>
				</fo:block> -->
			</fo:block>
			
			<fo:block padding="12px" />
		</xsl:for-each>	
			<fo:table display-align="center" keep-with-previous.within-page="always" page-break-inside="avoid">
				<fo:table-body>	
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
										  text-align="justify" space-after="3.0pt">
								<xsl:text>El Señor(a)........................................................................ RUT:................................................-...., declara haber recibido el(los) documento(s), a su entera satisfacción</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-size="7.0pt" font-family="sans-serif" padding="12px"
										  text-align="left" space-after="3.0pt">
								<xsl:text>Saluda Atentamente,</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
										  text-align="left" space-after="3.0pt">
								<xsl:text>Sara Mora M.</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
										  text-align="left" space-after="3.0pt">
								<xsl:text>Gerente General</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
										  text-align="left" space-after="3.0pt">
								<xsl:text>DIMOL SpA</xsl:text>
							</fo:block>
						</fo:table-cell>
					</fo:table-row>	
				</fo:table-body>
			</fo:table>
	</xsl:template>

</xsl:stylesheet>


