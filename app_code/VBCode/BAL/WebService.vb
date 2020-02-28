Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.IO
Imports System.Xml
Imports System.Xml.Linq
Imports System.Windows.Forms
Imports System.Net
Imports System.Xml.XPath


''' <summary>
''' This class is an alternative when you can't use Service References. It allows you to invoke Web Methods on a given Web Service URL.
''' Based on the code from http://stackoverflow.com/questions/9482773/web-service-without-adding-a-reference
''' </summary>
Public Class WebService
	Public Property Url() As String
		Get
			Return m_Url
		End Get
		Private Set
			m_Url = Value
		End Set
	End Property
	Private m_Url As String
	Public Property Method() As String
		Get
			Return m_Method
		End Get
		Private Set
			m_Method = Value
		End Set
	End Property
	Private m_Method As String
	Public Params As New Dictionary(Of String, String)()
	Public ResponseSOAP As XDocument = XDocument.Parse("<root/>")
	Public ResultXML As XDocument = XDocument.Parse("<root/>")
	Public ResultString As String = [String].Empty

	Private InitialCursorState As Cursor

	Public Sub New()
		Url = [String].Empty
		Method = [String].Empty
	End Sub
	Public Sub New(baseUrl As String)
		Url = baseUrl
		Method = [String].Empty
	End Sub
	Public Sub New(baseUrl As String, methodName As String)
		Url = baseUrl
		Method = methodName
	End Sub

	' Public API

	''' <summary>
	''' Adds a parameter to the WebMethod invocation.
	''' </summary>
	''' <param name="name">Name of the WebMethod parameter (case sensitive)</param>
	''' <param name="value">Value to pass to the paramenter</param>
	Public Sub AddParameter(name As String, value As String)
		Params.Add(name, value)
	End Sub

	Public Sub Invoke()
		Invoke(Method, True)
	End Sub

	''' <summary>
	''' Using the base url, invokes the WebMethod with the given name
	''' </summary>
	''' <param name="methodName">Web Method name</param>
	Public Sub Invoke(methodName As String)
		Invoke(methodName, True)
	End Sub

	''' <summary>
	''' Cleans all internal data used in the last invocation, except the WebService's URL.
	''' This avoids creating a new WebService object when the URL you want to use is the same.
	''' </summary>
	Public Sub CleanLastInvoke()
		ResponseSOAP = InlineAssignHelper(ResultXML, Nothing)
		ResultString = InlineAssignHelper(Method, [String].Empty)
		Params = New Dictionary(Of String, String)()
	End Sub

	#Region "Helper Methods"

	''' <summary>
	''' Checks if the WebService's URL and the WebMethod's name are valid. If not, throws ArgumentNullException.
	''' </summary>
	''' <param name="methodName">Web Method name (optional)</param>
	Private Sub AssertCanInvoke(Optional methodName As String = "")
		If Url = [String].Empty Then
			Throw New ArgumentNullException("You tried to invoke a webservice without specifying the WebService's URL.")
		End If
		If (methodName = "") AndAlso (Method = [String].Empty) Then
			Throw New ArgumentNullException("You tried to invoke a webservice without specifying the WebMethod.")
		End If
	End Sub

	Private Sub ExtractResult(methodName As String)
		' Selects just the elements with namespace http://tempuri.org/ (i.e. ignores SOAP namespace)
		Dim namespMan As New XmlNamespaceManager(New NameTable())
		namespMan.AddNamespace("foo", "http://tempuri.org/")

		Dim webMethodResult As XElement = ResponseSOAP.XPathSelectElement((Convert.ToString("//foo:") & methodName) + "Result", namespMan)
		' If the result is an XML, return it and convert it to string
		If webMethodResult.FirstNode.NodeType = XmlNodeType.Element Then
			ResultXML = XDocument.Parse(webMethodResult.FirstNode.ToString())
			ResultXML = Utils.RemoveNamespaces(ResultXML)
			ResultString = ResultXML.ToString()
		Else
			' If the result is a string, return it and convert it to XML (creating a root node to wrap the result)
			ResultString = webMethodResult.FirstNode.ToString()
			ResultXML = XDocument.Parse((Convert.ToString("<root>") & ResultString) + "</root>")
		End If
	End Sub

	''' <summary>
	''' Invokes a Web Method, with its parameters encoded or not.
	''' </summary>
	''' <param name="methodName">Name of the web method you want to call (case sensitive)</param>
	''' <param name="encode">Do you want to encode your parameters? (default: true)</param>
	Private Sub Invoke(methodName As String, encode As Boolean)
		AssertCanInvoke(methodName)
		Dim soapStr As String = "<?xml version=""1.0"" encoding=""utf-8""?>" & vbCr & vbLf & "                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""" & vbCr & vbLf & "                   xmlns:xsd=""http://www.w3.org/2001/XMLSchema""" & vbCr & vbLf & "                   xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" & vbCr & vbLf & "                  <soap:Body>" & vbCr & vbLf & "                    <{0} xmlns=""http://tempuri.org/"">" & vbCr & vbLf & "                      {1}" & vbCr & vbLf & "                    </{0}>" & vbCr & vbLf & "                  </soap:Body>" & vbCr & vbLf & "                </soap:Envelope>"

		Dim req As HttpWebRequest = DirectCast(WebRequest.Create(Url), HttpWebRequest)
		req.Headers.Add("SOAPAction", (Convert.ToString("""http://tempuri.org/") & methodName) + """")
		req.ContentType = "text/xml;charset=""utf-8"""
		req.Accept = "text/xml"
		req.Method = "POST"

		Using stm As Stream = req.GetRequestStream()
			Dim postValues As String = ""
			For Each param In Params
				If encode Then
					postValues += String.Format("<{0}>{1}</{0}>", HttpUtility.HtmlEncode(param.Key), HttpUtility.HtmlEncode(param.Value))
				Else
					postValues += String.Format("<{0}>{1}</{0}>", param.Key, param.Value)
				End If
			Next

			soapStr = String.Format(soapStr, methodName, postValues)
			Using stmw As New StreamWriter(stm)
				stmw.Write(soapStr)
			End Using
		End Using

		Using responseReader As New StreamReader(req.GetResponse().GetResponseStream())
			Dim result As String = responseReader.ReadToEnd()
			ResponseSOAP = XDocument.Parse(Utils.UnescapeString(result))
			ExtractResult(methodName)
		End Using
	End Sub

	''' <summary>
	''' This method should be called before each Invoke().
	''' </summary>
	Friend Sub PreInvoke()
		CleanLastInvoke()
		InitialCursorState = Cursor.Current

		' feel free to add more instructions to this method
	End Sub

	''' <summary>
	''' This method should be called after each (successful or unsuccessful) Invoke().
	''' </summary>
	Friend Sub PosInvoke()
		'Cursor.Current = InitialCursorState;
		' feel free to add more instructions to this method
	End Sub
	Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
		target = value
		Return value
	End Function
	#End Region

End Class
