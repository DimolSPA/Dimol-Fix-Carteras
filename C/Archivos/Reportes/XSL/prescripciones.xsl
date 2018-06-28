<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html lang="en">
			<head>
				<meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"/>
				<title/>
			</head>
			<body>
				<table cellspacing="3">
					<!--<xsl:call-template name="title">
						<xsl:with-param name="titulo" select="'DATA ARBOL JUDICIAL'"/>
						<xsl:with-param name="cliente" select="/ArbolJudicial/Titulo/Cliente"/>
						<xsl:with-param name="rutCliente" select="/ArbolJudicial/Titulo/RutCliente"/>
					</xsl:call-template>-->
					<xsl:call-template name="tablaArbol">
						<xsl:with-param name="rows" select="/Prescripciones/lstPrescr"/>
					</xsl:call-template>
				</table>
			</body>
		</html>
	</xsl:template>

	<xsl:template name="title">
		<xsl:param name="titulo"/>
		<xsl:param name="cliente"/>
		<xsl:param name="rutCliente"/>
		<tr>
			<td colspan="7" style ="font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold">
				<xsl:value-of select="$titulo"/>
			</td>
		</tr>
		<tr>
			<td colspan="7" style="font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold">
				<xsl:value-of select="$cliente"/>
			</td>
		</tr>
		<tr>
			<td colspan="7" style="font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold">
                RUT: <xsl:value-of select="$rutCliente"/>
			</td>

		</tr>
		<tr>
			<td colspan ="7" style="font-weight:bold;height:25px"/>
		</tr>

	</xsl:template>
	
	<xsl:template name="tablaArbol">
		<xsl:param name="rows"/>
				
				<xsl:if test="1 > 0">				
				
					<tr style="font-weight:bold;vertical-align:center">
							<td style="width:20px"/>
							<td style="text-align:center">RUT CLIENTE</td>
							<td style="text-align:center">NOMBRE CLIENTE</td>	
							<td style="text-align:center">RUT DEUDOR</td>
							<td style="text-align:center">NOMBRE DEUDOR</td>
							<td>ROL</td>
							<td>TRIBUNAL</td>
							<td>TIPO CAUSA</td>
							<td>FECHA JUDICIAL</td>
							<td>MATERIA JUDICIAL</td>
							<td>ESTADO ROL</td>
							<td>NUMERO</td>	
							<td>FECHA VENC</td>		
							<td>FECHA PRESC</td>								
							<td style="text-align:center">DEMANDADO</td>
							<td>SALDO</td>
							<td>CODIGO CARGA</td>
							<td>ABOGADO</td>
							<td>ASEGURADO</td>
					</tr>
					
					<xsl:for-each select="$rows/PrescripcionesBruto">
						<tr>
							<td style="width:20px"/>
							<td>
								<xsl:value-of select="RutCliente"/>
							</td>
							<td style="text-align:left">
								<xsl:value-of select="NombreCliente"/>
							</td>
							<td>
								<xsl:value-of select="RutDeudor"/>
							</td>
							<td style="text-align:left">
								<xsl:value-of select="NombreDeudor"/>
							</td>
							<td>
								<xsl:value-of select="Rol"/>							
							</td>
							<td>
								<xsl:value-of select="Tribunal"/>
							</td>
							<td>
								<xsl:value-of select="TipoCausa"/>	
							</td>
							<td>
								<xsl:value-of select="FechaJudicial"/>	
							</td>
							<td>
								<xsl:value-of select="MateriaJudicial"/>	
							</td>
							<td>
								<xsl:value-of select="EstadoRol"/>	
							</td>
							<td>
								<xsl:value-of select="Numero"/>	
							</td>
							<td>
								<xsl:value-of select="FechaVencimiento"/>	
							</td>
							<td>
								<xsl:value-of select="FechaPrescripcion"/>	
							</td>
							<td>
								<xsl:value-of select="Demandado"/>	
							</td>
							<td>
								<xsl:value-of select="Saldo"/>	
							</td>
							<td>
								<xsl:value-of select="CodigoCarga"/>	
							</td>
							<td>
								<xsl:value-of select="Abogado"/>	
							</td>
							<td>
								<xsl:value-of select="Asegurado"/>	
							</td>							
						</tr>
					</xsl:for-each>		
									
				
				</xsl:if>	
								
	</xsl:template>
</xsl:stylesheet>