﻿Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports ENTITY
Imports Janus.Windows.GridEX
Imports LOGIC
Imports UTILITIES
Imports Facturacion
Imports Logica.AccesoLogica
Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared

'importando librerias api conexion
Imports Newtonsoft.Json
Imports Presentacion.LoginResp
Imports Presentacion.ConnSiat
Imports Presentacion.RespMetodosPago
Imports Presentacion.EmisorResp
Imports Presentacion.RespTipoDoc
Imports Presentacion.NitResp
Imports Presentacion.ListarFacturas
Imports System.Xml

Public Class frmBillingDispatch
    Dim _inter As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Private _cargaCompleta = False

    'Token SIFAC
    Public tokenObtenido
    Public idNumi As String
    Public NPuntoVenta As Integer
    'Public dtDetalle As DataTable
    Public dt As DataTable

    Public CodProducto As String
    Public Cantidad As Integer
    Public PrecioU As Double
    Public PrecioTot As Double
    Public NombreProd As String
    Public NroFact As Integer

    Public _Fecha As Date

    Public QrUrl As String
    Public FactUrl As String
    Public SegundaLeyenda As String
    Public TerceraLeyenda As String
    Public Cudf As String

    Public NroTarjeta As String

#Region "Eventos"
    Private Sub frmBillingDispatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init()
    End Sub

    Private Sub cbChoferes_ValueChanged(sender As Object, e As EventArgs) Handles cbChoferes.ValueChanged
        Try
            If (_cargaCompleta) Then
                CargarPedidos()
                lblCantidadPedido.Text = dgjPedido.RowCount.ToString
                btnNotaVenta.Enabled = True
                btnFactura.Enabled = True
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btFacturar_Click(sender As Object, e As EventArgs) Handles btnNotaVenta.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim checks = Me.dgjPedido.GetCheckedRows()
            Dim listIdPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("Id").Value)).ToList()

            If (listIdPedido.Count = 0) Then
                Throw New Exception("Debe seleccionar por lo menos un pedido.")
            End If

            Dim list1 As List(Of VPedido_BillingDispatch) = CType(dgjPedido.DataSource, List(Of VPedido_BillingDispatch))
            'Dim list1 As List(Of VPedido_BillingDispatch) = New List(Of VPedido_BillingDispatch)

            list1 = list1.Where(Function(a) listIdPedido.Contains(a.Id)).ToList()

            'For i As Integer = 0 To list2.Count - 1 Step 1
            '    'If (list2(i).NroFactura.Equals("") Or list2(i).NroFactura.Equals("0")) Then
            '    If (list2(i).NroFactura = Nothing) Then
            '        list1.Add(list2(i))
            '    Else
            '        If (list2(i).NroFactura.Equals("") Or list2(i).NroFactura.Equals("0")) Then
            '            list1.Add(list2(i))
            '        End If
            '    End If
            'Next

            If (list1.Count = 0) Then
                ToastNotification.Show(Me, "No Existe ningun dato para generar Notas de Venta!!".ToUpper,
                                    My.Resources.OK,
                                    5 * 1000,
                                    eToastGlowColor.Red,
                                    eToastPosition.TopCenter)
                Return
            End If

            For i As Integer = 0 To list1.Count - 1 Step 1
                If L_YaSeGraboTV001(list1(i).Id) = False Then
                    GrabarTV001(Str(list1(i).Id))
                End If

                'Dim dtDetalle As DataTable = L_prObtenerDetallePedidoFactura(Str(list1(i).Id))

                'P_fnGenerarFactura(dtDetalle.Rows(0).Item("oanumi"), dtDetalle.Rows(0).Item("subtotal"), dtDetalle.Rows(0).Item("descuento"), dtDetalle.Rows(0).Item("total"), dtDetalle.Rows(0).Item("nit"), dtDetalle.Rows(0).Item("cliente"), dtDetalle.Rows(0).Item("codcli"))
                'P_prImprimirNotaVenta(dtDetalle.Rows(0).Item("oanumi"), True, True, idChofer)
                P_prImprimirNotaVenta(Str(list1(i).Id), True, True, idChofer)

            Next

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

            CargarPedidos()
            ToastNotification.Show(Me, "Notas de Venta Generadas Correctamente".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )



        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Function P_fnValidarFactura() As Boolean
        Return True
    End Function

    Private Function P_fnGenerarFactura(numi As String, subtotal As Double, descuento As Double, total As Double, nit As String, Nombre As String, Codcli As String) As Boolean
        Dim res As Boolean = False
        res = P_fnGrabarFacturarTFV001(numi, subtotal, descuento, total, nit, Nombre, Codcli) ' Grabar en la TFV001

        If (res) Then
            'Grabar Estado 5 de Facturado en la TO001D
            L_GrabarTO001D(numi, "5", "Factura")

            If (Not nit.Equals("0")) Then
                L_Grabar_Nit(nit, Nombre, "")
            Else
                L_Grabar_Nit(nit, "S/N", "")
            End If
        End If

        'Dim dtfv001 As DataTable = L_fnObtenerTabla("fvanitcli, fvadescli1, fvadescli2, fvaautoriz, fvanfac, fvaccont, fvafec,fvaest", "TFV001", "fvanumi=" + numi + " or fvanumi=" + "-" + numi)
        'If dtfv001.Rows.Count = 2 Then
        '    L_ActualizaNegativosTFV001(numi, "0")
        'End If

        Return res
    End Function


    Private Sub P_prImprimirFacturar(numi As String, impFactura As Boolean, grabarPDF As Boolean, nit As String)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        Dim QrFactura1 As String

        _Desc = CDbl(0)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = Now.Date.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        _Ds1 = L_Dosificacion("1", "1", _Fecha)

        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("yeautoriz").ToString
        _NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("yenunf")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                    Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                    Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Tables(0).Rows(0).Item("yenumi").ToString
        _Key = _Ds1.Tables(0).Rows(0).Item("yekey")
        _FechaAl = _Ds1.Tables(0).Rows(0).Item("yefal")

        Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        _NumFac = maxNFac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvasubtotal") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + nit + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))


        updateTO001C(numi, Str(_NumFac))
        _Ds = L_Reporte_Factura(numi, numi)

        _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación

        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        P_Global.Visualizador = New Visualizador
        Dim objrep As New Factura
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String
        Fecliteral = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        If mes = 1 Then
            mesl = "Enero"
        End If
        If mes = 2 Then
            mesl = "Febrero"
        End If
        If mes = 3 Then
            mesl = "Marzo"
        End If
        If mes = 4 Then
            mesl = "Abril"
        End If
        If mes = 5 Then
            mesl = "Mayo"
        End If
        If mes = 6 Then
            mesl = "Junio"
        End If
        If mes = 7 Then
            mesl = "Julio"
        End If
        If mes = 8 Then
            mesl = "Agosto"
        End If
        If mes = 9 Then
            mesl = "Septiembre"
        End If
        If mes = 10 Then
            mesl = "Octubre"
        End If
        If mes = 11 Then
            mesl = "Noviembre"
        End If
        If mes = 12 Then
            mesl = "Diciembre"
        End If
        Dim tipoPago = ObtenerTipoDePagoPedido(numi)

        Dim cadena As String = _Ds2.Tables(0).Rows(0).Item("scciu").ToString
        Dim posicion As Integer = cadena.IndexOf("-")
        Dim ciudad As String = cadena.Substring(0, posicion)

        Fecliteral = ciudad + ",  " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(_Ds.Tables(0))

        objrep.SetParameterValue("Fecliteral", Fecliteral)
        objrep.SetParameterValue("Nota2", _Ds1.Tables(0).Rows(0).Item("yenota2").ToString())
        'objrep.PrintOptions.PrinterName = "L4150 Series(Red) (Copiar 1)"

        objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
        objrep.SetParameterValue("Literal1", _Literal)
        objrep.SetParameterValue("NroFactura", _NumFac)
        objrep.SetParameterValue("NroAutoriz", _Autorizacion)
        objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
        objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
        objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scciu").ToString)
        objrep.SetParameterValue("ESFC", _Ds1.Tables(0).Rows(0).Item("yesfc").ToString)
        objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
        objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
        objrep.SetParameterValue("Tipo", "ORIGINAL")
        objrep.SetParameterValue("TipoPago", tipoPago)
        objrep.SetParameterValue("Logo", gb_ubilogo)
        'If imp = 1 Then
        '    objrep.SetParameterValue("Tipo", "ORIGINAL")
        'Else
        '    objrep.SetParameterValue("Tipo", "COPIA")
        'End If
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If

        L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)

        If (grabarPDF) Then
            'Copia de Factura en PDF
            If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
                Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
            End If
            objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

        End If
        'Dim pd As New PrintDocument()
        'pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
        'If (Not pd.PrinterSettings.IsValid) Then
        '    ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
        '                           My.Resources.WARNING, 5 * 1000,
        '                           eToastGlowColor.Blue, eToastPosition.BottomRight)
        'Else
        '    objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
        '    objrep.PrintToPrinter(1, False, 1, 1)
        'End If
        'objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
        'objrep.PrintToPrinter(1, False, 1, 1)




        ''For I = 0 To _Ds.Tables(0).Rows.Count - 1
        ''    _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        ''Next
        'If (impFactura) Then
        '    _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
        '    If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
        '        P_Global.Visualizador = New Visualizador 'Comentar
        '    End If


        '    Dim objrep As Object = Nothing
        '    objrep = New R_FacturaPreImpresa

        '    objrep.SetDataSource(_Ds.Tables(0))
        '    objrep.SetParameterValue("Hora", _Hora)
        '    objrep.SetParameterValue("Literal", _Literal)

        '    P_Global.Visualizador.CRV1.ReportSource = objrep
        '    P_Global.Visualizador.Show()
        '    P_Global.Visualizador.BringToFront()


        '    'If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
        '    '    P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
        '    '    P_Global.Visualizador.ShowDialog() 'Comentar
        '    '    P_Global.Visualizador.BringToFront() 'Comentar
        '    'End If

        '    'Dim pd As New PrintDocument()
        '    'pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
        '    'If (Not pd.PrinterSettings.IsValid) Then
        '    '    ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
        '    '                           My.Resources.WARNING, 5 * 1000,
        '    '                           eToastGlowColor.Blue, eToastPosition.BottomRight)
        '    'Else
        '    '    objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString '"EPSON TM-T20II Receipt5 (1)"
        '    '    objrep.PrintToPrinter(1, False, 1, 1)
        '    'End If

        'If (grabarPDF) Then
        '    'Copia de Factura en PDF
        '    If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
        '        Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
        '    End If
        '    objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

        'End If
        'End If

    End Sub
    Private Sub P_ReImprImprimirFacturar(numi As String, impFactura As Boolean, grabarPDF As Boolean, nit As String)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        Dim QrFactura1 As String

        _Desc = CDbl(0)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = Now.Date.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString
        _Ds1 = L_Dosificacion("1", "1", _Fecha)

        _Ds = L_Reporte_Factura(numi, numi)
        _Autorizacion = _Ds1.Tables(0).Rows(0).Item("yeautoriz").ToString
        _NumFac = CInt(_Ds1.Tables(0).Rows(0).Item("yenunf")) + 1
        _Nit = _Ds.Tables(0).Rows(0).Item("fvanitcli").ToString
        _Fechainv = Microsoft.VisualBasic.Right(_Fecha.ToShortDateString, 4) +
                    Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 5), 2) +
                    Microsoft.VisualBasic.Left(_Fecha.ToShortDateString, 2)
        _Total = _Ds.Tables(0).Rows(0).Item("fvatotal").ToString
        ice = _Ds.Tables(0).Rows(0).Item("fvaimpsi")
        _numidosif = _Ds1.Tables(0).Rows(0).Item("yenumi").ToString
        _Key = _Ds1.Tables(0).Rows(0).Item("yekey")
        _FechaAl = _Ds1.Tables(0).Rows(0).Item("yefal")

        _NumFac = CInt(_Ds.Tables(0).Rows(0).Item("fvanfac").ToString)
        'Dim maxnfac As Integer = L_fnObtenerMaxIdTabla("tfv001", "fvanfac", "fvaautoriz = " + _Autorizacion)
        '_NumFac = maxnfac + 1

        _TotalCC = Math.Round(CDbl(_Total), MidpointRounding.AwayFromZero)
        _Cod_Control = ControlCode.generateControlCode(_Autorizacion, _NumFac, _Nit, _Fechainv, CStr(_TotalCC), _Key)

        'Literal 
        _TotalLi = _Ds.Tables(0).Rows(0).Item("fvasubtotal") - _Ds.Tables(0).Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        QrFactura.Text = _Ds2.Tables(0).Rows(0).Item("scnit").ToString + "|" + Str(_NumFac).Trim + "|" + _Autorizacion + "|" + _Fecha + "|" + _Total + "|" + _TotalLi.ToString + "|" + _Cod_Control + "|" + nit + "|" + ice.ToString + "|0|0|" + Str(_Desc).Trim

        L_Modificar_Factura("fvanumi = " + CStr(numi),
                            "",
                            CStr(_NumFac),
                            CStr(_Autorizacion),
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            _Cod_Control,
                            _FechaAl.ToString("yyyy/MM/dd"),
                            "",
                            "",
                            CStr(numi))


        updateTO001C(numi, Str(_NumFac))
        _Ds = L_Reporte_Factura(numi, numi)

        _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación

        For I = 0 To _Ds.Tables(0).Rows.Count - 1
            _Ds.Tables(0).Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next
        P_Global.Visualizador = New Visualizador
        Dim objrep As New Factura
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String
        Fecliteral = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Dim tipoPago = ObtenerTipoDePagoPedido(numi)

        Dim cadena As String = _Ds2.Tables(0).Rows(0).Item("scciu").ToString
        Dim posicion As Integer = cadena.IndexOf("-")
        Dim ciudad As String = cadena.Substring(0, posicion)

        Fecliteral = ciudad + ",  " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(_Ds.Tables(0))

        objrep.SetParameterValue("Fecliteral", Fecliteral)
        objrep.SetParameterValue("Nota2", _Ds1.Tables(0).Rows(0).Item("yenota2").ToString())
        'objrep.PrintOptions.PrinterName = "L4150 Series(Red) (Copiar 1)"

        objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
        objrep.SetParameterValue("Literal1", _Literal)
        objrep.SetParameterValue("NroFactura", _NumFac)
        objrep.SetParameterValue("NroAutoriz", _Autorizacion)
        objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
        objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
        objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scciu").ToString)
        objrep.SetParameterValue("ESFC", _Ds1.Tables(0).Rows(0).Item("yesfc").ToString)
        objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
        objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)
        objrep.SetParameterValue("Tipo", "ORIGINAL")
        objrep.SetParameterValue("TipoPago", tipoPago)
        objrep.SetParameterValue("Logo", gb_ubilogo)
        'If imp = 1 Then
        '    objrep.SetParameterValue("Tipo", "ORIGINAL")
        'Else
        '    objrep.SetParameterValue("Tipo", "COPIA")
        'End If
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If
        'If (grabarPDF) Then
        '    'Copia de Factura en PDF
        '    If (Not Directory.Exists(gs_CarpetaRaiz + "\Facturas")) Then
        '        Directory.CreateDirectory(gs_CarpetaRaiz + "\Facturas")
        '    End If
        '    objrep.ExportToDisk(ExportFormatType.PortableDocFormat, gs_CarpetaRaiz + "\Facturas\" + CStr(_NumFac) + "_" + CStr(_Autorizacion) + ".pdf")

        'End If
        L_Actualiza_Dosificacion(_numidosif, _NumFac, numi)
    End Sub


    Private Sub P_ImprimirFactura(numi As String)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds1, _Ds2, _Ds3 As New DataSet
        Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim I, _NumFac, _numidosif, _TotalCC As Integer
        Dim ice, _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0
        Dim QrFactura1 As String


        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        Dim Dt As DataTable = L_ReporteFacturaNueva(numi)

        'Literal 
        _TotalLi = Dt.Rows(0).Item("fvasubtotal") - Dt.Rows(0).Item("fvadesc")
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"

        QrFactura.Text = Dt.Rows(0).Item("fvaQrUrl").ToString

        '_Ds = L_Reporte_Factura(numi, numi)
        _Ds2 = L_Reporte_Factura_Cia("1")
        _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación

        For I = 0 To Dt.Rows.Count - 1
            Dt.Rows(I).Item("fvaimgqr") = P_fnImageToByteArray(QrFactura.Image)
        Next


        P_Global.Visualizador = New Visualizador
        Dim objrep
        If Dt.Rows.Count <= 15 Then
            objrep = New R_FacturaMedioOficio
        Else
            objrep = New R_FacturaOficio
        End If




        objrep.SetDataSource(Dt)
        objrep.SetParameterValue("Nota2", "ESTA FACTURA CONTRIBUYE AL DESARROLLO DEL PAÍS, EL USO ILÍCITO SERÁ SANCIONADO PENALMENTE DE ACUERDO A LEY")

        objrep.SetParameterValue("Literal1", _Literal)

        objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
        objrep.SetParameterValue("NPuntoVenta", " No. Punto de Venta 9")
        objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
        objrep.SetParameterValue("Telefono", "Teléfono: " + _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scciu").ToString)

        objrep.SetParameterValue("ENit", _Ds2.Tables(0).Rows(0).Item("scnit").ToString)
        objrep.SetParameterValue("EActividad", _Ds2.Tables(0).Rows(0).Item("scact").ToString)


        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If

    End Sub

    Public Sub P_prImprimirNotaVenta(idPedido As String, impFactura As Boolean, grabarPDF As Boolean, idChofer As String)
        Dim _Fecha, _FechaAl As Date
        Dim _Ds, _Ds2, _Ds3 As New DataSet
        Dim _Hora, _Literal, _TotalDecimal, _TotalDecimal2 As String
        Dim _NumFac, _numidosif As Integer
        Dim _Desc, _TotalLi As Decimal
        Dim _VistaPrevia As Integer = 0

        _Desc = CDbl(0)

        Dim listResult = New LPedido().ListarDespachoXNotaVentaDeChofer(idChofer, idPedido)
        If (listResult.Count = 0) Then
            Throw New Exception("No hay registros para generar el reporte.")
        End If
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        _Fecha = Now.Date.ToString("dd/MM/yyyy")
        _Hora = Now.Hour.ToString + ":" + Now.Minute.ToString

        '_Ds = L_Reporte_Factura(numi, numi)


        'Literal 
        _TotalLi = listResult.Item(0).Total
        _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
        _TotalDecimal2 = CDbl(_TotalDecimal) * 100

        _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
        _Ds2 = L_Reporte_Factura_Cia("1")
        _Ds3 = L_ObtenerRutaImpresora("1") ' Datos de Impresion de Facturación
        Dim objrep As Object = Nothing
        Select Case _Ds3.Tables(0).Rows(0).Item("cbtimp").ToString
            Case "1"
                ReporteNotaVenta2(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "2"
                ReporteNotaVenta(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "3"
                ReporteNotaVenta3(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "4"
                ReporteNotaVenta4(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "5"
                ReporteNotaVenta5(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "6"
                ReporteNotaVenta6(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "7"
                ReporteNotaVenta7(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "8"
                ReporteNotaVenta8(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "9"
                ReporteNotaVenta9(idPedido, _Ds2, _Ds3, _Literal, listResult)
            Case "10"
                ReporteNotaVenta10(idPedido, _Ds2, _Ds3, _Literal, listResult)
        End Select
    End Sub

    Private Sub ReporteNotaVenta(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String

        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)

        mesl = ObtenerMesLiberal(mes)

        Fecliteral = "La Paz, " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Literal", _Literal)
        objrep.SetParameterValue("Fechali", Fecliteral)
        objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(2, False, 1, 1)
            End If
        End If
    End Sub

    Private Shared Function ObtenerMesLiberal(mes As Integer) As String
        Dim mesl As String = ""
        If mes = 1 Then
            mesl = "Enero"
        End If
        If mes = 2 Then
            mesl = "Febrero"
        End If
        If mes = 3 Then
            mesl = "Marzo"
        End If
        If mes = 4 Then
            mesl = "Abril"
        End If
        If mes = 5 Then
            mesl = "Mayo"
        End If
        If mes = 6 Then
            mesl = "Junio"
        End If
        If mes = 7 Then
            mesl = "Julio"
        End If
        If mes = 8 Then
            mesl = "Agosto"
        End If
        If mes = 9 Then
            mesl = "Septiembre"
        End If
        If mes = 10 Then
            mesl = "Octubre"
        End If
        If mes = 11 Then
            mesl = "Noviembre"
        End If
        If mes = 12 Then
            mesl = "Diciembre"
        End If

        Return mesl
    End Function

    Private Sub ReporteNotaVenta2(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta2
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String
        'Fecliteral = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Literal", _Literal)
        objrep.SetParameterValue("Fechali", Fecliteral)
        objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If
    End Sub

    Private Sub ReporteNotaVenta3(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta3
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String
        'Fecliteral = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        Dim tipoZona As String = L_fnVerificarZona("oanumi =" + idPedido)
        Dim esZonaLaPaz = IIf(tipoZona = "ES LA PAZ", "*", "")
        Dim esZonaElAlto = IIf(tipoZona = "ES EL ALTO", "*", "")


        objrep.Subreports.Item("NotaVenta3.rpt").SetDataSource(listResult)
        objrep.SetDataSource(listResult)
        'objrep.SetParameterValue("Literal", _Literal)
        'objrep.SetParameterValue("Fechali", Fecliteral)

        objrep.SetParameterValue("tipoZonaLaPaz", esZonaLaPaz)
        objrep.SetParameterValue("tipoZonaElAlto", esZonaElAlto)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        objrep.SetParameterValue("tipoZonaLaPaz", esZonaLaPaz, "NotaVenta3.rpt")
        objrep.SetParameterValue("tipoZonaElAlto", esZonaElAlto, "NotaVenta3.rpt")
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString, "NotaVenta3.rpt")
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString, "NotaVenta3.rpt")
        objrep.SetParameterValue("Logo", gb_ubilogo, "NotaVenta3.rpt")
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If

    End Sub

    Private Sub ReporteNotaVenta4(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta4
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String
        'Fecliteral = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        ' objrep.Subreports.Item("NotaVenta4.rpt").SetDataSource(listResult)
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        'objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString, "NotaVenta4.rpt")
        'objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString, "NotaVenta4.rpt")
        'objrep.SetParameterValue("Logo", gb_ubilogo, "NotaVenta4.rpt")

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If

    End Sub
    Private Sub ReporteNotaVenta5(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta5
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String
        'Fecliteral = _Ds.Tables(0).Rows(0).Item("fvafec").ToString
        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)

        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Literal", _Literal)
        objrep.SetParameterValue("Fechali", Fecliteral)
        objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(2, False, 1, 1)
            End If
        End If

    End Sub
    Private Sub ReporteNotaVenta7(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta7
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String

        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)
        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString

        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Literal", _Literal)
        objrep.SetParameterValue("Fechali", Fecliteral)
        objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(2, False, 1, 1)
            End If
        End If
    End Sub
    Private Sub ReporteNotaVenta6(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta_Ticket
        Dim zona, repartidor, vendedor As String

        Dim tZonaRepartidorVendedor As DataTable = L_fnObtenerZonaRepartidorDistribuidor("oanumi =" + idPedido)
        If tZonaRepartidorVendedor.Rows.Count() > 0 Then
            zona = tZonaRepartidorVendedor.Rows(0).Item("zona").ToString()
            repartidor = tZonaRepartidorVendedor.Rows(0).Item("repartidor").ToString()
            vendedor = tZonaRepartidorVendedor.Rows(0).Item("vendedor").ToString()
        Else
            zona = "--"
            repartidor = "--"
            vendedor = "--"
        End If
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("zona", zona)
        objrep.SetParameterValue("repartidor", repartidor)
        objrep.SetParameterValue("vendedor", vendedor)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                Dim nrocopias As Integer = _Ds3.Tables(0).Rows(0).Item("cbnrocopias")
                objrep.PrintToPrinter(nrocopias, False, 1, 1)
            End If
        End If
    End Sub

    Private Sub ReporteNotaVenta8(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta8
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String

        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If
    End Sub

    Private Sub ReporteNotaVenta9(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta9
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String

        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If
    End Sub
    Private Sub ReporteNotaVenta10(idPedido As String, _Ds2 As DataSet, _Ds3 As DataSet, _Literal As String, listResult As List(Of RDespachoNotaVenta))
        P_Global.Visualizador = New Visualizador
        Dim objrep As New NotaVenta10
        Dim dia, mes, ano As Integer
        Dim Fecliteral, mesl As String

        Fecliteral = listResult.Item(0).oafdoc
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        mesl = ObtenerMesLiberal(mes)

        Fecliteral = _Ds2.Tables(0).Rows(0).Item("scciu").ToString + " " + dia.ToString + " de " + mesl + " del " + ano.ToString
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("Telefono", _Ds2.Tables(0).Rows(0).Item("sctelf").ToString)
        objrep.SetParameterValue("Empresa", _Ds2.Tables(0).Rows(0).Item("scneg").ToString)
        objrep.SetParameterValue("Logo", gb_ubilogo)

        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CRV1.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                objrep.PrintToPrinter(1, False, 1, 1)
            End If
        End If
    End Sub
    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function
    Private Function P_fnGrabarFacturarTFV001(numi As String, subtotal As Double, descuento As Double, total As Double, nit As String, nameCliente As String, Codcli As String) As Boolean
        Dim a As Double = subtotal
        Dim b As Double = CDbl(0) 'Ya esta calculado el 55% del ICE
        Dim c As Double = CDbl("0")
        Dim d As Double = CDbl("0")
        Dim e As Double = a - b - c - d
        Dim f As Double = descuento
        Dim g As Double = e - f
        Dim h As Double = g * (13 / 100)

        Dim res As Boolean = False
        Dim _Hora As String = Now.Hour.ToString("D2") + ":" + Now.Minute.ToString("D2")
        Dim Anhio As Integer = Now.Date.Year
        'Grabado de Cabesera Factura

        L_Grabar_Factura(numi,
                        Now.Date.ToString("yyyy/MM/dd"),
                        NroFact,
                        gb_cufSifac,
                        "1",
                        nit,
                        Codcli,
                        nameCliente,
                        "",
                        CStr(Format(a, "####0.00")),
                        CStr(Format(b, "####0.00")),
                        CStr(Format(c, "####0.00")),
                        CStr(Format(d, "####0.00")),
                        CStr(Format(e, "####0.00")),
                        CStr(Format(f, "####0.00")),
                        CStr(Format(g, "####0.00")),
                        CStr(Format(h, "####0.00")),
                        "",
                        Now.Date.ToString("yyyy/MM/dd"),
                        "''",
                        "1",
                        numi,
                        _Hora,
                        QrUrl,
                        FactUrl,
                        SegundaLeyenda,
                        TerceraLeyenda,
                        Cudf,
                        9,
                        Anhio)


        Dim dtDetalle As DataTable = L_prObtenerDetallePedido(numi)
        For i As Integer = 0 To dtDetalle.Rows.Count - 1 Step 1

            L_Grabar_Factura_Detalle(numi.ToString,
                                        dtDetalle.Rows(i).Item("obcprod").ToString,
                                         dtDetalle.Rows(i).Item("producto").ToString,
                                        dtDetalle.Rows(i).Item("obpcant").ToString,
                                        dtDetalle.Rows(i).Item("obpbase").ToString,
                                        numi)

        Next
        updateTO001C(numi, NroFact)

        Return True
    End Function

    Private Sub btReporteDespachoCliente_Click(sender As Object, e As EventArgs) Handles btReporteDespachoCliente.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim listResult = New LPedido().ListarDespachoXClienteDeChofer(idChofer, IIf(cbEstado.SelectedIndex = 0, ENEstadoPedido.DICTADO, ENEstadoPedido.ENTREGADO))
            Dim lista = (From a In listResult
                         Where a.oafdoc >= Tb_Fecha.Value And
                                a.oafdoc <= Tb_FechaHasta.Value).ToList
            If (lista.Count = 0) Then
                Throw New Exception("No hay registros para generar el reporte.")
            End If

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New DespachoXCliente

            objrep.SetDataSource(lista)
            objrep.SetParameterValue("nroDespacho", String.Empty)
            objrep.SetParameterValue("nombreDistribuidor", cbChoferes.Text)
            objrep.SetParameterValue("FechaDocumento", Tb_Fecha.Value)
            objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)

            P_Global.Visualizador.CRV1.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btReporteDespachoLinea_Click(sender As Object, e As EventArgs) Handles btReporteDespachoLinea.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim listResult = New LPedido().ListarDespachoXProductoDeChofer(idChofer, IIf(cbEstado.SelectedIndex = 0, ENEstadoPedido.DICTADO, ENEstadoPedido.ENTREGADO), Tb_Fecha.Value, Tb_FechaHasta.Value)
            Dim lista = (From a In listResult
                         Group a By a.canumi, a.cadesc, a.categoria Into grupo = Group
                         Select New RDespachoXProducto With {
                          .canumi = grupo.FirstOrDefault().canumi,
                          .cacod = grupo.FirstOrDefault().cacod,
                          .cadesc = grupo.FirstOrDefault().cadesc,
                          .categoria = grupo.FirstOrDefault().categoria,
                          .obpcant = grupo.Sum(Function(item) item.obpcant),
                          .Caja = grupo.Sum(Function(item) item.Caja),
                          .Unidad = grupo.Sum(Function(item) item.Unidad),
                          .Total = grupo.Sum(Function(item) item.Total),
                          .Conv = grupo.FirstOrDefault().Conv,
                          .Pesokg = grupo.Sum(Function(item) item.Pesokg),
                          .cagr1 = grupo.FirstOrDefault().cagr1,
                          .cmdesc = grupo.FirstOrDefault().cmdesc,
                          .cauventa = grupo.FirstOrDefault().cauventa,
                          .uventa = grupo.FirstOrDefault().uventa,
                          .caumax = grupo.FirstOrDefault().caumax,
                          .uvmaxima = grupo.FirstOrDefault().uvmaxima,
                          .oaccbnumi = grupo.FirstOrDefault().oaccbnumi
                        }).ToList()
            If (lista.Count = 0) Then
                Throw New Exception("No hay registros para generar el reporte.")
            End If
            Dim empresaId = ObtenerEmpresaHabilitada()
            Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.DESPACHOXPRODUCTO))
            For Each fila As DataRow In empresaHabilitada.Rows
                Select Case fila.Item("TipoReporte").ToString
                    Case ENReporteTipo.DESPACHOXPRODUCTO_AgrupadoXCategoria
                        Dim objrep As New DespachoXProducto
                        SerParametros(lista, objrep)
                    Case ENReporteTipo.DESPACHOXPRODUCTO_SinAgrupacion
                        Dim objrep As New DespachoXProductoSinAgrupacion
                        SerParametros(lista, objrep)
                    Case ENReporteTipo.DESPACHOXPRODUCTO_AgrupadoXProveedor
                        Dim objrep As New DespachoXProductoProv
                        SerParametros(lista, objrep)
                End Select
            Next
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub SerParametros(listResult As List(Of RDespachoXProducto), objrep As Object)
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        P_Global.Visualizador = New Visualizador
        objrep.SetDataSource(listResult)
        objrep.SetParameterValue("nroDespacho", String.Empty)
        objrep.SetParameterValue("nombreDistribuidor", cbChoferes.Text)
        objrep.SetParameterValue("FechaDocumento", Tb_Fecha.Value)
        objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)
        P_Global.Visualizador.CRV1.ReportSource = objrep
        P_Global.Visualizador.ShowDialog()
        P_Global.Visualizador.BringToFront()
    End Sub

    Private Sub dgjPedido_SelectionChanged(sender As Object, e As EventArgs) Handles dgjPedido.SelectionChanged
        Try
            Dim idPedido = 0
            If (dgjPedido.GetRows().Count > 0) Then
                idPedido = Convert.ToInt32(dgjPedido.CurrentRow.Cells("Id").Value)
            End If

            CargarProductos(idPedido)
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub Tb_Fecha_ValueChanged(sender As Object, e As EventArgs) Handles Tb_Fecha.ValueChanged
        Try
            If (_cargaCompleta) Then
                CargarPedidos()
                lblCantidadPedido.Text = dgjPedido.RowCount.ToString
                btnNotaVenta.Enabled = True
                btnFactura.Enabled = True
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
#End Region

#Region "Privado, metodos y funciones"
    Private Sub Init()
        Try
            ConfigForm()
            CargarChoferes()
            Tb_Fecha.Value = DateTime.Today
            Tb_FechaHasta.Value = DateTime.Today
            _cargaCompleta = True
            cbEstado.SelectedIndex = 0
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub ConfigForm()
        Try
            Me.Text = "FACTURACIÓN/DESPACHO"
            ' Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CargarChoferes()
        Try
            Dim listResult As List(Of VCombo) = New LPersonal().ListarRepatidorCombo()

            With cbChoferes.DropDownList
                .Columns.Clear()

                .Columns.Add("Id").Width = 30
                .Columns("Id").Caption = "Id"
                .Columns("Id").Visible = True

                .Columns.Add("Descripcion").Width = 180
                .Columns("Descripcion").Caption = "Nombre repartidor"
                .Columns("Descripcion").Visible = True

                .ValueMember = "Id"
                .DisplayMember = "Descripcion"
                .DataSource = listResult

                .AlternatingColors = True
                .AllowColumnDrag = False
                .AutomaticSort = False
                .Refresh()
            End With
            cbChoferes.VisualStyle = VisualStyle.Office2007

            cbChoferes.SelectedIndex = 0
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CargarPedidos()
        Try
            Dim lista As List(Of VPedido_BillingDispatch) = ObtenerListaPedido()
            ArmarListaPedido(lista)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function ObtenerListaPedido() As List(Of VPedido_BillingDispatch)
        Dim idChofer = Me.cbChoferes.Value
        If (Not IsNumeric(idChofer)) Then
            Throw New Exception("Debe seleccionar un chofer.")
        End If
        If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
            Throw New Exception("Debe seleccionar un chofer.")
        End If

        Dim listResult = New LPedido().ListarPedidoAsignadoAChoferFechas(idChofer, IIf(cbEstado.SelectedIndex = 0, ENEstadoPedido.DICTADO, ENEstadoPedido.ENTREGADO), Tb_Fecha.Value, Tb_FechaHasta.Value)
        'Dim lista = (From a In listResult
        '             Where a.Fecha >= Tb_Fecha.Value And
        '                   a.Fecha <= Tb_FechaHasta.Value).ToList
        Return listResult
    End Function

    Private Sub ArmarListaPedido(lista As List(Of VPedido_BillingDispatch))
        dgjPedido.BoundMode = BoundMode.Bound
        dgjPedido.DataSource = lista
        dgjPedido.RetrieveStructure()


        With dgjPedido.RootTable.Columns("Fecha")
            .Caption = "Fecha Pedido"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Position = 0
        End With

        With dgjPedido.RootTable.Columns("NombreCliente")
            .Caption = "Cliente"
            .Width = 400
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Position = 1
        End With

        With dgjPedido.RootTable.Columns("Id")
            .Caption = "Pedido"
            .Width = 60
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Position = 2
        End With

        With dgjPedido.RootTable.Columns("NombreVendedor")
            .Caption = "Vendedor"
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Position = 3
        End With

        With dgjPedido.RootTable.Columns("idZona")
            .Caption = "Zona"
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = True
            .Position = 4
        End With
        With dgjPedido.RootTable.Columns("EstaFacturado")
            .Caption = "Facturado"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .Visible = False
            .Position = 5
        End With

        With dgjPedido.RootTable.Columns("NroFactura")
            .Caption = "Nro. Factura"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Position = 6
        End With
        With dgjPedido.RootTable.Columns("nombreZona")
            .Caption = "Nombre Zona"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Position = 7
        End With
        With dgjPedido.RootTable.Columns("observacion")
            .Caption = "observacion"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Position = 8

        End With
        With dgjPedido.RootTable.Columns("Subtotal")
            .Caption = "Subtotal"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Position = 9
        End With
        With dgjPedido.RootTable.Columns("Descuento")
            .Caption = "Descuento"
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Position = 10
        End With
        With dgjPedido.RootTable.Columns("Total")
            .Caption = "Total"
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Position = 11
        End With
        dgjPedido.RootTable.Columns.Add(New GridEXColumn("Check"))
        With dgjPedido.RootTable.Columns("Check")
            .Caption = "Seleccionar"
            .Width = 100
            .ShowRowSelector = True
            .UseHeaderSelector = True
            .FilterEditType = FilterEditType.NoEdit
            .Position = 12
        End With

        With dgjPedido
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False


            '.SelectionMode = SelectionMode.MultipleSelection
            '.AlternatingColors = True
            .AllowEdit = InheritableBoolean.False
            '.AllowColumnDrag = False
            '.AutomaticSort = False
            '.ColumnHeaders = InheritableBoolean.True

            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        dgjPedido.VisualStyle = VisualStyle.Office2007
    End Sub

    Private Sub CargarProductos(idPedido As Integer)
        Try
            Dim listResult = New LProducto().ListarProductoXPedido(idPedido)

            dgjProducto.BoundMode = Janus.Data.BoundMode.Bound
            dgjProducto.DataSource = listResult
            dgjProducto.RetrieveStructure()

            With dgjProducto.RootTable.Columns("Id")
                .Visible = False
            End With

            With dgjProducto.RootTable.Columns("NombreProducto")
                .Caption = "Producto"
                .Width = 250
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = True
            End With

            With dgjProducto.RootTable.Columns("Cantidad")
                .Caption = "Cantidad"
                .Width = 80
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
            End With

            With dgjProducto.RootTable.Columns("Precio")
                .Caption = "Precio"
                .Width = 80
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
            End With
            With dgjProducto.RootTable.Columns("SubTotal")
                .Caption = "SubTotal"
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
            End With
            With dgjProducto.RootTable.Columns("Descuento")
                .Caption = "Descuento"
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
            End With
            With dgjProducto.RootTable.Columns("Total")
                .Caption = "Total"
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                .Visible = True
                .FormatString = "0.00"
                .AggregateFunction = AggregateFunction.Sum
            End With

            With dgjProducto
                .GroupByBoxVisible = False
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                '.FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .VisualStyle = VisualStyle.Office2007
                .SelectionMode = SelectionMode.MultipleSelection
                .AlternatingColors = True
                .AllowEdit = InheritableBoolean.False
                .AllowColumnDrag = False
                .AutomaticSort = False
                '.ColumnHeaders = InheritableBoolean.False

                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               ENMensaje.MEDIANO,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               ENMensaje.MEDIANO,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
#End Region

#Region "Publico, metodos y funciones"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btVolverDist_Click(sender As Object, e As EventArgs) Handles btVolverDist.Click
        Try
            VolverPedidoDistribucion()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub VolverPedidoDistribucion()
        Try
            Dim checks = Me.dgjPedido.GetCheckedRows()
            Dim listIdPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("Id").Value)).ToList()
            Dim listFacPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("NroFactura").Value)).ToList()

            If (listIdPedido.Count = 0) Then
                Throw New Exception("Debe seleccionar por lo menos un pedido.")
            End If
            Dim _Result As MsgBoxResult

            _Result = MsgBox("Esta seguro de volver a distribucion?", MsgBoxStyle.YesNo, "Advertencia")
            If _Result = MsgBoxResult.Yes Then
                For Each nfact As Integer In listFacPedido
                    Dim nro As Integer = nfact
                    If nro > 0 Then
                        MostrarMensajeError("Debe de seleccionar solo los pedidos que no hayan sido facturados")
                        Exit Sub
                    End If
                Next

                For Each idPedido As Integer In listIdPedido
                    Dim idP As Integer = idPedido
                    Dim dt As DataTable = L_EstadoTO001D(idP)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item("estadomax") > 4 Then
                            MostrarMensajeError("No puede volver a Distribucion porque el pedido ya se encuentra en:" + dt.Rows(0).Item("oaddescrip"))
                            Exit Sub
                        End If
                    Else
                        MostrarMensajeError("No puede volver a Distribucion ")
                        Exit Sub
                    End If
                Next

                Dim idChofer = Me.cbChoferes.Value
                Dim result = New LPedido().VolverPedidoDistribucion(listIdPedido, idChofer)
                If (result) Then
                        CargarPedidos()
                        MostrarMensajeOk("Pedidos volvieron a Distribución correctamente")
                    End If


            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

#End Region
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _inter = _inter + 1
        If _inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
        'Me.Opacity = 100
        'Timer1.Enabled = False
    End Sub

    Private Sub btReporteDespachoPedido_Click(sender As Object, e As EventArgs) Handles btReporteDespachoPedido.Click
        Try
            Dim idChofer = Me.cbChoferes.Value
            If (Not IsNumeric(idChofer)) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim listResult = New LPedido().ListarDespachoDetalleXChofer(idChofer, IIf(cbEstado.SelectedIndex = 0, ENEstadoPedido.DICTADO, ENEstadoPedido.ENTREGADO))
            Dim lista = (From a In listResult
                         Where a.oafdoc >= Tb_Fecha.Value And
                                a.oafdoc <= Tb_FechaHasta.Value).ToList
            If (lista.Count = 0) Then
                Throw New Exception("No hay registros para generar el reporte.")
            End If

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New R_Ventasdespacho

            objrep.SetDataSource(lista)
            'objrep.SetParameterValue("nroDespacho", String.Empty)
            'objrep.SetParameterValue("nombreDistribuidor", cbChoferes.Text)
            'objrep.SetParameterValue("FechaDocumento", Tb_Fecha.Value)
            'objrep.SetParameterValue("nombreUsuario", P_Global.gs_user)

            P_Global.Visualizador.CRV1.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub Tb_FechaHasta_ValueChanged(sender As Object, e As EventArgs) Handles Tb_FechaHasta.ValueChanged
        Try
            If (_cargaCompleta) Then
                CargarPedidos()
                lblCantidadPedido.Text = dgjPedido.RowCount.ToString
                btnNotaVenta.Enabled = True
                btnFactura.Enabled = True
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btnFactura_Click(sender As Object, e As EventArgs) Handles btnFactura.Click
        Try
            tokenObtenido = ObtToken()

            Dim idChofer = Me.cbChoferes.Value
            If (Convert.ToInt32(idChofer) = ENCombo.ID_SELECCIONAR) Then
                Throw New Exception("Debe seleccionar un chofer.")
            End If

            Dim checks = Me.dgjPedido.GetCheckedRows()
            Dim listIdPedido = checks.Select(Function(a) Convert.ToInt32(a.Cells("Id").Value)).ToList()

            If (listIdPedido.Count = 0) Then
                Throw New Exception("Debe seleccionar por lo menos un pedido.")
            End If

            Dim list2 As List(Of VPedido_BillingDispatch) = CType(dgjPedido.DataSource, List(Of VPedido_BillingDispatch))
            Dim list1 As List(Of VPedido_BillingDispatch) = New List(Of VPedido_BillingDispatch)

            list2 = list2.Where(Function(a) listIdPedido.Contains(a.Id)).ToList()

            For i As Integer = 0 To list2.Count - 1 Step 1
                'If (list2(i).NroFactura.Equals("") Or list2(i).NroFactura.Equals("0")) Then
                If (list2(i).NroFactura = Nothing) Then
                    list1.Add(list2(i))
                Else
                    If (list2(i).NroFactura.Equals("") Or list2(i).NroFactura.Equals("0")) Then
                        list1.Add(list2(i))
                    Else
                        P_ImprimirFactura(list2(i).Id)
                        'Return
                    End If
                End If
            Next

            If (list1.Count = 0) Then
                ToastNotification.Show(Me, "No Existe ningun dato para generar Factura!!".ToUpper,
                                    My.Resources.WARNING,
                                    5 * 1000,
                                    eToastGlowColor.Red,
                                    eToastPosition.TopCenter)
                Return
            End If

            For i As Integer = 0 To list1.Count - 1 Step 1

                'Dim dtDetallePedido As DataTable = L_prObtenerDetallePedido(Str(list1(i).Id))
                Dim Succes As Integer = Emisor(tokenObtenido, Str(list1(i).Id), 9)
                If Succes = 200 Then
                    If L_YaSeGraboTV001(list1(i).Id) = False Then
                        GrabarTV001(Str(list1(i).Id))
                    End If
                    Dim dtEncabezado As DataTable = L_prObtenerEncabezadoPedido(Str(list1(i).Id))
                    P_fnGenerarFactura(dtEncabezado.Rows(0).Item("oanumi"), dtEncabezado.Rows(0).Item("subtotal"), dtEncabezado.Rows(0).Item("descuento"), dtEncabezado.Rows(0).Item("total"), dtEncabezado.Rows(0).Item("nit"), dtEncabezado.Rows(0).Item("cliente"), dtEncabezado.Rows(0).Item("codcli"))
                    P_ImprimirFactura(list2(i).Id)
                Else
                    ToastNotification.Show(Me, "El pedido Nro: " + Str(list1(i).Id) + " no pudo ser facturado".ToUpper,
                                   My.Resources.WARNING,
                                   4 * 1000,
                                   eToastGlowColor.Red,
                                   eToastPosition.TopCenter)
                End If



                ' P_prImprimirNotaVenta(dtDetalle.Rows(0).Item("oanumi"), True, True, idChofer)
                ' P_prImprimirNotaVenta(Str(list1(i).Id), True, True, idChofer)

            Next

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

            CargarPedidos()
            ToastNotification.Show(Me, "Facturas Generadas Correctamente".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )



        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub


    Private Sub cbEstado_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbEstado.SelectedValueChanged
        Try
            If (_cargaCompleta) Then
                CargarPedidos()
                lblCantidadPedido.Text = dgjPedido.RowCount.ToString
                btnNotaVenta.Enabled = True
                btnFactura.Enabled = True
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub dgjPedido_KeyDown(sender As Object, e As KeyEventArgs) Handles dgjPedido.KeyDown
        Dim listaPedido As List(Of VPedido_BillingDispatch) = ObtenerListaPedido()
        If (e.KeyData = Keys.Control + Keys.F) Then
            listaPedido = listaPedido.Where(Function(a) a.observacion.Contains("F,") Or a.observacion.Contains("f,")).ToList()
            ArmarListaPedido(listaPedido)
            btnNotaVenta.Enabled = False
            btnFactura.Enabled = True
            lblCantidadPedido.Text = listaPedido.Count.ToString
        End If
        If (e.KeyData = Keys.Control + Keys.N) Then
            listaPedido = listaPedido.Where(Function(a) Not (a.observacion.Contains("F,") OrElse a.observacion.Contains("f,"))).ToList()
            ArmarListaPedido(listaPedido)
            btnFactura.Enabled = False
            btnNotaVenta.Enabled = True
            lblCantidadPedido.Text = listaPedido.Count.ToString
        End If
    End Sub




    'Funciones de conexion con sifac para facturacion
    Public Shared Function ObtToken()
        Dim api = New DBApi()

        Dim Lenvio = New LoginEnvio()
        Lenvio.email = gb_email
        Lenvio.password = gb_password

        Dim url = gb_url + "/api/v2/login"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "bearer "),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.Post(url, headers, parametros, Lenvio)
        Dim json = JsonConvert.SerializeObject(Lenvio)
        ''MsgBox(json)

        Dim result = JsonConvert.DeserializeObject(Of RespuestLogin)(response)
        Dim Token As String
        Dim json1 = JsonConvert.SerializeObject(response)
        '' MsgBox(json1)
        Token = result.data.access_token.ToString
        Return Token
    End Function
    Public Function VerifConexion(tokenObtenido)

        Dim api = New DBApi()

        Dim url = gb_url + "/api/v2/conexion-siat"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of SiatConn)(response)

        Dim Codigoconn As String
        Codigoconn = result.meta.code
        'Dim json = JsonConvert.SerializeObject(result)
        'MsgBox(json)
        Return Codigoconn
    End Function


    Public Function Emisor(tokenObtenido, idNumi, NPuntoVenta)
        Try
            Dim api = New DBApi()
            Dim Emenvio = New EmisorEnvio.Emisor()


            Dim dtEncabezado As DataTable = L_prObtenerEncabezadoPedido(idNumi)
            Dim dtDetalle As DataTable = L_prObtenerDetallePedidoFact(idNumi)

            'Dim code = VerifConexion(tokenObtenido)
            'If (code = 200) Then

            '    If dtEncabezado.Rows(0).Item("ccdct").ToString() = "5" Then ''El tipo de Doc. es Nit
            '        Dim Succes As Integer = VerificarNit(tokenObtenido, dtEncabezado.Rows(0).Item("nit").ToString())
            '        If Succes <> 200 Then

            '            ToastNotification.Show(Me, "El Nit del cliente en el Pedido: " + idNumi + "No es válido, no puede facturarse".ToUpper,
            '           My.Resources.Mensaje,
            '           4 * 1000,
            '           eToastGlowColor.Red,
            '           eToastPosition.BottomLeft)
            '            Return ""
            '            Exit Function
            '        End If
            '    End If

            'End If


            Dim array(dtDetalle.Rows.Count - 1) As EmisorEnvio.Detalle
            Dim val = 0
            PrecioTot = 0
            For Each row In dtDetalle.Rows
                Dim EmenvioDetalle = New EmisorEnvio.Detalle()

                EmenvioDetalle.codigoProductoSin = (row("cdcodsin").ToString)
                EmenvioDetalle.codigoProducto = (row("obcprod").ToString)
                EmenvioDetalle.descripcion = (row("producto").ToString)
                EmenvioDetalle.unidadMedida = Convert.ToInt32(row("cdcodu"))
                EmenvioDetalle.cantidad = (row("obpcant"))
                EmenvioDetalle.precioUnitario = (row("obpbase"))
                EmenvioDetalle.montoDescuento = (row("obdesc"))
                EmenvioDetalle.subTotal = (row("obtotal"))
                EmenvioDetalle.numeroSerie = ""
                EmenvioDetalle.numeroImei = ""

                PrecioTot = PrecioTot + (row("obtotal")) 'total

                array(val) = EmenvioDetalle
                'vector = array
                val = val + 1

            Next
            Dim NumFactura As Integer
            Dim email As String
            Dim CodMetPago As Integer


            If dtEncabezado.Rows(0).Item("ccemail").ToString = String.Empty Then
                email = "cliente@crex.com.bo"
            Else
                email = dtEncabezado.Rows(0).Item("ccemail").ToString
            End If
            If dtEncabezado.Rows(0).Item("tipoventa") = 0 Then
                CodMetPago = 6 'Credito
            Else
                CodMetPago = 1 'Contado
            End If
            Dim dtmax = L_fnObtenerMaxFact(NPuntoVenta, Convert.ToInt32(Now.Date.Year))
            If dtmax.Rows.Count = 0 Then
                NumFactura = 1
            Else
                Dim maxNFac As Integer = dtmax.Rows(0).Item("fvanfac")
                NumFactura = maxNFac + 1
            End If

            Emenvio.numeroFactura = NumFactura
            Emenvio.nombreRazonSocial = dtEncabezado.Rows(0).Item("cliente").ToString()
            Emenvio.codigoTipoDocumentoIdentidad = Convert.ToInt32(dtEncabezado.Rows(0).Item("ccdct"))
            Emenvio.numeroDocumento = dtEncabezado.Rows(0).Item("nit").ToString()
            Emenvio.complemento = "" '---------------------------------
            Emenvio.codigoCliente = "C-" + dtEncabezado.Rows(0).Item("codcli").ToString()
            Emenvio.codigoMetodoPago = CodMetPago
            Emenvio.numeroTarjeta = ""
            Emenvio.codigoPuntoVenta = NPuntoVenta
            Emenvio.codigoDocumentoSector = 1 '-------------------
            Emenvio.codigoMoneda = 1 'falta
            Emenvio.tipoCambio = 1 'CDbl(cbCambioDolar.Text) '--------------------
            Emenvio.descuentoAdicional = 0 '-------------------
            Emenvio.montoTotal = Format((PrecioTot - Emenvio.descuentoAdicional), "#.#0")
            Emenvio.montoTotalSujetoIva = Format((PrecioTot - Emenvio.descuentoAdicional), "#.#0")
            Emenvio.montoTotalMoneda = Format((PrecioTot - Emenvio.descuentoAdicional), "#.#0")
            Emenvio.montoGiftCard = 0 '----------------
            Emenvio.codigoExcepcion = 0 '---------------
            Emenvio.usuario = gs_user
            Emenvio.email = email
            Emenvio.actividadEconomica = 471110 'Actividad económica una sola para todos los productos
            Emenvio.detalles = array
            Dim json = JsonConvert.SerializeObject(Emenvio)
            Dim url = gb_url + "/api/v2/emision"

            Dim headers = New List(Of Parametro) From {
                New Parametro("Authorization", "Bearer " + tokenObtenido),
                New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
            }

            Dim parametros = New List(Of Parametro)

            Dim response = api.Post(url, headers, parametros, Emenvio)

            Dim result = JsonConvert.DeserializeObject(Of RespEmisor)(response)
            Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

            Dim codigo = result.meta.code
            Dim xml As String
            If codigo = 200 Then
                Dim details = result.data.details
                xml = result.data.dataXml
                Dim doc As XmlDocument = New XmlDocument()
                doc.LoadXml(xml)
                Dim nodo = doc.DocumentElement("cabecera")
                gb_cufSifac = nodo.ChildNodes.Item(5).InnerText 'extrae dato CUF de xml sifac

                NroFact = result.data.numeroFactura
                QrUrl = result.data.qrUrl
                FactUrl = result.data.facturaUrl
                SegundaLeyenda = result.data.leyenda
                TerceraLeyenda = result.data.terceraLeyenda
                Cudf = result.data.cufd

                'Dim notifi = New notifi

                'notifi.tipo = 2
                'notifi.Context = "SIFAC".ToUpper
                'notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Factura enviada al Siat".ToUpper
                'notifi.ShowDialog()

            ElseIf codigo = 400 Or codigo = 500 Then

                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "sifac".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & "la factura no pudo enviarse al siat".ToUpper
                notifi.ShowDialog()

            ElseIf codigo = 401 Or codigo = 404 Or codigo = 405 Then
                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat".ToUpper
                notifi.ShowDialog()

            ElseIf codigo = 422 Then
                Dim details = JsonConvert.SerializeObject(resultError.errors.details)

                Dim Mensaje = Replace(details, """", "")
                Dim Mensaje1 = Replace(Mensaje, "[", "")
                Dim Mensaje2 = Replace(Mensaje1, "]", "")
                Dim Mensaje3 = Split(Mensaje2, ".")

                Dim MensajeInicial = Mensaje3(0)
                Dim NroFactUlt = Mensaje3(2).Trim

                If MensajeInicial = "La Factura No" Then

                    If dtmax.Rows.Count = 0 Then
                        Dim maxNFac As Integer = 0
                        NumFactura = maxNFac + 2
                    Else
                        Dim maxNFac As Integer = dtmax.Rows(0).Item("fvanfac")
                        NumFactura = maxNFac + 2
                    End If

                    Dim FechaI = Date.Today.AddDays(-1).ToString("yyyy-MM-dd")
                    Dim FechaF = (Now.Date.ToString("yyyy-MM-dd"))

                    Dim dt = ListarFacturas(tokenObtenido, 9.ToString, FechaI, FechaF)
                    If dt.Count > 0 Then
                        Dim Conteo As Integer = dt.Count
                        Dim NAutorizacion = dt(Conteo - 1).cuf
                        Dim NroFactAnulada = dt(Conteo - 1).numeroFactura

                        Dim Succes As Integer = F0_PedidosAsignacion.AnularFactura(tokenObtenido, NAutorizacion, 3, False)
                        If Succes = 200 Then
                            L_fnInsertarFacturaAnuladaSifac((Now.Date.ToString("yyyy-MM-dd")), 9, NroFactAnulada, NAutorizacion, "ANULADA")
                        Else
                            L_fnInsertarFacturaAnuladaSifac((Now.Date.ToString("yyyy-MM-dd")), 9, NroFactAnulada, NAutorizacion, "NO ANULADA")
                        End If
                    Else
                        L_fnInsertarFacturaAnuladaSifac((Now.Date.ToString("yyyy-MM-dd")), 9, (NumFactura - 1), 0, "NO ANULADA")
                    End If

                    Emenvio.numeroFactura = NumFactura

                    Dim json2 = JsonConvert.SerializeObject(Emenvio)
                    Dim url2 = gb_url + "/api/v2/emision"

                    Dim headers2 = New List(Of Parametro) From {
                    New Parametro("Authorization", "Bearer " + tokenObtenido),
                    New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
                }

                    Dim parametros2 = New List(Of Parametro)
                    Dim response2 = api.Post(url2, headers2, parametros2, Emenvio)
                    Dim result2 = JsonConvert.DeserializeObject(Of RespEmisor)(response2)
                    Dim resultError2 = JsonConvert.DeserializeObject(Of Resp400)(response2)

                    codigo = result2.meta.code
                    Dim xml2 As String
                    If codigo = 200 Then
                        Dim details2 = result2.data.details
                        xml2 = result2.data.dataXml
                        Dim doc As XmlDocument = New XmlDocument()
                        doc.LoadXml(xml2)
                        Dim nodo = doc.DocumentElement("cabecera")
                        gb_cufSifac = nodo.ChildNodes.Item(5).InnerText 'extrae dato CUF de xml sifac

                        NroFact = result2.data.numeroFactura
                        QrUrl = result2.data.qrUrl
                        FactUrl = result2.data.facturaUrl
                        SegundaLeyenda = result2.data.leyenda
                        TerceraLeyenda = result2.data.terceraLeyenda
                        Cudf = result2.data.cufd

                        'Dim notifi = New notifi

                        'notifi.tipo = 2
                        'notifi.Context = "SIFAC".ToUpper
                        'notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details2 & vbCrLf & " " & vbCrLf & "Factura enviada al Siat".ToUpper
                        'notifi.ShowDialog()
                    Else
                        Dim notifi = New notifi

                        notifi.tipo = 2
                        notifi.Context = "ADMINISTRACION".ToUpper
                        notifi.Header = "Ocurrió algún inconveniente, comunicarse con el Administrador del Sistema.".ToUpper
                        notifi.ShowDialog()
                    End If

                Else
                    Dim notifi = New notifi

                    notifi.tipo = 2
                    notifi.Context = "SIFAC".ToUpper
                    notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "La factura no pudo enviarse al Siat".ToUpper
                    notifi.ShowDialog()
                End If

            End If

            Return codigo
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return 0
        End Try
    End Function

    Public Function VerificarNit(tokenObtenido As String, nitCli As String)
        Try

            Dim api = New DBApi()
            Dim nit As String = nitCli.Trim

            Dim url = gb_url + "/api/v2/nit/" + nit + ""

            Dim headers = New List(Of Parametro) From {
                New Parametro("Authorization", "Bearer " + tokenObtenido),
                New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
            }

            Dim parametros = New List(Of Parametro)

            Dim response = api.MGet(url, headers, parametros)

            Dim result = JsonConvert.DeserializeObject(Of RespuestNit)(response)
            Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

            Dim codigo = result.meta.code

            If codigo = 200 Then
                Dim details = result.data.details

                'Dim notifi = New notifi

                'notifi.tipo = 2
                'notifi.Context = "SIFAC".ToUpper
                'notifi.Header = "Proceso Exitoso - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "Validado correctamente".ToUpper
                'notifi.ShowDialog()

            ElseIf codigo = 400 Then

                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & " Verifique".ToUpper
                notifi.ShowDialog()

            ElseIf codigo = 500 Then

                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim siat = JsonConvert.SerializeObject(resultError.errors.siat)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & siat & vbCrLf & " " & vbCrLf & " Verifique".ToUpper
                notifi.ShowDialog()

            ElseIf codigo = 401 Or codigo = 404 Or codigo = 405 Then
                Dim details = JsonConvert.SerializeObject(resultError.errors.details)
                Dim notifi = New notifi

                notifi.tipo = 2
                notifi.Context = "SIFAC".ToUpper
                notifi.Header = "Error de solicitud - Código: " + codigo.ToString() & vbCrLf & " " & vbCrLf & details & vbCrLf & " " & vbCrLf & "No se pudo validar".ToUpper
                notifi.ShowDialog()

            End If

            Return codigo

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return ""
        End Try

    End Function

    Public Function ListarFacturas(tokenObtenido As String, NPventa As String, fechaI As String, fechaF As String) As List(Of ListarFacturas.Data)
        Dim pagina = "1"
        Dim cantpag = "1000"
        Dim api = New DBApi()

        'Dim url = gb_url + "/api/v2/facturas-emitidas/1/471110/1/100"
        Dim url = gb_url + "/api/v2/facturas/" + NPventa + "/471110/" + fechaI + "/" + fechaF + "/1/1000"


        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of RespListarFacturas)(response)
        Dim resultError = JsonConvert.DeserializeObject(Of ListarFacturasError)(response)

        Dim codigo = result.meta.code
        Dim dt = result.data

        Return dt
    End Function
End Class