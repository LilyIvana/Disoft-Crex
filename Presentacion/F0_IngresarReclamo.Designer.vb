<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F0_IngresarReclamo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim tbConcep_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim CbMotivoA_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_IngresarReclamo))
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnsalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnguardar = New DevComponents.DotNetBar.ButtonX()
        Me.tbConcep = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbObs = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.MEP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.CbMotivoA = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel2.SuspendLayout()
        CType(Me.tbConcep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CbMotivoA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.LabelX2)
        Me.GroupPanel2.Controls.Add(Me.CbMotivoA)
        Me.GroupPanel2.Controls.Add(Me.btnsalir)
        Me.GroupPanel2.Controls.Add(Me.btnguardar)
        Me.GroupPanel2.Controls.Add(Me.tbConcep)
        Me.GroupPanel2.Controls.Add(Me.tbObs)
        Me.GroupPanel2.Controls.Add(Me.LabelX13)
        Me.GroupPanel2.Controls.Add(Me.LabelX1)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(547, 235)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 1
        Me.GroupPanel2.Text = "INGRESE EL RECLAMO QUE SE DESEA REGISTRAR"
        '
        'btnsalir
        '
        Me.btnsalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnsalir.BackColor = System.Drawing.Color.Transparent
        Me.btnsalir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.Image = Global.Presentacion.My.Resources.Resources.atras
        Me.btnsalir.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnsalir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnsalir.Location = New System.Drawing.Point(276, 158)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnsalir.Size = New System.Drawing.Size(101, 42)
        Me.btnsalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnsalir.TabIndex = 3
        Me.btnsalir.Text = "SALIR"
        Me.btnsalir.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'btnguardar
        '
        Me.btnguardar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnguardar.BackColor = System.Drawing.Color.Transparent
        Me.btnguardar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnguardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnguardar.Image = Global.Presentacion.My.Resources.Resources.save
        Me.btnguardar.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnguardar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnguardar.Location = New System.Drawing.Point(154, 158)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnguardar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnguardar.Size = New System.Drawing.Size(101, 42)
        Me.btnguardar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnguardar.TabIndex = 2
        Me.btnguardar.Text = "GRABAR"
        Me.btnguardar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'tbConcep
        '
        tbConcep_DesignTimeLayout.LayoutString = resources.GetString("tbConcep_DesignTimeLayout.LayoutString")
        Me.tbConcep.DesignTimeLayout = tbConcep_DesignTimeLayout
        Me.tbConcep.Location = New System.Drawing.Point(154, 16)
        Me.tbConcep.Name = "tbConcep"
        Me.tbConcep.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.tbConcep.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.tbConcep.SelectedIndex = -1
        Me.tbConcep.SelectedItem = Nothing
        Me.tbConcep.Size = New System.Drawing.Size(278, 22)
        Me.tbConcep.TabIndex = 0
        '
        'tbObs
        '
        '
        '
        '
        Me.tbObs.Border.Class = "TextBoxBorder"
        Me.tbObs.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObs.Location = New System.Drawing.Point(154, 76)
        Me.tbObs.MaxLength = 60
        Me.tbObs.Multiline = True
        Me.tbObs.Name = "tbObs"
        Me.tbObs.PreventEnterBeep = True
        Me.tbObs.Size = New System.Drawing.Size(378, 62)
        Me.tbObs.TabIndex = 1
        '
        'LabelX13
        '
        Me.LabelX13.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.Location = New System.Drawing.Point(9, 76)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(106, 23)
        Me.LabelX13.TabIndex = 45
        Me.LabelX13.Text = "OBSERVACION:"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(9, 15)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(98, 23)
        Me.LabelX1.TabIndex = 34
        Me.LabelX1.Text = "CONCEPTO:"
        '
        'MEP
        '
        Me.MEP.ContainerControl = Me
        '
        'CbMotivoA
        '
        Me.CbMotivoA.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.CbMotivoA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        CbMotivoA_DesignTimeLayout.LayoutString = resources.GetString("CbMotivoA_DesignTimeLayout.LayoutString")
        Me.CbMotivoA.DesignTimeLayout = CbMotivoA_DesignTimeLayout
        Me.CbMotivoA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbMotivoA.Location = New System.Drawing.Point(154, 45)
        Me.CbMotivoA.Name = "CbMotivoA"
        Me.CbMotivoA.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.CbMotivoA.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.CbMotivoA.SelectedIndex = -1
        Me.CbMotivoA.SelectedItem = Nothing
        Me.CbMotivoA.Size = New System.Drawing.Size(278, 22)
        Me.CbMotivoA.TabIndex = 422
        Me.CbMotivoA.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(9, 44)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(140, 23)
        Me.LabelX2.TabIndex = 424
        Me.LabelX2.Text = "MOTIVO ANULACIÓN:"
        '
        'F0_IngresarReclamo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 235)
        Me.Controls.Add(Me.GroupPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "F0_IngresarReclamo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "F0_IngresarReclamo"
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        CType(Me.tbConcep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CbMotivoA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbObs As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbConcep As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnsalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnguardar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents MEP As System.Windows.Forms.ErrorProvider
    Public WithEvents CbMotivoA As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
End Class
