Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica
Imports System.Drawing.Printing

Public Class F1_Impresoras

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()

        Me.Text = "Configuraciòn Impresoras ".ToUpper
        CargarNombreImpresoras()

        _PMIniciarTodo()
        _PAsignarPermisos()
    End Sub

    Private Sub _PAsignarPermisos()
        Dim dtRolUsu() As DataRow = L_prRolDetalleGeneral(gi_userRol).Select("yaprog='" + _nameButton + "'")

        Dim show As Boolean = False
        Dim add As Boolean = False
        Dim modif As Boolean = False
        Dim del As Boolean = False

        If (dtRolUsu.Count = 1) Then
            show = dtRolUsu(0).Item("ycshow")
            add = dtRolUsu(0).Item("ycadd")
            modif = dtRolUsu(0).Item("ycmod")
            del = dtRolUsu(0).Item("ycdel")
        End If

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub

    Private Sub CargarNombreImpresoras()
        Dim ImpresorasInstaladas As String
        Dim dt As New DataTable

        dt.Columns.Add("nombre")

        For i As Integer = 0 To PrinterSettings.InstalledPrinters.Count - 1
            ImpresorasInstaladas = PrinterSettings.InstalledPrinters(i)
            dt.Rows.Add(ImpresorasInstaladas)
        Next
        With cbImpresoras
            .DropDownList.Columns.Clear()
            '.DropDownList.Columns.Add("nombre").Width = 60
            '.DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("nombre").Width = 350
            .DropDownList.Columns("nombre").Caption = "Nombre"
            .ValueMember = "nombre"
            .DisplayMember = "nombre"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
#End Region

#Region "METODOS SOBRECARGADOS"
    Public Overrides Sub _PMOHabilitar()
        'tbImpActual.ReadOnly = False
        cbImpresoras.ReadOnly = False
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        tbImpActual.ReadOnly = True
        cbImpresoras.ReadOnly = True
    End Sub



    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean = L_prModificarimpresora(tbCodigo.Text, cbImpresoras.Text)
        If res Then
            ToastNotification.Show(Me, "El nombre de la impresora se modificó con éxito.".ToUpper,
                                   My.Resources.GRABACION_EXITOSA, 4000, eToastGlowColor.Green,
                                   eToastPosition.TopCenter)
            _PSalirRegistro()
        End If
        Return res
    End Function


    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        'If tbImpActual.Text = String.Empty Then
        '    tbImpActual.BackColor = Color.Red
        '    MEP.SetError(tbImpActual, "ingrese la descripcion de la sucursal!".ToUpper)
        '    _ok = False
        'Else
        '    tbImpActual.BackColor = Color.White
        '    MEP.SetError(tbImpActual, "")
        'End If
        If cbImpresoras.SelectedIndex = -1 Or cbImpresoras.Text = String.Empty Then
            cbImpresoras.BackColor = Color.Red
            MEP.SetError(cbImpresoras, "Debe seleccionar una impresora".ToUpper)
            _ok = False
        Else
            cbImpresoras.BackColor = Color.White
            MEP.SetError(cbImpresoras, "")
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prImpresoras()
        Return dtBuscador
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)

        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("cbnumi", True, "COD", 90))
        listEstCeldas.Add(New Modelo.Celda("cbrut", True, "NOMBRE IMPRESORA", 300))
        listEstCeldas.Add(New Modelo.Celda("cbcod", False))
        listEstCeldas.Add(New Modelo.Celda("cbdesc", False))
        listEstCeldas.Add(New Modelo.Celda("cbvp", False))
        listEstCeldas.Add(New Modelo.Celda("cbfact", False))
        listEstCeldas.Add(New Modelo.Celda("cbhact", False))
        listEstCeldas.Add(New Modelo.Celda("cbuact", False))
        listEstCeldas.Add(New Modelo.Celda("cbnrocopias", False))

        Return listEstCeldas
    End Function

    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        With JGrM_Buscador
            tbCodigo.Text = .GetValue("cbnumi").ToString
            tbImpActual.Text = .GetValue("cbrut").ToString

            lbFecha.Text = CType(.GetValue("cbfact").ToString, Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("cbhact").ToString
            lbUsuario.Text = .GetValue("cbuact").ToString

        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub


    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()
        Else
            _modulo.Select()
            Close()
        End If
    End Sub
#End Region


    Private Sub F1_Almacen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbImpActual.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        cbImpresoras.Focus()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

End Class