Option Strict Off
Option Explicit On
Public Class frmOptions
	Inherits System.Windows.Forms.Form

	Public Event RequestForCleanMRU()
	Public Event RequestForCleanAddressBar()

	Public Structure StyleSheet
		Dim BackColor As Color
		Dim ForeColor As Color
		Dim Font As Font
		Dim LineHeight As Integer
	End Structure

	Public Event RequestForStyleSheet(ByRef StyleSheetSet As StyleSheet)
	Public Event RequestForSetting(ByRef MaxMRUItem As Integer, ByRef TimeInterval As Integer)
	Public Event RequestForSaveStyleSheet(ByVal StyleSheetSet As StyleSheet)
	Public Event RequestForSaveSetting(ByVal MaxMRUItem As Integer, ByVal TimeInterval As Integer)
	Public Event RequestForCreateCssFile(ByVal FileName As String)
	Public Event RequestForOpacitySetting(ByRef Value As Integer)
	Public Event RequestForSaveOpacity(ByVal Value As Integer)
	Public Event RequestForSaveImageFolder(ByVal Value As String)
	Public Event RequestforImageFolder(ByRef Value As String)



	Private Sub cmdBackColor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBackColor.Click


		Dim cDLG As New System.Windows.Forms.ColorDialog
		cDLG.Color = Me.frmEx.BackColor
		Dim result As DialogResult = cDLG.ShowDialog

		If result = Windows.Forms.DialogResult.OK Then
			frmEx.BackColor = cDLG.Color
		End If
		

	End Sub

	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

		Me.Close()

	End Sub

	Private Sub cmdClearAddressBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClearAddressBar.Click
		RaiseEvent RequestForCleanAddressBar()
	End Sub

	Private Sub cmdClearRecent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClearRecent.Click
		RaiseEvent RequestForCleanMRU()
	End Sub

	Private Sub cmdFont_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFont.Click


		Dim cDLG As New System.Windows.Forms.FontDialog
		cDLG.Font = Me.lblSample1.Font
		Dim result As DialogResult = cDLG.ShowDialog
		If result = Windows.Forms.DialogResult.OK Then
			lblSample1.Font = cDLG.Font
			lblSample2.Font = cDLG.Font
			lblSample3.Font = cDLG.Font
			lblSample4.Font = cDLG.Font
		End If

	End Sub

	Private Sub cmdForeColor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdForeColor.Click

		Dim cDLG As New System.Windows.Forms.ColorDialog
		cDLG.Color = Me.lblSample1.ForeColor
		Dim result As DialogResult = cDLG.ShowDialog

		If result = Windows.Forms.DialogResult.OK Then
			lblSample1.ForeColor = cDLG.Color
			lblSample2.ForeColor = cDLG.Color
			lblSample3.ForeColor = cDLG.Color
			lblSample4.ForeColor = cDLG.Color
		End If

	End Sub

	Public Sub MakeCss(ByRef fullpath As String)

		Dim Writer As New IO.StreamWriter(fullpath)

		With Writer
			.WriteLine("body {")
			.WriteLine("        background-color: " & System.Drawing.ColorTranslator.ToHtml(frmEx.BackColor) & ";")
			.WriteLine("        margin-left:8;")
			.WriteLine("        margin-top:8;")
			.WriteLine("        margin-right:8;")
			.WriteLine("        margin-bottom:8;")
			.WriteLine("        line-height:" & CInt(Me.NumLineHeight.Value) & "%;")
			.WriteLine("     }")
			.WriteLine("body,p,tr,td,.m_text {")
			.WriteLine("        font-family:" & Chr(34) & frmEx.Font.Name & Chr(34) & ";")
			'.WriteLine "        font-size:" + Str$(frmEx.Font.Size) + "pt;"
			.WriteLine("        color: " & System.Drawing.ColorTranslator.ToHtml(lblSample1.ForeColor) & ";")
			'.WriteLine "        line-height:" + cboLineHeight.Tag + ";"
		End With
		With frmEx.Font
			If .Bold Then Writer.WriteLine("        font-weight:Bold;")
			If .Italic Then Writer.WriteLine("        font-style:Italic;")
			If .Underline Then Writer.WriteLine("        text-decroation:underline;")
		End With
		Writer.Write("}")
		Writer.Close()
		MsgBox(fullpath & vbCrLf & " saved.", MsgBoxStyle.Information)
	End Sub
	Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
		Dim MyStyle As New StyleSheet
		With MyStyle
			.BackColor = frmEx.BackColor
			.ForeColor = lblSample1.ForeColor
			.LineHeight = CInt(NumLineHeight.Value)
			.Font = lblSample1.Font
		End With
		RaiseEvent RequestForSaveStyleSheet(MyStyle)
		RaiseEvent RequestForSaveSetting(CInt(Me.NumMRU.Value), CInt(Me.NumInterval.Value))
		RaiseEvent RequestForSaveOpacity(TrackOpacity.Value)
		RaiseEvent RequestForSaveImageFolder(Me.txtImageFolder.Text)
	End Sub

	Private Sub cmdSaveCss_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSaveCss.Click

		Dim cDLG As New Windows.Forms.SaveFileDialog
		With cDLG
			.InitialDirectory = My.Application.Info.DirectoryPath
			.Filter = "Style Sheet File|*.css|All File|*.*"
		End With
		Dim Result As DialogResult = cDLG.ShowDialog
		If Result = Windows.Forms.DialogResult.OK Then
			Me.MakeCss(cDLG.FileName)
			'RaiseEvent RequestForCreateCssFile(cDLG.FileName)
		End If

	End Sub

	Private Sub frmOptions_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

		lblSample1.Text = "天若有情天亦老凤凰台上凤凰游剑气萧瑟清澈山水秋风扫落叶天若有情天亦老凤凰台上凤凰游剑气萧瑟清澈山水秋风扫落叶天若有情天亦老凤凰台上凤凰游剑气萧瑟清澈山水秋风扫落叶"
		lblSample2.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
		lblSample3.Text = lblSample1.Text
		lblSample4.Text = lblSample2.Text

		Dim MyStyle As New StyleSheet
		RaiseEvent RequestForStyleSheet(MyStyle)
		Try
			With MyStyle
				lblSample1.Font = .Font
				lblSample2.Font = .Font
				lblSample3.Font = .Font
				lblSample4.Font = .Font
				lblSample1.ForeColor = .ForeColor
				lblSample2.ForeColor = .ForeColor
				lblSample3.ForeColor = .ForeColor
				lblSample4.ForeColor = .ForeColor
				frmEx.BackColor = .BackColor
				NumLineHeight.Value = .LineHeight
			End With

			Dim maxMRUItem As Integer
			Dim TimeInterval As Integer

			RaiseEvent RequestForSetting(maxMRUItem, TimeInterval)

			Me.NumMRU.Value = maxMRUItem
			Me.NumInterval.Value = TimeInterval

			Dim Opacity As Integer = 100
			RaiseEvent RequestForOpacitySetting(Opacity)
			Me.TrackOpacity.Value = Opacity
			Me.lblOpacitySetting.Text = "Programe Opacity is :" & TrackOpacity.Value & "%"

			Dim ImageFolder As String = ""
			RaiseEvent RequestforImageFolder(ImageFolder)
			Me.txtImageFolder.Text = ImageFolder
		Catch ex As Exception
			MsgBox(ex.Message, MsgBoxStyle.Information)
		End Try


	End Sub

	Private Sub NumLineHeight_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumLineHeight.ValueChanged
		Const DIS As Integer = 10
		Dim CurDIS As Integer = NumLineHeight.Value * DIS / 100
		lblSample2.Location = New Point(0, lblSample1.Location.Y + lblSample1.Size.Height + CurDIS)
		lblSample3.Location = New Point(0, lblSample2.Location.Y + lblSample2.Size.Height + CurDIS)
		lblSample4.Location = New Point(0, lblSample3.Location.Y + lblSample3.Size.Height + CurDIS)

	End Sub


	Private Sub TrackOpacity_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackOpacity.Scroll
		Me.lblOpacitySetting.Text = "Programe Opacity is :" & TrackOpacity.Value & "%"
	End Sub

	Private Sub btnSelectImageFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectImageFolder.Click
		Dim Result As DialogResult
		Dim Dlg As New FolderBrowserDialog
		Dlg.SelectedPath = txtImageFolder.Text
		result = Dlg.ShowDialog
		If Result = Windows.Forms.DialogResult.OK Then
			txtImageFolder.Text = Dlg.SelectedPath
		End If

	End Sub
End Class