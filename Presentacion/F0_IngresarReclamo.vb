Imports Logica.AccesoLogica

'importando librerias api conexion
Imports Newtonsoft.Json
Imports Presentacion.RespMotivoAnulacion
Imports Presentacion.AnulacionResp

Public Class F0_IngresarReclamo
#Region "Atributos prinvados"
    Private numiPedido As String
    Private estadoPedido As String
    Private tipoReclamo As String
    Public respuesta As Boolean

    'Sifac
    Public token As String
#End Region

#Region "metodos privados"
    Public Sub New(_numiPedido As String, _estadoPedido As String, _tipoReclamo As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        numiPedido = _numiPedido
        estadoPedido = _estadoPedido
        tipoReclamo = _tipoReclamo
        respuesta = False
    End Sub
    Private Sub _prIniciarTodo()
        _PCargarComboLibreria(tbConcep, gi_ConceptoReclamo)

        token = frmBillingDispatch.ObtToken()
        CodMotivoAnulacion(token)
    End Sub

    Private Sub _PCargarComboLibreria(ByVal cb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal concep As Integer)
        Dim _Ds As New DataSet
        _Ds = L_General_LibreriaDetalle(-1, concep)

        cb.DropDownList.Columns.Clear()

        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("ceid").ToString).Width = 50
        cb.DropDownList.Columns(0).Caption = "Código"
        cb.DropDownList.Columns.Add(_Ds.Tables(0).Columns("cedesc").ToString).Width = 250
        cb.DropDownList.Columns(1).Caption = "Descripcion"

        cb.ValueMember = _Ds.Tables(0).Columns("ceid").ToString
        cb.DisplayMember = _Ds.Tables(0).Columns("cedesc").ToString
        cb.DataSource = _Ds.Tables(0)
        cb.Refresh()

        If _Ds.Tables(0).Rows.Count > 0 Then
            cb.SelectedIndex = 0
        End If

    End Sub

    Private Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        If tbObs.Text = String.Empty Then
            tbObs.BackColor = Color.Red
            MEP.SetError(tbObs, "ingrese la observacion de la anulación!".ToUpper)
            _ok = False
        Else
            tbObs.BackColor = Color.White
            MEP.SetError(tbObs, "")
        End If

        If tbConcep.SelectedIndex < 0 Then
            tbConcep.BackColor = Color.Red
            MEP.SetError(tbConcep, "seleccione el concepto de la anulación!".ToUpper)
            _ok = False
        Else
            tbConcep.BackColor = Color.White
            MEP.SetError(tbConcep, "")
        End If

        If CbMotivoA.SelectedIndex < 0 Then
            tbConcep.BackColor = Color.Red
            MEP.SetError(CbMotivoA, "seleccione el concepto de la anulación!".ToUpper)
            _ok = False
        Else
            CbMotivoA.BackColor = Color.White
            MEP.SetError(CbMotivoA, "")
        End If


        Return _ok
    End Function
#End Region



    Private Sub ModeloHor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            P_Moverenfoque()
        End If
    End Sub

    Private Sub P_Moverenfoque()
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If _PMOValidarCampos() = True Then
            Dim numi As String = ""
            L_PedidoReclamos_Grabar(numi, numiPedido, estadoPedido, tbObs.Text, tipoReclamo, tbConcep.Value)
            respuesta = True

            Me.Close()
        End If
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub

    Private Sub F0_IngresarReclamo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Public Function CodMotivoAnulacion(tokenObtenido)

        Dim api = New DBApi()

        Dim url = "https://www.pilotocrex.sifac.nwc.com.bo/api/v2/motivo-anulacion"

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Bearer " + tokenObtenido),
            New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros)

        Dim result = JsonConvert.DeserializeObject(Of Motivo)(response)

        Dim dt = result.data
        dt.RemoveAt(1)

        With CbMotivoA

            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("codigoClasificador").Width = 70
            .DropDownList.Columns("codigoClasificador").Caption = "COD"
            .DropDownList.Columns.Add("descripcion").Width = 500
            .DropDownList.Columns("descripcion").Caption = "DESCRIPCION"
            .ValueMember = "codigoClasificador"
            .DisplayMember = "descripcion"
            .DataSource = dt
            .Refresh()
        End With

        CbMotivoA.SelectedIndex = 0

        Return ""
    End Function
End Class