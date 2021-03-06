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
											<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"
                  space-before="4.0pt" text-align="left" width="200px" >
												<fo:block>
													<xsl:value-of select="ResumenGestiones/Encabezado/Nombre"/>
												</fo:block>
												<fo:block>
													<xsl:text>R.U.T. : </xsl:text>
													<xsl:value-of select="ResumenGestiones/Encabezado/Rut"/>
												</fo:block>
												<fo:block>
													<xsl:text>GIRO: </xsl:text>
													<xsl:value-of select="ResumenGestiones/Encabezado/Giro"/>
												</fo:block>
											</fo:block>
										</fo:table-cell>
										<fo:table-cell>
											<fo:block margin-left="27px">
												<fo:external-graphic src="url('file:///D:/Dimol/logo-reporte.svg')" width="200px" height="80px" />
											</fo:block>
										</fo:table-cell>
										<fo:table-cell>
											<fo:block font-size="7.0pt" font-family="serif" padding-after="2.0pt"
                  space-before="4.0pt" text-align="right">
												<fo:block/>
												<fo:block>
													<xsl:value-of select="ResumenGestiones/Encabezado/Comuna"/>
													<xsl:text>, </xsl:text>
													<xsl:value-of select="ResumenGestiones/Encabezado/Region"/>
													<xsl:text>, </xsl:text>
													<xsl:value-of select="ResumenGestiones/Encabezado/Pais"/>
												</fo:block>
												<fo:block>
													<xsl:text>TELÉFONO: </xsl:text>
													<xsl:value-of select="ResumenGestiones/Encabezado/Telefono"/>
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
									<fo:block font-size="6.0pt" font-family="sans-serif" padding-after="2.0pt"
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
				</fo:static-content>

				<!-- The main contents of the body page, that is, the catalog
           entries -->
				<fo:flow flow-name="xsl-region-body">
					<xsl:call-template name="title">
						<xsl:with-param name="titulo" select="'RESUMEN DE GESTIONES'"/>
						<xsl:with-param name="cliente" select="ResumenGestiones/Titulo/Cliente"/>
						<xsl:with-param name="rutDeudor" select="ResumenGestiones/Titulo/RutDeudor"/>
						<xsl:with-param name="nombreDeudor" select="ResumenGestiones/Titulo/Deudor"/>
					</xsl:call-template>
					<fo:block/>
					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="ResumenGestiones/lstDocumentos"/>
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
		<fo:block font-size="7.0pt" font-family="serif">
			<fo:table>
				<fo:table-column column-width="60px"/>
				<fo:table-column column-width="430px"/>
				<fo:table-column column-width="200px"/>
				<fo:table-header>
					<fo:table-row>
						<fo:table-cell>
							<fo:block font-weight="bold" >FECHA</fo:block>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block font-weight="bold">COMENTARIO</fo:block>
						</fo:table-cell>
						<fo:table-cell>
							<fo:block font-weight="bold">USUARIO</fo:block>
						</fo:table-cell>
					</fo:table-row>
				</fo:table-header>
				<fo:table-body>
					<xsl:choose>
						<xsl:when test="$filas != ''">
							<xsl:for-each select="$filas/ResumenGestionesDetalle">
								<fo:table-row>
									<fo:table-cell>
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Fecha"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Comentario"/>
										</fo:block>
									</fo:table-cell>
									<fo:table-cell>
										<fo:block margin-bottom="3px">
											<xsl:value-of select="Usuario"/>
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
					</fo:table-row>
				</fo:table-body>

			</fo:table>
		</fo:block>
	</xsl:template>

</xsl:stylesheet>


