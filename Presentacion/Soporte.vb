
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports System.Drawing
Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class Soporte
#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Lote As Boolean = False
    Public _modulo As SideNavItem
    Dim Table_producto As DataTable
    Dim FilaSelectLote As DataRow = Nothing
    Public Modificar As Boolean
    Dim RutaGlobal As String = gs_CarpetaRaiz

#End Region
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelInferior.Enabled = False

        Modificar = False
    End Sub

    Private Sub Soporte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub
    Private Sub _IniciarTodo()
        MSuperTabControl.SelectedTabIndex = 0
        btnGrabar.Enabled = False ''Para controlar que no cambie el motivo según lo seleccionado
        _prCargarSoporte()
        _prInhabiliitar()
        _prAsignarPermisos()
        Me.Text = "SOLICITUD  DE  SOPORTE"
    End Sub
    Private Sub _prAsignarPermisos()
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
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbFecha.Value = Date.Now
        tbHora.Clear()
        tbModulo.Clear()
        tbDescripcion.Clear()
        tbEstado.Clear()
    End Sub
    Private Sub _prhabilitar()
        tbModulo.ReadOnly = False
        tbDescripcion.ReadOnly = False
    End Sub
    Private Sub _prInhabiliitar()
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        grSoporte.Enabled = True

        tbCodigo.ReadOnly = True
        tbFecha.IsInputReadOnly = True
        tbHora.ReadOnly = True
        tbModulo.ReadOnly = True
        tbDescripcion.ReadOnly = True
        tbEstado.ReadOnly = True
    End Sub
    Public Function _ValidarCampos() As Boolean

        If (tbModulo.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor debe colocar el nombre del Módulo".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbModulo.Focus()
            Return False
        End If
        If (tbDescripcion.Text = String.Empty) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor debe colocar la descripción".ToUpper, img, 2500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbDescripcion.Focus()
            Return False
        End If

        Return True
    End Function
    Public Sub _GuardarNuevo()

        Dim numi As String = ""
        Dim res As Boolean = L_GrabarSoporte(numi, tbFecha.Value.ToString("yyyy/MM/dd"), gs_NombreBD, gs_empresaDescSistema, tbModulo.Text.Trim, tbDescripcion.Text.Trim)
        If res Then

            _prCargarSoporte()
            _Limpiar()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Soporte ".ToUpper + tbCodigo.Text + " Grabado con éxito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Soporte no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub
    Private Sub _prCargarSoporte()
        Dim dt As New DataTable
        dt = L_CargarSoporte()

        grSoporte.DataSource = dt
        grSoporte.RetrieveStructure()
        grSoporte.AlternatingColors = True

        With grSoporte.RootTable.Columns("sdnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grSoporte.RootTable.Columns("sdnomBD")
            .Visible = False
        End With
        With grSoporte.RootTable.Columns("sdnumiSop")
            .Visible = False
        End With
        With grSoporte.RootTable.Columns("sdnomEmpresa")
            .Visible = False
        End With
        With grSoporte.RootTable.Columns("sdfecha")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
        End With
        With grSoporte.RootTable.Columns("sdhora")
            .Width = 90
            .Caption = "HORA"
            .Visible = True
        End With
        With grSoporte.RootTable.Columns("sdusuario")
            .Visible = False
        End With

        With grSoporte
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With


    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                '_prGuardarModificado()
            End If
        End If
    End Sub

    Private Sub grSoporte_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grSoporte.EditingCell
        e.Cancel = True
    End Sub

    Private Sub grSoporte_KeyDown(sender As Object, e As KeyEventArgs) Handles grSoporte.KeyDown
        If (e.KeyData = Keys.Enter) Then
            MSuperTabControl.SelectedTabIndex = 0
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub grSoporte_SelectionChanged(sender As Object, e As EventArgs) Handles grSoporte.SelectionChanged
        If (grSoporte.RowCount >= 0 And grSoporte.Row >= 0) Then
            _prMostrarRegistro(grSoporte.Row)
        End If
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)

        With grSoporte
            tbCodigo.Text = .GetValue("sdnumi")
            tbFecha.Value = .GetValue("sdfecha")
            tbHora.Text = .GetValue("sdhora")
            tbModulo.Text = .GetValue("sdnomModulo")
            tbDescripcion.Text = .GetValue("sddescrip")
            tbEstado.Text = .GetValue("estado")

            If tbEstado.Text = "SOLICITUD" Then
                tbEstado.BackColor = Color.Red
            ElseIf tbEstado.Text = "REVISIÓN" Then
                tbEstado.BackColor = Color.Orange
            ElseIf tbEstado.Text = "CONCLUIDO" Then
                tbEstado.BackColor = Color.Green
            End If

            lbFecha.Text = CType(.GetValue("sdfecha"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("sdhora").ToString
            lbUsuario.Text = .GetValue("sdusuario").ToString

        End With


        LblPaginacion.Text = Str(grSoporte.Row + 1) + "/" + grSoporte.RowCount.ToString

    End Sub
End Class