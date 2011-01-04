Option Strict Off
Option Explicit On
Imports MYPLACE.Shared
Namespace MYPLACE.Product.ZhReader.ReadingHelper
	Public Class HHCParser
		Private mFileName As String
		Private mContent As System.Collections.ObjectModel.Collection(Of Page)
		Public Property FileName() As String
			Get
				Return mFileName
			End Get
			Set(ByVal value As String)
				mFileName = value
				Call Me.ParseFile(value)
			End Set
		End Property
		Public ReadOnly Property Content() As System.Collections.ObjectModel.Collection(Of Page)
			Get
				Return (mContent)
			End Get
		End Property

		Private Sub ParseFile(ByRef FileName As String)

			If Not IO.File.Exists(FileName) Then Return

			Dim hdoc As New mshtml.HTMLDocument
			'Dim ThisChild As Object

			mContent = New System.Collections.ObjectModel.Collection(Of Page)

			Dim Body As mshtml.HTMLBody

			Body = hdoc.createElement("BODY")
			Body.innerHTML = "ABDD"
			'Body.insertAdjacentHTML("body", "ABCD")
			hdoc.appendChild(Body)



			'For Each ThisChild In hdoc.body.children
			'	If ThisChild.nodeName = "UL" Then getLI(ThisChild, mContent, "", sBasePath)
			'Next ThisChild

			'Dim HHCText As String
			'Dim Reader As New IO.StreamReader(FileName, True)

			'HHCText = Reader.ReadToEnd

			'Call ParseText(HHCText, "")


		End Sub

		Public Function ParseText(ByRef HHCText As String, Optional ByVal sBasePath As String = "") As Boolean

			'Dim hdoc As New mshtml.HTMLDocument
			'Dim ThisChild As Object

			'mContent = New System.Collections.ObjectModel.Collection(Of Page)

			'Dim Body As mshtml.HTMLBody

			'Body = hdoc.createElement("BODY")
			'Body..innerHTML = HHCText
			'hdoc.appendChild(Body)



			'For Each ThisChild In hdoc.body.children
			'	If ThisChild.nodeName = "UL" Then getLI(ThisChild, mContent, "", sBasePath)
			'Next ThisChild

		End Function

		Private Sub getLI(ByVal ULE As mshtml.HTMLUListElement, ByRef sAll As Collections.ObjectModel.Collection(Of Page), ByVal sParent As String, Optional ByVal sBasePath As String = "")

			Dim LI As mshtml.HTMLLIElement
			Dim oChild As Object
			Dim p As mshtml.HTMLParamElement
			Dim LIName As String
			Dim LILocal As String
			Dim NewPage As Page

			If sBasePath <> "" Then sBasePath = FileFunction.BuildPath(sBasePath, "")

			For Each LI In ULE.childNodes

				NewPage = New Page
				LIName = ""
				LILocal = ""

				For Each oChild In LI.childNodes

					Select Case oChild.nodeName
						Case "OBJECT"
							For Each p In oChild.childNodes
								If p.name = "Name" Then LIName = p.value
								If p.name = "Local" Then LILocal = p.value
							Next p
							If sParent <> "" Then LIName = FileFunction.BuildPath(sParent, LIName)
							NewPage.Name = LIName
							NewPage.Path = sBasePath & LILocal
						Case "UL"
							LIName = FileFunction.BuildPath(LIName, "")
							NewPage.Name = LIName
							'UPGRADE_WARNING: 未能解析对象 oChild 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
							getLI(oChild, sAll, LIName, sBasePath)
					End Select

					sAll.Add(NewPage)
				Next oChild

			Next LI

		End Sub
	End Class
End Namespace
