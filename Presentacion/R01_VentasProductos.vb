Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls

Public Class R01_VentasProductos
    Dim _inter As Integer = 0
#Region "VARIABLES GLOBALES"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim titulo As String = ""
    Public _modulo As SideNavItem

    Private idProveedor As Integer = 0
    Private idProducto As Integer = 0

#End Region
#Region "METODOS PRIVADOS"
    Public Sub _prIniciarTodo()
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        'If (Not gb_ConexionAbierta) Then
        '    L_prAbrirConexion()
        'End If
        'Me.WindowState = FormWindowState.Maximized
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = "REPORTE PRODUCTOS EN VENTAS"
        P_prArmarComboProducto()
        _IniciarComponentes()

        MCrReporte.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub
    Private Sub P_prArmarComboProducto()
        Dim DtP As DataTable
        DtP = L_fnObtenerProductos()

        g_prArmarCombo(cbProducto, DtP, 60, 200, "COD", "PRODUCTOS")
        cbProducto.SelectedIndex = Convert.ToInt32(DtP.Rows.Count - 1)

    End Sub
    Public Sub _IniciarComponentes()

        tbAlmacen.ReadOnly = True
        tbAlmacen.Enabled = False
        CheckTodosAlmacen.CheckValue = True
        tbProveedor.ReadOnly = True
        tbProveedor.Enabled = False
        ckTodosProveedor.CheckValue = True
        CheckTodosProducto.Checked = True

    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        '_dt = L_prReporteVentasProductos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        Dim fechaDesde As DateTime = tbFechaI.Value.ToString("yyyy/MM/dd")
        Dim fechaHasta As DateTime = tbFechaF.Value.ToString("yyyy/MM/dd")
        Dim idAlmacen As Integer = 0

        If tbAlmacen.SelectedIndex <> 0 Then idAlmacen = tbAlmacen.Value
        If cbProducto.SelectedIndex <> -1 Then idProducto = cbProducto.Value
        If tbCodigoProveedor.Text <> String.Empty Then idProveedor = tbCodigoProveedor.Text

        _dt = L_prReporteVentasProductos(fechaDesde, fechaHasta, idAlmacen, idProveedor, idproducto)


        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_VentasProductos
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)

            MCrReporte.ReportSource = objrep
            MCrReporte.Show()
            MCrReporte.BringToFront()



        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MCrReporte.ReportSource = Nothing
        End If





    End Sub

    Private Sub MBtGenerar_Click(sender As Object, e As EventArgs) Handles MBtGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub R01_VentasAtendidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub
#End Region



    Private Sub MBtSalir_Click(sender As Object, e As EventArgs) Handles MBtSalir.Click
        '_tab.Close()
        _modulo.Select()
        Me.Close()
    End Sub
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

    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckValueChanged
        If (CheckUnaALmacen.Checked) Then
            CheckTodosAlmacen.CheckValue = False
            tbAlmacen.Enabled = True
            tbAlmacen.BackColor = Color.White
            tbAlmacen.Focus()
            tbAlmacen.ReadOnly = False
            R01_ReporteVentasVendedores._prCargarComboLibreriaSucursal(tbAlmacen)
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
            R01_ReporteVentasVendedores._prCargarComboLibreriaSucursal(tbAlmacen)
            CType(tbAlmacen.DataSource, DataTable).Rows.Clear()
            tbAlmacen.SelectedIndex = -1
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

    Private Sub CheckUnaProducto_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaProducto.CheckValueChanged
        If (CheckUnaProducto.Checked) Then
            CheckTodosProducto.CheckValue = False
            cbProducto.Enabled = True
            cbProducto.BackColor = Color.White
            cbProducto.Focus()
            cbProducto.ReadOnly = False
            If (CType(cbProducto.DataSource, DataTable).Rows.Count > 0) Then
                cbProducto.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub CheckTodosProducto_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosProducto.CheckValueChanged
        If (CheckTodosProducto.Checked) Then
            CheckUnaProducto.CheckValue = False
            cbProducto.Enabled = True
            cbProducto.BackColor = Color.Gainsboro
            cbProducto.ReadOnly = True
            cbProducto.SelectedIndex = -1
            idProducto = 0
        End If
    End Sub
End Class