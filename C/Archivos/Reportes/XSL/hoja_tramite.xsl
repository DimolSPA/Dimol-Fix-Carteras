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
												<xsl:value-of select="/HojaTramite/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/HojaTramite/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/HojaTramite/Encabezado/Giro"/>
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
												<xsl:value-of select="/HojaTramite/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/HojaTramite/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/HojaTramite/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/HojaTramite/Encabezado/Telefono"/>
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
			                  FECHA EMISION: <xsl:value-of select="/HojaTramite/FechaReporte"/>
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
						<xsl:with-param name="titulo" select="'HOJA DE TRAMITE'"/>
						<xsl:with-param name="cliente" select="/HojaTramite/Titulo/Cliente"/>
						<xsl:with-param name="rutCliente" select="/HojaTramite/FechaLarga"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="listaDeudores">
						<xsl:with-param name="deudor" select="/HojaTramite/lstHojaCliente"/>
					</xsl:call-template>

					<fo:block id="UltimaPagina"/>
				</fo:flow>
			</fo:page-sequence>
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

	

	<xsl:template name="listaDeudores">
		<xsl:param name="deudor"/>
		<xsl:for-each select="$deudor/HojaTramiteCliente">
			
						<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt"
								  space-before="2.0pt" text-align="center" 
								  border-style="solid" border-width="1" border-color="#000" >

						<fo:table font-size="6.0pt" margin="1mm" keep-with-previous.within-page="always" page-break-inside="avoid">
							<fo:table-column column-width="50%"/>
							<fo:table-column column-width="50%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell padding="1px" text-align="left" number-columns-spanned="2">
										<fo:block>
											<xsl:text>DEUDOR: </xsl:text><xsl:value-of select="RutDeudor"/><xsl:text> </xsl:text><xsl:value-of select="NombreDeudor"/>
										</fo:block>
									</fo:table-cell>									
								</fo:table-row>
								<fo:table-row>
									<fo:table-cell  padding="1px" text-align="left" >
										<fo:block>
											<xsl:text>CAUSA: </xsl:text><xsl:value-of select="Causa"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell  padding="1px" text-align="left">
										<fo:block>
											<xsl:text>MONTO: </xsl:text><xsl:value-of select="Monto"/>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
								<fo:table-row>
									<fo:table-cell padding="1px" text-align="left">
										<fo:block>
											<xsl:text>JUZGADO: </xsl:text><xsl:value-of select="Juzgado"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell  padding="1px" text-align="left">
										<fo:block>
											<xsl:text>DIRECCION: </xsl:text><xsl:value-of select="Direccion"/>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
								<fo:table-row>
									<fo:table-cell padding="1px" text-align="left">
										<fo:block>
											<xsl:text>ROL: </xsl:text><xsl:value-of select="Rol"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell  padding="1px" text-align="left">
										<fo:block>
											<xsl:text>CIUDAD: </xsl:text><xsl:value-of select="Ciudad"/>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
								<xsl:if test="lstAsegurados/HojaTramiteAsegurado != ''">
									<xsl:for-each select="lstAsegurados/HojaTramiteAsegurado">
										<fo:table-row>
											<fo:table-cell padding="1px" text-align="left" number-columns-spanned="2">
												<fo:block>
													<fo:table>
														<fo:table-column column-width="13%"/>
														<fo:table-column column-width="87%"/>
														<fo:table-body>
															<fo:table-row>
																<fo:table-cell padding-left="-3px">
																	<fo:block>
																		<xsl:if test="position()=1">
																			<xsl:text>ASEGURADO(S): </xsl:text>
																		</xsl:if>
																	</fo:block>
																</fo:table-cell>
																<fo:table-cell>
																	<fo:block>	
																		<xsl:value-of select="Nombre"/>
																	</fo:block>
																</fo:table-cell>	
															</fo:table-row>		
														</fo:table-body>
													</fo:table>	
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
									</xsl:for-each>
								</xsl:if>
							</fo:table-body>
						</fo:table>
					</fo:block>
				<xsl:call-template name="listaTramites">
					<xsl:with-param name="detalle" select="lstHojaDetalle"/>
				</xsl:call-template>
				
			
		</xsl:for-each>			
	
	</xsl:template>
	
	
	<xsl:template name="listaTramites">
		<xsl:param name="detalle"/>
		<xsl:if test=". != ''">
		
				<fo:block padding="2mm" />						
				
				<fo:block font-size="6.0pt" font-family="calibri" padding-after="20px">
				
					
					<fo:table display-align="center">
						<fo:table-column column-width="8%"/>
						<fo:table-column />
						<fo:table-column />
						<fo:table-column column-width="50%"/>
						
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border-after-style="solid" padding="2px" text-align="center">
									<fo:block font-weight="bold">FECHA</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="solid" padding="2px" text-align="center">
									<fo:block font-weight="bold">MATERIA</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="solid" padding="2px" text-align="center">
									<fo:block font-weight="bold">ESTADO</fo:block>
								</fo:table-cell>
								<fo:table-cell border-after-style="solid" padding="2px" text-align="center">
									<fo:block font-weight="bold" >COMENTARIO</fo:block>
								</fo:table-cell>								
							</fo:table-row>
						</fo:table-header>
						<fo:table-body>
							<xsl:choose>
								<xsl:when test="$detalle/HojaTramiteDetalle != ''">
									<xsl:for-each select="$detalle/HojaTramiteDetalle">
										<fo:table-row font-size="5.0pt">
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px"  text-align="center">
												<fo:block>
													<xsl:value-of select="FechaJudicial"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px"  text-align="left">
												<fo:block>
													<xsl:value-of select="Materia"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px"  text-align="left">
												<fo:block>
													<xsl:value-of select="Estado"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-after-style="dashed" border-after-width="0.5pt" padding="2px"  text-align="justify">
												<fo:block>
													<xsl:value-of select="Comentario"/>
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
										
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							
						</fo:table-body>
					</fo:table>
				
				</fo:block>
				</xsl:if>
	</xsl:template>

	
</xsl:stylesheet>