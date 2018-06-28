<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html lang="en">
			<head>
				<title/>
			</head>
			<body>
				<table style="font-family:'Times New Roman', Times, serif;font-size:14px;text-align:left" cellspacing="3">
					<xsl:call-template name="title">
						<xsl:with-param name="titulo" select="'ESTADO DE CUENTA'"/>
						<xsl:with-param name="cliente" select="/Liquidacion/Titulo/Cliente"/>
						<xsl:with-param name="rutCliente" select="/Liquidacion/Titulo/RutCliente"/>
					</xsl:call-template>

					<xsl:call-template name="tabla">
						<xsl:with-param name="filas" select="/Liquidacion/lstDocumentos"/>
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
			<td colspan="12" style ="font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold">
				<xsl:value-of select="$titulo"/>
			</td>
		</tr>
		<tr>
			<td colspan="12" style="font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold">
				<xsl:value-of select="$cliente"/>
			</td>
		</tr>
		<tr>
			<td colspan="12" style="font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold">
                RUT: <xsl:value-of select="$rutCliente"/>
			</td>

		</tr>
		<tr>
			<td colspan ="12" style="font-weight:bold;height:25px"/>
		</tr>

	</xsl:template>

	<xsl:template name="tabla">
		<xsl:param name="filas"/>

		<xsl:for-each select="$filas/LiquidacionDeudor">
			<tr style="font-weight:bold">
				<td style ="width:20px"/>
				<td>RUT: </td>
				<td>
					<xsl:value-of select="RutDeudorFormateado"/>
				</td>
				<td colspan="2">NOMBRE: </td>
				<td>
					<xsl:value-of select="NombreFantasia"/>
				</td>
				<td/>
				<td/>
				<td/>
				<td/>
				<td/>
			</tr>
			<tr>
				<td style="width:20px"/>
				<td>CIUDAD:</td>
				<td>
					<xsl:value-of select="Ciudad"/>
				</td>
				<td style="width:20px"/>
				<td>COMUNA:</td>
				<td>
					<xsl:value-of select="Comuna"/>
				</td>
				<td style ="width:20px"/>
				<td>REGION:</td>
				<td colspan="2">
					<xsl:value-of select="Region"/>
				</td>
				<td/>
				<td/>
			</tr>
			<tr>
				<td style="width:20px"/>
				<td>DIRECCION:</td>
				<td>
					<xsl:value-of select="Direccion"/>
				</td>
				<td/>
				<td>TELEFONO:</td>
				<td>
					<xsl:value-of select="Telefono"/>
				</td>
				<td/>
				<td>COD. POSTAL:</td>
				<td colspan="2">
					<xsl:value-of select="CodigoPostal"/>
				</td>
				<td/>
				<td/>
			</tr>
			<tr>
				<td style="width:20px"/>
				<td>CELULAR:</td>
				<td>
					<xsl:value-of select="Celular"/>
				</td>
				<td/>
				<td>E-MAIL:</td>
				<td>
					<xsl:value-of select="Email"/>
				</td>
				<td/>
				<td>FAX:</td>
				<td colspan="2">
					<xsl:value-of select="Fax"/>
				</td>
				<td/>
				<td/>
			</tr>
			<tr>
				<td style="width:20px"/>
				<td/>
				<td/>
				<td/>
				<td style ="font-weight:bold;">FECHA EMISION:</td>
				<td style="font-weight:bold;">
					<xsl:value-of select="/Liquidacion/FechaEmision"/>
				</td>
				<td/>
				<td style="font-weight:bold;">GESTOR:</td>
				<td style="font-weight:bold;" colspan="2">
					<xsl:value-of select="Gestor"/>
				</td>
				<td/>
				<td/>
			</tr>
			<tr>
				<td colspan="12" style="font-weight:bold;height:25px"/>
			</tr>
			<tr>
				<td style="width:20px"/>
				<td colspan="11" style="font-weight:bold;color:darkblue;text-align:center">DETALLE DE CUENTA</td>
			</tr>
			<xsl:if test="TotalesPesos/CantidadDocumentos > 0">
				<tr>
					<td colspan="2" style="font-weight:bold;color:darkblue;text-align:right">PESOS</td>
					<td/>
					<td/>
					<td> </td>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
				</tr>
				<tr style="font-weight:bold;text-align:center">
					<td style="width:20px"/>
					<td/>
					<td>TIPO</td>
					<td colspan="2">NUMERO</td>
					<td>FECHA EMISION</td>
					<td colspan="2">FECHA VENC.</td>
					<td>DIAS  VENC.</td>
					<td>MONTO</td>
					<td>SALDO</td>
					<td>NEGOCIO</td>
					<td/>
				</tr>

				<xsl:choose>
					<xsl:when test="$filas/LiquidacionDeudor != ''">
						<xsl:for-each select="lstDetallesPesos/LiquidacionDetalle">
							<tr>
								<td colspan="2"/>
								<td>
									<xsl:value-of select="TipoDocumento"/>
								</td>
								<td style="text-align:right" colspan="2">
									<xsl:value-of select="Numero"/>
								</td>
								<td style="text-align:center">
									<xsl:value-of select="FechaEmision"/>
								</td>
								<td style="text-align:center" colspan="2">
									<xsl:value-of select="FechaVencimiento"/>
								</td>
								<td style="color:red;text-align:right">
									<xsl:value-of select="DiasVencido"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Monto"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Saldo"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Negocio"/>
								</td>
								<td/>
							</tr>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
					</xsl:otherwise>
				</xsl:choose> 
				<tr style="font-weight:bold;color:darkblue">
					<td colspan="2"/>
					<td/>
					<td/>
					<td/>
					<td colspan="3">SUB-TOTAL PESOS</td>
					<td/>
					<td style="text-align:right">
						<xsl:value-of select="TotalesPesos/Monto"/>
					</td>
					<td style="text-align:right">
						<xsl:value-of select="TotalesPesos/Saldo"/>
					</td>
					<td/>
					<td/>
				</tr>
				<tr style="font-weight:bold;color:darkblue">
					<td colspan="2"/>
					<td/>
					<td/>
					<td/>
					<td colspan="3">CANTIDAD DOCUMENTOS</td>
					<td/>
					<td style="text-align:right">
						<xsl:value-of select="TotalesPesos/CantidadDocumentos"/>
					</td>
					<td/>
					<td/>
					<td/>
				</tr>
			</xsl:if>
			<xsl:if test="TotalesDolar/CantidadDocumentos > 0">
				<tr>
					<td colspan="2" style="font-weight:bold;color:darkblue;text-align:right">DOLAR</td>
					<td/>
					<td/>
					<td> </td>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
				</tr>
				<tr style="font-weight:bold;text-align:center">
					<td style="width:20px"/>
					<td/>
					<td>TIPO</td>
					<td colspan="2">NUMERO</td>
					<td>FECHA EMISION</td>
					<td colspan="2">FECHA VENC.</td>
					<td>DIAS  VENC.</td>
					<td>MONTO</td>
					<td>SALDO</td>
					<td>NEGOCIO</td>
					<td/>
				</tr>

				<xsl:choose>
					<xsl:when test="$filas/LiquidacionDeudor != ''">
						<xsl:for-each select="lstDetallesDolares/LiquidacionDetalle">
							<tr>
								<td colspan="2"/>
								<td>
									<xsl:value-of select="TipoDocumento"/>
								</td>
								<td style="text-align:right" colspan="2">
									<xsl:value-of select="Numero"/>
								</td>
								<td style="text-align:center">
									<xsl:value-of select="FechaEmision"/>
								</td>
								<td style="text-align:center" colspan="2">
									<xsl:value-of select="FechaVencimiento"/>
								</td>
								<td style="color:red;text-align:right">
									<xsl:value-of select="DiasVencido"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Monto"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Saldo"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Negocio"/>
								</td>
								<td/>
							</tr>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
					</xsl:otherwise>
				</xsl:choose> 
				<tr style="font-weight:bold;color:darkblue">
					<td colspan="2"/>
					<td/>
					<td/>
					<td/>
					<td colspan="3">SUB-TOTAL DOLARES</td>
					<td/>
					<td style="text-align:right">
						<xsl:value-of select="TotalesDolar/Monto"/>
					</td>
					<td style="text-align:right">
						<xsl:value-of select="TotalesDolar/Saldo"/>
					</td>
					<td/>
					<td/>
				</tr>
				<tr style="font-weight:bold;color:darkblue">
					<td colspan="2"/>
					<td/>
					<td/>
					<td/>
					<td colspan="3">CANTIDAD DOCUMENTOS</td>
					<td/>
					<td style="text-align:right">
						<xsl:value-of select="TotalesDolar/CantidadDocumentos"/>
					</td>
					<td/>
					<td/>
					<td/>
				</tr>
			</xsl:if>
			<xsl:if test="TotalesUF/CantidadDocumentos > 0">
				<tr>
					<td colspan="2" style="font-weight:bold;color:darkblue;text-align:right">UF</td>
					<td/>
					<td/>
					<td> </td>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
					<td/>
				</tr>
				<tr style="font-weight:bold;text-align:center">
					<td style="width:20px"/>
					<td/>
					<td>TIPO</td>
					<td colspan="2">NUMERO</td>
					<td>FECHA EMISION</td>
					<td colspan="2">FECHA VENC.</td>
					<td>DIAS  VENC.</td>
					<td>MONTO</td>
					<td>SALDO</td>
					<td>NEGOCIO</td>
					<td/>
				</tr>

				<xsl:choose>
					<xsl:when test="$filas/LiquidacionDeudor != ''">
						<xsl:for-each select="lstDetallesUF/LiquidacionDetalle">
							<tr>
								<td colspan="2"/>
								<td>
									<xsl:value-of select="TipoDocumento"/>
								</td>
								<td style="text-align:right" colspan="2">
									<xsl:value-of select="Numero"/>
								</td>
								<td style="text-align:center">
									<xsl:value-of select="FechaEmision"/>
								</td>
								<td style="text-align:center" colspan="2">
									<xsl:value-of select="FechaVencimiento"/>
								</td>
								<td style="color:red;text-align:right">
									<xsl:value-of select="DiasVencido"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Monto"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Saldo"/>
								</td>
								<td style="text-align:right">
									<xsl:value-of select="Negocio"/>
								</td>
								<td/>
							</tr>
						</xsl:for-each>
					</xsl:when>
					<xsl:otherwise>
					</xsl:otherwise>
				</xsl:choose> 
				<tr style="font-weight:bold;color:darkblue">
					<td colspan="2"/>
					<td/>
					<td/>
					<td/>
					<td colspan="3">SUB-TOTAL UF</td>
					<td/>
					<td style="text-align:right">
						<xsl:value-of select="TotalesUF/Monto"/>
					</td>
					<td style="text-align:right">
						<xsl:value-of select="TotalesUF/Saldo"/>
					</td>
					<td/>
					<td/>
				</tr>
				<tr style="font-weight:bold;color:darkblue">
					<td colspan="2"/>
					<td/>
					<td/>
					<td/>
					<td colspan="3">CANTIDAD DOCUMENTOS</td>
					<td/>
					<td style="text-align:right">
						<xsl:value-of select="TotalesUF/CantidadDocumentos"/>
					</td>
					<td/>
					<td/>
					<td/>
				</tr>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>