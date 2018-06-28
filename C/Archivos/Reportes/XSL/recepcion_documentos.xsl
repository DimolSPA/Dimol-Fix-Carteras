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
					<fo:region-body column-count="1" margin-top="0.5in" margin-bottom="0.25in"/>

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
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Giro"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="27px">
											<fo:external-graphic src="url('file:///C:/Archivos/Reportes/XSL/logo-reporte.svg')" width="200px" height="80px" />
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											<fo:block/>
											<fo:block>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Telefono"/>
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
			                  FECHA EMISION: <xsl:value-of select="/RecepcionDocumentos/FechaReporte"/>
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
							<fo:table-column column-width="30%"/>
							<fo:table-column column-width="40%"/>
							<fo:table-column column-width="30%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Giro"/>
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
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/RecepcionDocumentos/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
						
					<xsl:call-template name="title">			
						<xsl:with-param name="titulo" select="'INGRESO DE DOCUMENTOS'"/>
						<xsl:with-param name="cliente" select="/RecepcionDocumentos/Titulo/Cliente"/>
					</xsl:call-template>
					<fo:block/>					
					
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/RecepcionDocumentos/lstDocsPorIngr"/>
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
			                   text-align="left">
				<xsl:text>&#160;</xsl:text>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="left">
				<xsl:text>&#160;</xsl:text>
			</fo:block>

			<!--<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                   text-align="left">
				<xsl:text>Señores: </xsl:text>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                 text-align="left" text-decoration="underline" font-weight="bold">
				<xsl:value-of select="$cliente"/>
			</fo:block>
			<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  text-align="left" space-after="3.0pt">
				<xsl:text>Hemos recibido por parte de vuestra empresa los siguientes documentos: </xsl:text>
			</fo:block>-->


		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>
		<fo:block font-size="4.0pt" font-family="calibri">
				
			<xsl:for-each select="$filas/RecepcionDocumentosPorIngreso">	
					
				<fo:block font-size="8.0pt" font-weight="bold" padding="10px">
					<xsl:text>Ingresado el </xsl:text><xsl:value-of select="FechaIngreso"/>
				</fo:block>
								
				<fo:table display-align="center">
					<fo:table-column column-width="8%"/>
					<fo:table-column />
					<fo:table-column column-width="6%"/>
					<fo:table-column column-width="6%"/>
					<fo:table-column column-width="6%"/>
					<fo:table-column column-width="8%"/>
					<fo:table-column column-width="8%"/>
					<fo:table-column column-width="8%"/>
					<fo:table-column />
					<fo:table-column column-width="8%"/>
					<fo:table-column />
					<fo:table-column column-width="6%"/>
					<fo:table-header>
						
						<fo:table-row>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold" >RUT</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">TIPO</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">NUMERO</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold" >FEC. EMISION</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">FEC. VENC.</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">MONEDA</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold" >MONTO</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">ASIGNADO</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">MOTIVO</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold" >RUT ASEG.</fo:block>
							</fo:table-cell>						
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold" >ASEGURADO</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">ORIGINAL</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-header>
					<fo:table-body>
						<xsl:choose>
							<xsl:when test="$filas != ''">
								<xsl:for-each select="lstDocumentos/RecepcionDocumentosDetalle">
								
										<xsl:variable name="decoration">
											<xsl:choose>
												<xsl:when test="Diferencia != 0">line-through</xsl:when>
												<xsl:otherwise></xsl:otherwise>
											</xsl:choose>
										</xsl:variable>
									
										<fo:table-row>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="RutDeudorFormateado"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="TipoDocumento"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="Numero"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="FechaDocumento"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="FechaVencimiento"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="Moneda"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="Monto"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="Asignado"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="MotivoCobranza"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="RutSubCartera"/></fo:inline>
												</fo:block>
											</fo:table-cell>									
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="SubCartera"/></fo:inline>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<fo:inline text-decoration="{$decoration}"><xsl:value-of select="DocumentoOriginal"/></fo:inline>
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
							<fo:table-cell>
								<fo:block/>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block/>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block/>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="center" number-columns-spanned="2">
								<fo:block font-weight="bold">TOTAL EN PESOS:</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">
									<xsl:value-of select="Totales/Total"/>
								</fo:block>
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

					</fo:table-body>

				</fo:table>
				
			</xsl:for-each>		
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
					<xsl:text>DIMOL LTDA.</xsl:text>
				</fo:block>
			</fo:block> -->
		</fo:block>
	</xsl:template>

</xsl:stylesheet>


