Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class R01_ReporteVentasVendedores

    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date

        Me.Text = "REPORTE VENTAS VENDEDORES-PROVEEDORES"
        MCrReporte.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
    End Sub

    Public Sub _IniciarComponentes()

        tbAlmacen.ReadOnly = True
        tbAlmacen.Enabled = False
        CheckTodosAlmacen.CheckValue = True
        tbProveedor.ReadOnly = True
        tbProveedor.Enabled = False
        ckTodosProveedor.CheckValue = True
        tbVendedor.ReadOnly = True
        tbVendedor.Enabled = False
        CheckTodosVendedor.CheckValue = True

    End Sub
    Private Sub MBtGenerar_Click(sender As Object, e As EventArgs) Handles MBtGenerar.Click
        _prCargarReporte()
    End Sub
    Private Sub _prCargarReporte()
        Dim _ventasAtendidas As New DataTable
        _ventasAtendidas = _prValidadrFiltros()
        If (_ventasAtendidas.Rows.Count > 0) Then
            If swFiltroVendedores.Value = True Then
                If (swProducto.Value = True) Then
                    Dim objrep As New R_VentasVendProvProd
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MCrReporte.ReportSource = objrep
                    MCrReporte.Show()
                    MCrReporte.BringToFront()
                Else
                    Dim objrep As New R_VentasVendProv
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MCrReporte.ReportSource = objrep
                    MCrReporte.Show()
                    MCrReporte.BringToFront()
                End If
            Else
                If (swProducto.Value = True) Then
                    Dim objrep As New R_VentasProvProdSinVendedor
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MCrReporte.ReportSource = objrep
                    MCrReporte.Show()
                    MCrReporte.BringToFront()
                Else
                    Dim objrep As New R_VentasProvSinVendedor
                    objrep.SetDataSource(_ventasAtendidas)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    objrep.SetParameterValue("usuario", L_Usuario)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    MCrReporte.ReportSource = objrep
                    MCrReporte.Show()
                    MCrReporte.BringToFront()
                End If
            End If


        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MCrReporte.ReportSource = Nothing
        End If
    End Sub
    Public Function _prValidadrFiltros() As DataTable

        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim idVendedor As Integer = 0
        Dim idProveedor As Integer = 0
        Dim idAlmacen As Integer = 0
        Dim ventasAtendidas As DataTable

        If CheckTodosAlmacen.Checked = False And CheckUnaALmacen.Checked = True And tbCodAlmacen.Text <> String.Empty Then
            idAlmacen = tbCodAlmacen.Text
        End If
        If CheckTodosVendedor.Checked = False And checkUnVendedor.Checked = True And tbVendedor.Text <> String.Empty Then
            idVendedor = tbCodigoVendedor.Text
        End If
        If ckTodosProveedor.Checked = False And ckUnoProveedor.Checked = True And tbCodigoProveedor.Text <> String.Empty Then
            idProveedor = tbCodigoProveedor.Text
        End If

        If swFiltroVendedores.Value = True Then
            If swProducto.Value = True Then
                ventasAtendidas = L_BuscarVentasVendedorProveedoresProductos(fechaDesde, fechaHasta, idAlmacen, idVendedor, idProveedor)
            Else
                ventasAtendidas = L_BuscarVentasVendedoresProveedores(fechaDesde, fechaHasta, idAlmacen, idVendedor, idProveedor)
            End If
        Else
            If swProducto.Value = True Then
                ventasAtendidas = L_BuscarVentasProveedoresProductosSinVendedor(fechaDesde, fechaHasta, idAlmacen, idProveedor)
            Else
                ventasAtendidas = L_BuscarVentasProveedoresSinVendedor(fechaDesde, fechaHasta, idAlmacen, idProveedor)
            End If
        End If


        Return ventasAtendidas

    End Function

    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown
        If (checkUnVendedor.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnObtenerPersonal(3)

                Dim listEstCeldas As New List(Of Modelo.MCelda)
                listEstCeldas.Add(New Modelo.MCelda("cod,", True, "CÓDIGO", 80))
                listEstCeldas.Add(New Modelo.MCelda("desc", True, "VENDEDOR", 180))


                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 250
                ef.Context = "Seleccione Vendedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbVendedor.Focus()
                        Return
                    End If
                    tbCodigoVendedor.Text = Row.Cells("cod").Value
                    tbVendedor.Text = Row.Cells("desc").Value
                    MBtGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub tbProveedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProveedor.KeyDown
        If (ckUnoProveedor.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                dt = L_fnObtenerProveedor()

                Dim listEstCeldas As New List(Of Modelo.MCelda)
                listEstCeldas.Add(New Modelo.MCelda("cod,", True, "CÓDIGO", 70))
                listEstCeldas.Add(New Modelo.MCelda("desc", True, "PROVEEDOR", 200))

                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 2
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Proveedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    tbCodigoProveedor.Text = Row.Cells("cod").Value
                    tbProveedor.Text = Row.Cells("desc").Value
                End If
            End If
        End If
    End Sub

    Private Sub checkUnVendedor_CheckValueChanged(sender As Object, e As EventArgs) Handles checkUnVendedor.CheckValueChanged
        If (checkUnVendedor.Checked) Then
            CheckTodosVendedor.CheckValue = False
            tbVendedor.Enabled = True
            tbVendedor.BackColor = Color.White
            tbVendedor.Focus()
        End If
    End Sub

    Private Sub CheckTodosVendedor_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosVendedor.CheckValueChanged
        If (CheckTodosVendedor.Checked) Then
            checkUnVendedor.CheckValue = False
            tbVendedor.Enabled = True
            tbVendedor.BackColor = Color.Gainsboro
            tbVendedor.Clear()
            tbCodigoVendedor.Clear()
        End If
    End Sub

    Private Sub ckUnoProveedor_CheckValueChanged(sender As Object, e As EventArgs) Handles ckUnoProveedor.CheckValueChanged
        If (ckUnoProveedor.Checked) Then
            ckTodosProveedor.CheckValue = False
            tbProveedor.Enabled = True
            tbProveedor.BackColor = Color.White
            tbProveedor.Focus()
        End If
    End Sub

    Private Sub ckTodosProveedor_CheckValueChanged(sender As Object, e As EventArgs) Handles ckTodosProveedor.CheckValueChanged
        If (ckTodosProveedor.Checked) Then
            ckUnoProveedor.CheckValue = False
            tbProveedor.Enabled = True
            tbProveedor.BackColor = Color.Gainsboro
            tbProveedor.Clear()
            tbCodigoProveedor.Clear()
        End If
    End Sub

    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckValueChanged
        If (CheckUnaALmacen.Checked) Then
            CheckTodosAlmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.White
            tbAlmacen.Focus()
            tbAlmacen.ReadOnly = False
            _prCargarComboLibreriaSucursal(tbAlmacen)
            If (CType(tbAlmacen.DataSource, DataTable).Rows.Count > 0) Then
                tbAlmacen.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckValueChanged
        If (CheckTodosAlmacen.Checked) Then
            CheckUnaALmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.Gainsboro
            tbAlmacen.ReadOnly = True
            _prCargarComboLibreriaSucursal(tbAlmacen)
            CType(tbAlmacen.DataSource, DataTable).Rows.Clear()
            tbAlmacen.SelectedIndex = -1
        End If
    End Sub

    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnMovimientoListarSucursales()

        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "CÓDIGO"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub R01_ReporteVentasVendedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub
End Class