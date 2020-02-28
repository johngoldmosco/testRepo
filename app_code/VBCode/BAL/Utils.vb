Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Xml.Linq
Imports System.IO
Imports System.Text.RegularExpressions

''' <summary>
''' Summary description for Utils
''' </summary>
Public NotInheritable Class Utils
	Private Sub New()
	End Sub
	''' <summary>
	''' Remove all xmlns:* instances from the passed XmlDocument to simplify our xpath expressions
	''' </summary>
	Public Shared Function RemoveNamespaces(oldXml As XDocument) As XDocument
		' FROM: http://social.msdn.microsoft.com/Forums/en-US/bed57335-827a-4731-b6da-a7636ac29f21/xdocument-remove-namespace?forum=linqprojectgeneral

		Dim newXml As XDocument = XDocument.Parse(Regex.Replace(oldXml.ToString(), "(xmlns:?[^=]*=[""][^""]*[""])", "", RegexOptions.IgnoreCase Or RegexOptions.Multiline))
		Return newXml

	End Function

	''' <summary>
	''' Remove all xmlns:* instances from the passed XmlDocument to simplify our xpath expressions
	''' </summary>
	Public Shared Function RemoveNamespaces(oldXml As String) As XDocument
		Dim newXml As XDocument = XDocument.Parse(oldXml)
		Return RemoveNamespaces(newXml)
	End Function

	''' <summary>
	''' Converts a string that has been HTML-enconded for HTTP transmission into a decoded string.
	''' </summary>
	''' <param name="escapedString">String to decode.</param>
	''' <returns>Decoded (unescaped) string.</returns>
	Public Shared Function UnescapeString(escapedString As String) As String
		Return HttpUtility.HtmlDecode(escapedString)
	End Function
End Class

