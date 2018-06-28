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
	<xsl:variable name="file" select="document('C:\Program Files (x86)\Stylus Studio X15 Release 2 XML Enterprise Suite\examples\resumen_gestiones.xml')"/>-->

	<xsl:template match="/">
		<fo:root>
			<fo:layout-master-set>
				<!-- Define the cover page. -->
				<fo:simple-page-master master-name="cover" page-height="11in"
                             page-width="8.5in" margin-top="0.3125in"
                             margin-bottom="0.3125in" margin-left="0.3125in"
                             margin-right="0.3125in">
					<fo:region-body margin-top="0.3125in" margin-bottom="0.3125in"/>
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
								<fo:table-column column-width="50%"/>
								<fo:table-column column-width="50%"/>
								<fo:table-body>
									<fo:table-row>
										<fo:table-cell>
											<fo:block margin-left="1px" text-align="left">
												<fo:external-graphic src="url('file:///D:/Archivos/Reportes/XSL/indice.png')" content-height="scale-to-fit" width="70px" scaling="uniform" />
											</fo:block>
										</fo:table-cell>
										<fo:table-cell>
											
											<fo:block font-size="10.0pt" font-family="serif" padding-after="2.5pt" text-align="left">
													
												<xsl:text>Santiago, viernes, 30 de junio de 2017 </xsl:text>
											</fo:block>
										</fo:table-cell>
									</fo:table-row>
								</fo:table-body>
							</fo:table>
					</fo:block>
				</fo:static-content>

				<!-- Define the contents of the footer. -->
				<!--<fo:static-content flow-name="xsl-region-after">
					<fo:table>
						<fo:table-column column-width="50%"/>
						<fo:table-column column-width="50%"/>
						<fo:table-body>
							<fo:table-row>
								<fo:table-cell>
									<fo:block font-size="7.5pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="left">
			                  Pág.  <fo:page-number/> de <fo:page-number-citation ref-id="UltimaPagina"/> 
									</fo:block>
								</fo:table-cell>
								<fo:table-cell>
									<fo:block font-size="6.0pt" font-family="sans-serif" padding-after="2.0pt"
			                  space-before="4.0pt" text-align="right">
			                  FECHA EMISION: <xsl:value-of select="ResumenGestiones/FechaReporte"/>
									</fo:block>
								</fo:table-cell>
							</fo:table-row>
						</fo:table-body>
					</fo:table>
				</fo:static-content>-->

				<!-- The main contents of the body page, that is, the catalog
           entries -->
				<fo:flow flow-name="xsl-region-body">
					<!--<xsl:call-template name="title">
						<xsl:with-param name="titulo" select="'RESUMEN DE GESTIONES'"/>
						<xsl:with-param name="cliente" select="ResumenGestiones/Titulo/Cliente"/>
						<xsl:with-param name="rutDeudor" select="ResumenGestiones/Titulo/RutDeudor"/>
						<xsl:with-param name="nombreDeudor" select="ResumenGestiones/Titulo/Deudor"/>
					</xsl:call-template>-->
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/MutualManualCliente/lstDetalle"/>
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

		<fo:block font-size="12.5pt" font-family="sans-serif" padding-after="2.5pt"
			                  space-before="5.0pt" text-align="center">
			<fo:block>
				<xsl:value-of select="$titulo"/>
			</fo:block>
			<fo:block>
				<xsl:value-of select="$cliente"/>
			</fo:block>
			<fo:block>
				<xsl:text>DEUDOR RUT: </xsl:text>
				<xsl:value-of select="$rutDeudor"/>
				<xsl:text>&#160;</xsl:text>
				<xsl:value-of select="$nombreDeudor"/>
			</fo:block>


		</fo:block>
	</xsl:template>



	<xsl:template name="tabla">
		<xsl:param name="filas"/>
		<fo:block font-size="6.25pt" font-family="calibri" padding-before="6px">
			<fo:table display-align="center">
				<fo:table-column column-width="30%"/>
				<fo:table-column column-width="10%"/>
				<fo:table-column column-width="10%"/>
				<fo:table-column column-width="20%"/>
				<fo:table-column column-width="20%"/>
				<fo:table-column column-width="10%"/>
				<fo:table-header>
					<fo:table-row>
						<fo:table-cell number-columns-spanned="2" padding="1px">
							<fo:block>Departamento de Cobranza Privados</fo:block>
						</fo:table-cell>
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell number-columns-spanned="2" padding="1px">
							<fo:block>Mutual de Seguridad C.ch.c.</fo:block>
						</fo:table-cell>
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell padding="1px">
							<fo:block>RUT </fo:block>
						</fo:table-cell>
						<fo:table-cell number-columns-spanned="5" padding="1px">
							<fo:block font-weight="bold"><xsl:value-of select="/MutualManualCliente/Rut"/></fo:block>
						</fo:table-cell>
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell padding="1px">
							<fo:block>NOMBRE </fo:block>
						</fo:table-cell>
						<fo:table-cell number-columns-spanned="5" padding="1px">
							<fo:block><xsl:value-of select="/MutualManualCliente/Nombre"/></fo:block>
						</fo:table-cell>
					</fo:table-row>
					<fo:table-row>
						<fo:table-cell background-color="#A9A9A9" padding="2px" text-align="left">
							<fo:block font-weight="bold">Nombre</fo:block>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" padding="2px" text-align="center">
							<fo:block font-weight="bold">Factura</fo:block>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" padding="2px" text-align="center">
							<fo:block font-weight="bold">Fecha de Factura</fo:block>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" padding="2px" text-align="center">
							<fo:block font-weight="bold">Prestación</fo:block>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" padding="2px" text-align="center">
							<fo:block font-weight="bold">Agencia</fo:block>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" padding="2px" text-align="center">
							<fo:block font-weight="bold">Monto</fo:block>
						</fo:table-cell>
					</fo:table-row>
				</fo:table-header>
				<fo:table-body>
					<xsl:choose>
						<xsl:when test="$filas != ''">
							<xsl:for-each select="$filas/MutualManualDetalle">
								<fo:table-row>
									<fo:table-cell border-after-width="0.625pt" padding="2px"  text-align="left">
										<fo:block margin-bottom="3px">
											<xsl:value-of select="/MutualManualCliente/Nombre"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border-after-width="0.625pt" padding="2px"  text-align="center">
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Factura"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border-after-width="0.625pt" padding="2px"  text-align="center">
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Fecha"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border-after-width="0.625pt" padding="2px"  text-align="center">
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Prestacion"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border-after-width="0.625pt" padding="2px"  text-align="center">
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Agencia"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell border-after-width="0.625pt" padding="2px"  text-align="center">
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Monto"/>
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
					<fo:table-row>
						<fo:table-cell background-color="#A9A9A9"  padding="2px" text-align="left">
							<fo:block font-weight="bold">Total General</fo:block>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" >
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" >
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" >
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" >
							<fo:block/>
						</fo:table-cell>
						<fo:table-cell background-color="#A9A9A9" border-after-width="0.625pt" padding="2px" text-align="center">
							<fo:block>
								<xsl:value-of select="/MutualManualCliente/Totales/Total"/>
							</fo:block>
						</fo:table-cell>
					</fo:table-row>
						
				</fo:table-body>

			</fo:table>
			
			
			<fo:block margin-top="35px" font-weight="bold" text-align="right">Atte.</fo:block>
			<fo:block font-weight="bold" text-align="right">Mutual de Seguridad</fo:block>
			
		</fo:block>
	</xsl:template>

</xsl:stylesheet>


