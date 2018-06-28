CREATE VIEW dbo.CARTERA_CLIENTES_DOCUMENTOS_CPBT_DOC
AS
SELECT        dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_PCLID, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CTCID, dbo.PROVCLI.PCL_RUT, dbo.PROVCLI.PCL_NOMBRE, 
                         dbo.TIPOS_CPBTDOC_IDIOMAS.TCI_NOMBRE, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CCBID, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_NUMERO, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECING, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECDOC, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECVENC, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECULTGEST, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECPLAZO, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECCALCINT, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECCAST, dbo.ESTADOS_CARTERA_IDIOMAS.ECI_NOMBRE, ect2.ECT_NOMBRE, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_ESTCPBT, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODMON, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_TIPCAMBIO, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_ASIGNADO, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_MONTO, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_SALDO, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_GASTJUD, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_GASTOTRO, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_INTERESES, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_HONORARIOS, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CALCHON, dbo.BANCOS.BCO_NOMBRE, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_RUTGIR, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_NOMGIR, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_COMENTARIO, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_RETENT, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_NUMESP, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_NUMAGRUPA, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CARTA, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_COBRABLE, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CCTID, dbo.SUBCARTERAS.SBC_RUT, dbo.SUBCARTERAS.SBC_NOMBRE, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_DOCORI, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_DOCANT, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_TIPCART, dbo.TIPOS_CPBTDOC_IDIOMAS.TCI_IDID, dbo.ESTADOS_CARTERA_IDIOMAS.ECI_IDID, dbo.MOTIVO_COBRANZA_IDIOMAS.MCI_IDID, dbo.MONEDAS.MON_NOMBRE, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_COMPROMISO, dbo.ESTADOS_CARTERA.ECT_AGRUPA, dbo.ESTADOS_CARTERA.ECT_UTILIZA, dbo.ESTADOS_CARTERA.ECT_PREJUD, dbo.PROVCLI_CODIGO_CARGA.PCC_CODIGO, 
                         dbo.PROVCLI_CODIGO_CARGA.PCC_NOMBRE, dbo.PROVCLI_CODIGO_CARGA.PCC_CODID, dbo.DEUDORES.CTC_RUT, dbo.DEUDORES.CTC_NOMFANT, dbo.PROVCLI.PCL_NOMFANT, dbo.DEUDORES.CTC_DIRECCION, 
                         dbo.DEUDORES.CTC_COMID, dbo.ESTADOS_CARTERA_IDIOMAS.ECI_ESTID, dbo.MOTIVO_COBRANZA_IDIOMAS.MCI_NOMBRE, dbo.TIPOS_CPBTDOC_IDIOMAS.TCI_TPCID, dbo.DEUDORES.CTC_NUMERO, 
                         dbo.DEUDORES.CTC_DIGITO, dbo.DEUDORES.CTC_QUIEBRA, ect2.ECT_ESTID AS ESTIJ, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_IDCUENTA, dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_DESCCUENTA, DATEDIFF(DAY, 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_FECVENC, GETDATE()) AS DIAS_VENCIDO
