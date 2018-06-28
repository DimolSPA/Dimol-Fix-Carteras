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
						<xsl:with-param name="rows" select="/ArbolJudicial/lstCategoria"/>
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
									
					<!--<tr>
						<td></td>
						<td colspan="7" style="text-align:center">RESUMEN DE CARTERA</td>
					</tr>
				
					<tr></tr>
					<tr></tr>-->
						
					<tr>
						<!--<td></td>-->
						<td style="border: 1px solid black;">Cliente</td>
						<td style="border: 1px solid black;">Rut</td>
						<td style="border: 1px solid black;">Nombre Deudor</td>
						<td style="border: 1px solid black;">Asegurado</td>
						<td style="border: 1px solid black;">Rol</td>
						<td style="border: 1px solid black;">Tribunal</td>
						<td style="border: 1px solid black;">Tipo Causa</td>
						<td style="border: 1px solid black;">Materia Judicial</td>
						<td style="border: 1px solid black;">Estado Rol</td>
						<td style="border: 1px solid black;">Fecha Judicial</td>
						<td style="border: 1px solid black;">Dias Sin Gestion</td>
						<td style="border: 1px solid black;">Logica</td>
						<td style="border: 1px solid black;">Nº Movimientos</td>
						<td style="border: 1px solid black;">Saldo</td>	
						<td style="border: 1px solid black;">Abogado</td>
						<td style="border: 1px solid black;">Categoria</td>
					</tr>
					
					<xsl:for-each select="$rows/ArbolJudicialCategoria">										
						
						<xsl:for-each select="lstDetalle/ArbolJudicialDetalle">
							<tr>
								<!--<td></td>-->
								<td style="border: 1px solid black;"><xsl:value-of select="NombreCliente"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="RutDeudor"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="NombreDeudor"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Asegurado"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Rol"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Tribunal"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="TipoCausa"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="MateriaJudicial"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="EstadoRol"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="FechaJudicial"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="DiasSinGestion"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Logica"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Cuenta"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Saldo"/></td>									
								<td style="border: 1px solid black;"><xsl:value-of select="Abogado"/></td>
								<td style="border: 1px solid black;"><xsl:value-of select="Categoria"/></td>
							</tr>
						</xsl:for-each>
					</xsl:for-each>
					
				</xsl:if>	
								
	</xsl:template>
</xsl:stylesheet>