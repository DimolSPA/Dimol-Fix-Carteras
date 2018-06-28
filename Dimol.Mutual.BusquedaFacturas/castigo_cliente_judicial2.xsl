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
				
				<!--<fo:static-content flow-name="xsl-region-after">
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
			                  FECHA EMISION: <xsl:value-of select="/CastigoJudicial2/FechaReporte"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:static-content>-->
				
				<fo:flow flow-name="xsl-region-body">												
												
						<xsl:call-template name="tabla">
							<xsl:with-param name="filas" select="/CastigoJudicial2/lstDocsDeudor"/>
						</xsl:call-template>										
						
						<fo:block id="UltimaPagina"/>
						
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>	

	<xsl:template name="tabla">
		<xsl:param name="filas"/>
		
		<xsl:for-each select="$filas/CastigoJudicialDeudor2">
			
						<fo:table>
							<fo:table-column column-width="30%"/>
							<fo:table-column column-width="40%"/>
							<fo:table-column column-width="30%"/>
							<fo:table-body>
								<fo:table-row>
									<fo:table-cell>
										<fo:block font-size="5.0pt" font-family="serif" padding-after="2.0pt" space-before="4.0pt" text-align="left" width="200px" inline-progression-dimension="50%">
											<fo:block>
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Nombre"/>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : </xsl:text>
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Rut"/>
											</fo:block>
											<fo:block>
												<xsl:text>GIRO: </xsl:text>
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Giro"/>
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
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Comuna"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Region"/>
												<xsl:text>, </xsl:text>
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Pais"/>
											</fo:block>
											<fo:block>
												<xsl:text>TELÉFONO: </xsl:text>
												<xsl:value-of select="/CastigoJudicial2/Encabezado/Telefono"/>
											</fo:block>
										</fo:block>
									</fo:table-cell>
								</fo:table-row>
							</fo:table-body>
						</fo:table>
			
			<fo:block font-size="8.0pt" font-family="sans-serif" padding-after="2.0pt" padding-before="10px"
			                  space-before="4.0pt" text-align="center">
				<fo:block  font-size="10.0pt" font-family="sans-serif" padding-after="2.0pt"
								  space-before="2.0pt" text-align="center" font-weight="bold">
					<xsl:value-of select="'CERTIFICADO DE INCOBRABILIDAD'"/>
				</fo:block>
				<fo:block font-size="9.0pt" font-family="sans-serif" padding-after="2.0pt"
								  space-before="2.0pt" text-align="center">
					<xsl:value-of select="/CastigoJudicial2/Titulo/Cliente"/>
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
									<xsl:text>Santiago, </xsl:text><xsl:value-of select="/CastigoJudicial2/FechaComprobante"/>
								</fo:block>
							</fo:table-cell>

							<fo:table-cell text-align="right">
								<fo:block font-weight="bold">
									<xsl:text>FOLIO: </xsl:text><xsl:value-of select="CbcNumProvCli"/><xsl:text>-</xsl:text><xsl:value-of select="1"/>
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
				</fo:table>
				
				<fo:block padding="4px" />	
				
				<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								   text-align="left" font-weight="bold">
					<xsl:text>Señor(es) </xsl:text>
				</fo:block>
							
				<fo:table display-align="center">
					<fo:table-column column-width="70%" />
					<fo:table-column />
					<fo:table-body>
						<fo:table-row>
							<fo:table-cell text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="NombreUsuario"/>
								</fo:block>
							</fo:table-cell>

							<fo:table-cell text-align="right">
								<fo:block font-weight="bold">
									<xsl:text>RUT: </xsl:text><xsl:value-of select="RutUsuario"/>
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
				</fo:table>
				
				<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								 text-align="left">
					<xsl:value-of select="lstSucursalCliente/Direccion"/>
				</fo:block>
				<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								 text-align="left">
					<xsl:value-of select="lstSucursalCliente/Comuna"/>
				</fo:block>
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								 text-align="left" text-decoration="underline">
					<xsl:value-of select="lstSucursalCliente/Ciudad"/>
				</fo:block>
				<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								  text-align="left" space-after="3.0pt">
					<xsl:text>De nuestra Consideración: </xsl:text>
				</fo:block>
				<fo:block padding="5px"/>
				<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
								  text-align="justify" space-after="3.0pt">
					<xsl:text>De acuerdo a las gestiones efectuadas tendientes a recuperar el crédito encomendado, y pese a haber agotado prudencialmente las instancias de cobranza judicial conforme los procedimientos establecidos y aceptados, recomendamos el castigo contable de los documentos correspondientes a: </xsl:text>
				</fo:block>				

			</fo:block>
			
			
			<fo:block/>
			
			
			<fo:block font-size="6.0pt" font-family="serif">
				<fo:table display-align="center">
					<fo:table-column column-width="25%"/>
					<fo:table-column />
					<fo:table-body>
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">DEUDOR</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="RutDeudor"/>
									<xsl:text>   </xsl:text>
									<xsl:value-of select="NombreDeudor"/>
								</fo:block>
							</fo:table-cell>	
						</fo:table-row>							
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">DIRECCION</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="Direccion"/>
								</fo:block>
							</fo:table-cell>	
						</fo:table-row>	
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">TRIBUNAL</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="Tribunal"/>
								</fo:block>
							</fo:table-cell>	
						</fo:table-row>	
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">ROL</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<xsl:value-of select="Rol"/>
								</fo:block>
							</fo:table-cell>	
						</fo:table-row>	
						
					</fo:table-body>
				</fo:table>
				
				<fo:table>
					<fo:table-column column-width="25%"/>
					<fo:table-column />
					<fo:table-body>
						<fo:table-row>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">ABOGADO PATROCINANTE</fo:block>
							</fo:table-cell>
							<fo:table-cell padding="2px" text-align="left">
								<fo:block font-weight="bold">
									<fo:inline padding-right="5px"><xsl:value-of select="/CastigoJudicial2/RutAbogado"/></fo:inline>
									<xsl:value-of select="/CastigoJudicial2/NombreAbogado"/>
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>	
				</fo:table>		
				
				<fo:block padding="5px" />
				
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
				<xsl:text>Detalle Deuda:</xsl:text>
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
							<!--<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">ORI.</fo:block>
							</fo:table-cell>
							<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
								<fo:block font-weight="bold">ANT.</fo:block>
							</fo:table-cell>-->						
							
						</fo:table-row>
					</fo:table-header>
					<fo:table-body>
						<xsl:choose>
							<xsl:when test="$filas != ''">
								<xsl:for-each select="lstDocs/CastigoJudicialDetalle2">
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
							
							<fo:table-cell border-before-style="solid" border-before-width="1pt" >
								<fo:block/>
							</fo:table-cell>
							
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
													
						</fo:table-row>

					</fo:table-body>

				</fo:table>
				
			</fo:block>
			
			<fo:block padding="12px" />
			
			<fo:table display-align="center" keep-with-previous.within-page="always" page-break-inside="avoid">
				<fo:table-body>	
					<fo:table-row>
						<fo:table-cell number-columns-spanned = "2">
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="4.0pt"
										  text-align="justify" space-after="3.0pt">
								<xsl:text>Esta certificación es enviada a usted para los efectos previstos en el art. 31 de la Ley de Rentas y Circular Nº 24 de 2008 del Servicio de Impuestos Internos.</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell number-columns-spanned = "2">
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="4.0pt"
										  text-align="justify" space-after="3.0pt">
								<xsl:text>Para efectos de lo dispuesto en la Resolución del SII Nº 96 del 22 de Agosto de 2008, declaramos bajo juramento que los datos contenidos en el presente instrumento representan el fiel reflejo de la realidad.</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell number-columns-spanned = "2">
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="4.0pt"
										  text-align="justify" space-after="3.0pt">
								<xsl:text>Se adjuntan copias de la demanda, estampe(s) y situación tributaria del SII.</xsl:text>
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
							<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt" text-align="left" space-after="3.0pt">
								<xsl:value-of select="/CastigoJudicial2/NombreAbogado"/>
							</fo:block>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt" text-align="right" space-after="3.0pt">
								P.P. <xsl:value-of select="/CastigoJudicial2/NombreUsuario"/>
							</fo:block>
						</fo:table-cell>						
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
										  text-align="left" space-after="3.0pt">
								<xsl:text>Abogado(a)</xsl:text>
							</fo:block>
						</fo:table-cell>	
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-weight="bold" font-size="7.0pt" font-family="sans-serif" padding-after="2.0pt"
										  text-align="left" space-after="3.0pt">
								<xsl:value-of select="/CastigoJudicial2/Empresa"/>
							</fo:block>
						</fo:table-cell>
					</fo:table-row>	
				</fo:table-body>
			</fo:table>
		
			<xsl:if test="position() != last()">
				<fo:block page-break-after="always"/>
			</xsl:if>	
			
		</xsl:for-each>	
	</xsl:template>

</xsl:stylesheet>