FROM            dbo.ESTADOS_CARTERA INNER JOIN
                         dbo.ESTADOS_CARTERA_IDIOMAS ON dbo.ESTADOS_CARTERA.ECT_CODEMP = dbo.ESTADOS_CARTERA_IDIOMAS.ECI_CODEMP AND 
                         dbo.ESTADOS_CARTERA.ECT_ESTID = dbo.ESTADOS_CARTERA_IDIOMAS.ECI_ESTID INNER JOIN
                         dbo.ESTADOS_CARTERA AS ect2 ON dbo.ESTADOS_CARTERA_IDIOMAS.ECI_CODEMP = ect2.ECT_CODEMP INNER JOIN
                         dbo.PROVCLI_CODIGO_CARGA RIGHT OUTER JOIN
                         dbo.CARTERA_CLIENTES_CPBT_DOC ON dbo.PROVCLI_CODIGO_CARGA.PCC_PCLID = dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_PCLID AND 
                         dbo.PROVCLI_CODIGO_CARGA.PCC_CODID = dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODID LEFT OUTER JOIN
                         dbo.BANCOS ON dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP = dbo.BANCOS.BCO_CODEMP AND dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_BCOID = dbo.BANCOS.BCO_BCOID LEFT OUTER JOIN
                         dbo.SUBCARTERAS ON dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP = dbo.SUBCARTERAS.SBC_CODEMP AND dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_SBCID = dbo.SUBCARTERAS.SBC_SBCID INNER JOIN
                         dbo.CARTERA_CLIENTES ON dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP = dbo.CARTERA_CLIENTES.CTC_CODEMP AND dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_PCLID = dbo.CARTERA_CLIENTES.CTC_PCLID AND 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CTCID = dbo.CARTERA_CLIENTES.CTC_CTCID INNER JOIN
                         dbo.PROVCLI ON dbo.CARTERA_CLIENTES.CTC_CODEMP = dbo.PROVCLI.PCL_CODEMP AND dbo.CARTERA_CLIENTES.CTC_PCLID = dbo.PROVCLI.PCL_PCLID INNER JOIN
                         dbo.DEUDORES ON dbo.CARTERA_CLIENTES.CTC_CODEMP = dbo.DEUDORES.CTC_CODEMP AND dbo.CARTERA_CLIENTES.CTC_CTCID = dbo.DEUDORES.CTC_CTCID INNER JOIN
                         dbo.TIPOS_CPBTDOC_IDIOMAS ON dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP = dbo.TIPOS_CPBTDOC_IDIOMAS.TCI_CODEMP AND 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_TPCID = dbo.TIPOS_CPBTDOC_IDIOMAS.TCI_TPCID ON dbo.ESTADOS_CARTERA_IDIOMAS.ECI_CODEMP = dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP AND 
                         dbo.ESTADOS_CARTERA_IDIOMAS.ECI_ESTID = dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_ESTID AND ect2.ECT_CODEMP = dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP AND 
                         ect2.ECT_ESTID = dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_ESTIDJ INNER JOIN
                         dbo.MOTIVO_COBRANZA_IDIOMAS ON dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP = dbo.MOTIVO_COBRANZA_IDIOMAS.MCI_CODEMP AND 
                         dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_MTCID = dbo.MOTIVO_COBRANZA_IDIOMAS.MCI_MTCID INNER JOIN
                         dbo.MONEDAS ON dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODEMP = dbo.MONEDAS.MON_CODEMP AND dbo.CARTERA_CLIENTES_CPBT_DOC.CCB_CODMON = dbo.MONEDAS.MON_CODMON

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ESTADOS_CARTERA"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 233
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ESTADOS_CARTERA_IDIOMAS"
            Begin Extent = 
               Top = 6
               Left = 271
               Bottom = 136
               Right = 441
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ect2"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 233
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PROVCLI_CODIGO_CARGA"
            Begin Extent = 
               Top = 138
               Left = 271
               Bottom = 268
               Right = 441
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CARTERA_CLIENTES_CPBT_DOC"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BANCOS"
            Begin Extent = 
               Top = 270
               Left = 273
               Bottom = 400
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SUBCARTERAS"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right =', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CARTERA_CLIENTES_DOCUMENTOS_CPBT_DOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CARTERA_CLIENTES"
            Begin Extent = 
               Top = 402
               Left = 252
               Bottom = 515
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PROVCLI"
            Begin Extent = 
               Top = 516
               Left = 252
               Bottom = 646
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DEUDORES"
            Begin Extent = 
               Top = 648
               Left = 38
               Bottom = 778
               Right = 258
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TIPOS_CPBTDOC_IDIOMAS"
            Begin Extent = 
               Top = 648
               Left = 296
               Bottom = 778
               Right = 466
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MOTIVO_COBRANZA_IDIOMAS"
            Begin Extent = 
               Top = 780
               Left = 38
               Bottom = 910
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MONEDAS"
            Begin Extent = 
               Top = 780
               Left = 246
               Bottom = 910
               Right = 431
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CARTERA_CLIENTES_DOCUMENTOS_CPBT_DOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'CARTERA_CLIENTES_DOCUMENTOS_CPBT_DOC';

