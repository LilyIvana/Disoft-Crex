<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class R01_ReporteVentasVendedores
    Inherits Modelo.ModeloR01


    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim tbAlmacen_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(R01_ReporteVentasVendedores))
        Me.swFiltroVendedores = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.swProducto = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbVendedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.CheckTodosVendedor = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.checkUnVendedor = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbCodigoVendedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.tbProveedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.ckTodosProveedor = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.ckUnoProveedor = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbCodigoProveedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbAlmacen = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.CheckTodosAlmacen = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckUnaALmacen = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.tbCodAlmacen = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaF = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaI = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.MPnSuperior.SuspendLayout()
        Me.MPnInferior.SuspendLayout()
        Me.MPanelToolBarUsuario.SuspendLayout()
        Me.MPanelToolBarAccion.SuspendLayout()
        CType(Me.MBubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MPnUsuario.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MGpFiltro.SuspendLayout()
        CType(Me.tbAlmacen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MPnSuperior
        '
        Me.MPnSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.MPnSuperior.Style.BackColor1.Color = System.Drawing.Color.Gray
        Me.MPnSuperior.Style.BackColor2.Color = System.Drawing.SystemColors.ActiveCaption
        Me.MPnSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.MPnSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MPnSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MPnSuperior.Style.GradientAngle = 90
        '
        'MPnInferior
        '
        Me.MPnInferior.Location = New System.Drawing.Point(300, 655)
        Me.MPnInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.MPnInferior.Style.BackColor1.Color = System.Drawing.Color.Gray
        Me.MPnInferior.Style.BackColor2.Color = System.Drawing.SystemColors.ActiveCaption
        Me.MPnInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.MPnInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MPnInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MPnInferior.Style.GradientAngle = 90
        '
        'MTbUsuario
        '
        Me.MTbUsuario.ReadOnly = True
        Me.MTbUsuario.Text = "DEFAULT"
        '
        'MBtGenerar
        '
        '
        'MBubbleBarUsuario
        '
        '
        '
        '
        Me.MBubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.MBubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.MBubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.MBubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'MGpFiltro
        '
        Me.MGpFiltro.Controls.Add(Me.swFiltroVendedores)
        Me.MGpFiltro.Controls.Add(Me.swProducto)
        Me.MGpFiltro.Controls.Add(Me.LabelX2)
        Me.MGpFiltro.Controls.Add(Me.tbVendedor)
        Me.MGpFiltro.Controls.Add(Me.CheckTodosVendedor)
        Me.MGpFiltro.Controls.Add(Me.checkUnVendedor)
        Me.MGpFiltro.Controls.Add(Me.tbCodigoVendedor)
        Me.MGpFiltro.Controls.Add(Me.LabelX6)
        Me.MGpFiltro.Controls.Add(Me.tbProveedor)
        Me.MGpFiltro.Controls.Add(Me.ckTodosProveedor)
        Me.MGpFiltro.Controls.Add(Me.ckUnoProveedor)
        Me.MGpFiltro.Controls.Add(Me.tbCodigoProveedor)
        Me.MGpFiltro.Controls.Add(Me.LabelX5)
        Me.MGpFiltro.Controls.Add(Me.tbAlmacen)
        Me.MGpFiltro.Controls.Add(Me.CheckTodosAlmacen)
        Me.MGpFiltro.Controls.Add(Me.CheckUnaALmacen)
        Me.MGpFiltro.Controls.Add(Me.tbCodAlmacen)
        Me.MGpFiltro.Controls.Add(Me.LabelX3)
        Me.MGpFiltro.Controls.Add(Me.LabelX1)
        Me.MGpFiltro.Controls.Add(Me.tbFechaF)
        Me.MGpFiltro.Controls.Add(Me.LabelX4)
        Me.MGpFiltro.Controls.Add(Me.tbFechaI)
        Me.MGpFiltro.Size = New System.Drawing.Size(300, 621)
        '
        '
        '
        Me.MGpFiltro.Style.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.MGpFiltro.Style.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.MGpFiltro.Style.BackColorGradientAngle = 90
        Me.MGpFiltro.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGpFiltro.Style.BorderBottomWidth = 1
        Me.MGpFiltro.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MGpFiltro.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGpFiltro.Style.BorderLeftWidth = 1
        Me.MGpFiltro.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGpFiltro.Style.BorderRightWidth = 1
        Me.MGpFiltro.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGpFiltro.Style.BorderTopWidth = 1
        Me.MGpFiltro.Style.CornerDiameter = 4
        Me.MGpFiltro.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.MGpFiltro.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.MGpFiltro.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MGpFiltro.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.MGpFiltro.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.MGpFiltro.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbFechaI, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.LabelX4, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbFechaF, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.LabelX1, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.LabelX3, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbCodAlmacen, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.CheckUnaALmacen, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.CheckTodosAlmacen, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbAlmacen, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.LabelX5, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbCodigoProveedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.ckUnoProveedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.ckTodosProveedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbProveedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.LabelX6, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbCodigoVendedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.checkUnVendedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.CheckTodosVendedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.tbVendedor, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.LabelX2, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.swProducto, 0)
        Me.MGpFiltro.Controls.SetChildIndex(Me.swFiltroVendedores, 0)
        '
        'MCrReporte
        '
        Me.MCrReporte.Size = New System.Drawing.Size(984, 691)
        '
        'swFiltroVendedores
        '
        '
        '
        '
        Me.swFiltroVendedores.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swFiltroVendedores.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swFiltroVendedores.Location = New System.Drawing.Point(56, 8)
        Me.swFiltroVendedores.Name = "swFiltroVendedores"
        Me.swFiltroVendedores.OffBackColor = System.Drawing.Color.DarkSlateGray
        Me.swFiltroVendedores.OffText = "SIN VENDEDORES"
        Me.swFiltroVendedores.OffTextColor = System.Drawing.Color.White
        Me.swFiltroVendedores.OnBackColor = System.Drawing.Color.Gold
        Me.swFiltroVendedores.OnText = "CON VENDEDORES"
        Me.swFiltroVendedores.Size = New System.Drawing.Size(170, 22)
        Me.swFiltroVendedores.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swFiltroVendedores.TabIndex = 282
        Me.swFiltroVendedores.Value = True
        Me.swFiltroVendedores.ValueObject = "Y"
        '
        'swProducto
        '
        '
        '
        '
        Me.swProducto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swProducto.Location = New System.Drawing.Point(138, 116)
        Me.swProducto.Name = "swProducto"
        Me.swProducto.OffBackColor = System.Drawing.Color.DarkSlateGray
        Me.swProducto.OffText = "NO"
        Me.swProducto.OffTextColor = System.Drawing.Color.White
        Me.swProducto.OnBackColor = System.Drawing.Color.Gold
        Me.swProducto.OnText = "Si"
        Me.swProducto.Size = New System.Drawing.Size(120, 22)
        Me.swProducto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swProducto.TabIndex = 281
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(3, 116)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(124, 23)
        Me.LabelX2.TabIndex = 280
        Me.LabelX2.Text = "Mostrar Productos:"
        '
        'tbVendedor
        '
        Me.tbVendedor.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.tbVendedor.Border.Class = "TextBoxBorder"
        Me.tbVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbVendedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbVendedor.Location = New System.Drawing.Point(5, 178)
        Me.tbVendedor.Name = "tbVendedor"
        Me.tbVendedor.PreventEnterBeep = True
        Me.tbVendedor.Size = New System.Drawing.Size(167, 22)
        Me.tbVendedor.TabIndex = 279
        '
        'CheckTodosVendedor
        '
        '
        '
        '
        Me.CheckTodosVendedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosVendedor.Location = New System.Drawing.Point(229, 177)
        Me.CheckTodosVendedor.Name = "CheckTodosVendedor"
        Me.CheckTodosVendedor.Size = New System.Drawing.Size(55, 23)
        Me.CheckTodosVendedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodosVendedor.TabIndex = 278
        Me.CheckTodosVendedor.Text = "Todos"
        '
        'checkUnVendedor
        '
        '
        '
        '
        Me.checkUnVendedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkUnVendedor.Location = New System.Drawing.Point(179, 177)
        Me.checkUnVendedor.Name = "checkUnVendedor"
        Me.checkUnVendedor.Size = New System.Drawing.Size(44, 23)
        Me.checkUnVendedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkUnVendedor.TabIndex = 275
        Me.checkUnVendedor.Text = "Una"
        '
        'tbCodigoVendedor
        '
        '
        '
        '
        Me.tbCodigoVendedor.Border.Class = "TextBoxBorder"
        Me.tbCodigoVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigoVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigoVendedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodigoVendedor.Location = New System.Drawing.Point(83, 150)
        Me.tbCodigoVendedor.Name = "tbCodigoVendedor"
        Me.tbCodigoVendedor.PreventEnterBeep = True
        Me.tbCodigoVendedor.Size = New System.Drawing.Size(22, 22)
        Me.tbCodigoVendedor.TabIndex = 277
        Me.tbCodigoVendedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCodigoVendedor.Visible = False
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX6.Location = New System.Drawing.Point(3, 151)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(74, 23)
        Me.LabelX6.TabIndex = 276
        Me.LabelX6.Text = "Vendedor:"
        '
        'tbProveedor
        '
        Me.tbProveedor.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.tbProveedor.Border.Class = "TextBoxBorder"
        Me.tbProveedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProveedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbProveedor.Location = New System.Drawing.Point(4, 234)
        Me.tbProveedor.Name = "tbProveedor"
        Me.tbProveedor.PreventEnterBeep = True
        Me.tbProveedor.Size = New System.Drawing.Size(167, 22)
        Me.tbProveedor.TabIndex = 274
        '
        'ckTodosProveedor
        '
        '
        '
        '
        Me.ckTodosProveedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ckTodosProveedor.Location = New System.Drawing.Point(228, 233)
        Me.ckTodosProveedor.Name = "ckTodosProveedor"
        Me.ckTodosProveedor.Size = New System.Drawing.Size(55, 23)
        Me.ckTodosProveedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ckTodosProveedor.TabIndex = 273
        Me.ckTodosProveedor.Text = "Todos"
        '
        'ckUnoProveedor
        '
        '
        '
        '
        Me.ckUnoProveedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ckUnoProveedor.Location = New System.Drawing.Point(178, 233)
        Me.ckUnoProveedor.Name = "ckUnoProveedor"
        Me.ckUnoProveedor.Size = New System.Drawing.Size(44, 23)
        Me.ckUnoProveedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ckUnoProveedor.TabIndex = 270
        Me.ckUnoProveedor.Text = "Una"
        '
        'tbCodigoProveedor
        '
        '
        '
        '
        Me.tbCodigoProveedor.Border.Class = "TextBoxBorder"
        Me.tbCodigoProveedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigoProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigoProveedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodigoProveedor.Location = New System.Drawing.Point(85, 206)
        Me.tbCodigoProveedor.Name = "tbCodigoProveedor"
        Me.tbCodigoProveedor.PreventEnterBeep = True
        Me.tbCodigoProveedor.Size = New System.Drawing.Size(22, 22)
        Me.tbCodigoProveedor.TabIndex = 272
        Me.tbCodigoProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCodigoProveedor.Visible = False
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX5.Location = New System.Drawing.Point(3, 206)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(74, 23)
        Me.LabelX5.TabIndex = 271
        Me.LabelX5.Text = "Proveedor:"
        '
        'tbAlmacen
        '
        Me.tbAlmacen.BackColor = System.Drawing.Color.Gainsboro
        tbAlmacen_DesignTimeLayout.LayoutString = resources.GetString("tbAlmacen_DesignTimeLayout.LayoutString")
        Me.tbAlmacen.DesignTimeLayout = tbAlmacen_DesignTimeLayout
        Me.tbAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAlmacen.Location = New System.Drawing.Point(5, 296)
        Me.tbAlmacen.Name = "tbAlmacen"
        Me.tbAlmacen.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.tbAlmacen.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.tbAlmacen.SelectedIndex = -1
        Me.tbAlmacen.SelectedItem = Nothing
        Me.tbAlmacen.Size = New System.Drawing.Size(164, 22)
        Me.tbAlmacen.TabIndex = 269
        Me.tbAlmacen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'CheckTodosAlmacen
        '
        '
        '
        '
        Me.CheckTodosAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosAlmacen.Location = New System.Drawing.Point(227, 296)
        Me.CheckTodosAlmacen.Name = "CheckTodosAlmacen"
        Me.CheckTodosAlmacen.Size = New System.Drawing.Size(55, 23)
        Me.CheckTodosAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodosAlmacen.TabIndex = 268
        Me.CheckTodosAlmacen.Text = "Todos"
        '
        'CheckUnaALmacen
        '
        '
        '
        '
        Me.CheckUnaALmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnaALmacen.Location = New System.Drawing.Point(177, 296)
        Me.CheckUnaALmacen.Name = "CheckUnaALmacen"
        Me.CheckUnaALmacen.Size = New System.Drawing.Size(44, 23)
        Me.CheckUnaALmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnaALmacen.TabIndex = 265
        Me.CheckUnaALmacen.Text = "Una"
        '
        'tbCodAlmacen
        '
        '
        '
        '
        Me.tbCodAlmacen.Border.Class = "TextBoxBorder"
        Me.tbCodAlmacen.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodAlmacen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodAlmacen.Location = New System.Drawing.Point(85, 264)
        Me.tbCodAlmacen.Name = "tbCodAlmacen"
        Me.tbCodAlmacen.PreventEnterBeep = True
        Me.tbCodAlmacen.Size = New System.Drawing.Size(22, 22)
        Me.tbCodAlmacen.TabIndex = 267
        Me.tbCodAlmacen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tbCodAlmacen.Visible = False
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(5, 263)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(74, 23)
        Me.LabelX3.TabIndex = 266
        Me.LabelX3.Text = "Almacen:"
        Me.LabelX3.Visible = False
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(7, 74)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(52, 23)
        Me.LabelX1.TabIndex = 264
        Me.LabelX1.Text = "Al:"
        '
        'tbFechaF
        '
        '
        '
        '
        Me.tbFechaF.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaF.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaF.ButtonDropDown.Visible = True
        Me.tbFechaF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaF.IsPopupCalendarOpen = False
        Me.tbFechaF.Location = New System.Drawing.Point(83, 74)
        '
        '
        '
        '
        '
        '
        Me.tbFechaF.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaF.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaF.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaF.Name = "tbFechaF"
        Me.tbFechaF.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaF.TabIndex = 263
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX4.Location = New System.Drawing.Point(4, 42)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(74, 23)
        Me.LabelX4.TabIndex = 262
        Me.LabelX4.Text = "Fecha Del:"
        '
        'tbFechaI
        '
        '
        '
        '
        Me.tbFechaI.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaI.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaI.ButtonDropDown.Visible = True
        Me.tbFechaI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaI.IsPopupCalendarOpen = False
        Me.tbFechaI.Location = New System.Drawing.Point(83, 43)
        '
        '
        '
        '
        '
        '
        Me.tbFechaI.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaI.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaI.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaI.Name = "tbFechaI"
        Me.tbFechaI.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaI.TabIndex = 261
        '
        'R01_ReporteVentasVendedores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 691)
        Me.Name = "R01_ReporteVentasVendedores"
        Me.Text = "R01_ReporteVentasVendedores"
        Me.Controls.SetChildIndex(Me.MPnInferior, 0)
        Me.Controls.SetChildIndex(Me.MPnUsuario, 0)
        Me.MPnSuperior.ResumeLayout(False)
        Me.MPnInferior.ResumeLayout(False)
        Me.MPanelToolBarUsuario.ResumeLayout(False)
        Me.MPanelToolBarUsuario.PerformLayout()
        Me.MPanelToolBarAccion.ResumeLayout(False)
        CType(Me.MBubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MPnUsuario.ResumeLayout(False)
        Me.MPnUsuario.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MGpFiltro.ResumeLayout(False)
        Me.MGpFiltro.PerformLayout()
        CType(Me.tbAlmacen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents swFiltroVendedores As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents swProducto As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbVendedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents CheckTodosVendedor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents checkUnVendedor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbCodigoVendedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbProveedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents ckTodosProveedor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents ckUnoProveedor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbCodigoProveedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbAlmacen As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents CheckTodosAlmacen As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckUnaALmacen As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents tbCodAlmacen As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaF As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaI As DevComponents.Editors.DateTimeAdv.DateTimeInput
End Class
