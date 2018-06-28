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
												<xsl:text>MUTUAL DE SEGURIDAD C.CH.C. </xsl:text>
											</fo:block>
											<fo:block>
												<xsl:text>R.U.T. : 70.285.100-9</xsl:text>
											</fo:block>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-left="27px">
											
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"  space-before="4.0pt" text-align="right">
											
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
						<xsl:with-param name="titulo" select="'LIQUIDACION DE DEUDA PREJUDICIAL'"/>
						<xsl:with-param name="cliente" select="''"/>
						<xsl:with-param name="rutCliente" select="''"/>
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
				<xsl:text></xsl:text>
				<xsl:value-of select="$rutCliente"/>
			</fo:block>
		</fo:block>
	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>

		<xsl:for-each select="$filas/LiquidacionDuraDeudor">
			<fo:block>
				<xsl:for-each select="$filas/LiquidacionDuraDeudor/lstAsegurados/LiquidacionDuraAsegurado">
				<xsl:if test="lstDetallesPesos != ''">
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt">
					<xsl:if test="RutSubCartera != ''">
						<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  color="brown" white-space-collapse="false">
							<xsl:text></xsl:text>
						</fo:block>
						<fo:table font-size="7.0pt">
						<fo:table-column column-width="15%"/>
						<fo:table-column column-width="1%"/>
						<fo:table-column column-width="45%"/>
						<fo:table-column column-width="15%"/>
						<fo:table-column column-width="10%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>N° CONTRATO</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>:</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:value-of select="RutSubCartera"/><xsl:text>  -  </xsl:text><xsl:value-of select="SubCartera"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
									
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
							
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>RUT EMPRESA: </xsl:text>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>:</xsl:text>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/lstDocumentos/LiquidacionDuraDeudor/RutDeudorFormateado"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>FECHA LIQUIDACION </xsl:text>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>:</xsl:text>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/FechaEmision"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:text>FECHA CALCULO:</xsl:text>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
										<xsl:value-of select="/LiquidacionDura/FechaEmision"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>OFICINA DE COBRO</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>:</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:value-of select="IdDivision"/><xsl:text>  -  </xsl:text><xsl:value-of select="Division"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
									
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
							
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>TASA VIGENTE</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>:</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:value-of select="Tasa"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
									
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
							
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
							<fo:table-row>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>EMPRESA COBRANZA</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										<xsl:text>:</xsl:text> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell padding="0px" text-align="left">
									<fo:block>
										06- DIMOL
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
									
									</fo:block>
								</fo:table-cell>
								<fo:table-cell  padding="0px" text-align="left">
									<fo:block>
							
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
					</xsl:if>
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  white-space-collapse="false" >
						<xsl:text>   </xsl:text>
						<xsl:value-of select="TipoDocumento"/>
				</fo:block>
				<fo:block font-size="7.0pt" font-family="calibri" padding-after="2.0pt" font-weight="bold" font-style="italic"  white-space-collapse="false" >
						<xsl:text>   </xsl:text>
						<xsl:value-of select="TipoDocumento"/>
				</fo:block>
				<fo:block font-size="6.0pt" font-family="calibri" margin-top="10pt">
					<fo:table>
						<fo:table-column column-width="24%"/>
						<fo:table-column column-width="7%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-column column-width="6%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-column column-width="9%"/>
						<fo:table-header>
							<fo:table-row>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">ASIGNACION</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">CLASE</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" keep-together="" >FECHA</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >DIVISION</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold">MONTO</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >REAJUSTE</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >INTERESES</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >IMPUESTOS</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >GASTOS COBR.</fo:block>
								</fo:table-cell>
								<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
									<fo:block font-weight="bold" >TOTAL</fo:block>
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
													<xsl:value-of select="Numero"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="left">
												<fo:block>
													CL
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="FechaEmision"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="center">
												<fo:block>
													<xsl:value-of select="Negocio"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Saldo"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right">
												<fo:block>
													<xsl:value-of select="Honorarios"/>
												</fo:block>
											</fo:table-cell>
											
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="red">
												<fo:block>
													<xsl:value-of select="Intereses"/>
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border="solid" border-width="1px" padding="2px" text-align="right" color="blue">
												<fo:block>
													<xsl:value-of select="Impuesto"/>
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
									</fo:table-row>
								</xsl:otherwise>
							</xsl:choose> 
							<fo:table-row font-size="6.0pt">
								<fo:table-cell text-align="left" border-width="1px" padding="2px" number-columns-spanned="4">
									<fo:block  font-weight="bold" font-style="italic" color="blue">TOTAL    :</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Saldo"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" >
										<xsl:value-of select="TotalesPesos/Honorarios"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px" padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="red">
										<xsl:value-of select="TotalesPesos/Intereses"/>
									</fo:block>
								</fo:table-cell>
								<fo:table-cell text-align="right" border-width="1px"  padding-top="2px" padding-right="0px">
									<fo:block font-weight="bold" font-style="italic" color="blue">
										<xsl:value-of select="TotalesPesos/Impuesto"/>
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
				</xsl:for-each>
			</fo:block>
		</xsl:for-each>				
	</xsl:template>
</xsl:stylesheet>

