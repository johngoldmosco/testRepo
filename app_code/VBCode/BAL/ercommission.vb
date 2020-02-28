Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Collections

''' <summary>
''' Summary description for ExampleAPIProxy
''' </summary>
Public Class ercommission
			'
			' TODO: Add constructor logic here
			'
	Public Sub New()
	End Sub

	Private Shared strCommissionURL As String = ConfigurationManager.AppSettings("commissionService")
    Private Shared ExampleAPI As New WebService(strCommissionURL & Convert.ToString("er_commission.asmx"))
	' DEFAULT location of the WebService, containing the WebMethods
	Public Shared Sub ChangeUrl(webserviceEndpoint As String)
		ExampleAPI = New WebService(webserviceEndpoint)
	End Sub

	Public Function ExampleWebMethod(intUserID As Integer, intOperatorType As Integer, intOperator As Integer, strToken As String) As String
		ExampleAPI.PreInvoke()

		ExampleAPI.AddParameter("userid ", intUserID.ToString())
		ExampleAPI.AddParameter("int_op_type_id", intOperatorType.ToString())
		ExampleAPI.AddParameter("int_op_id", intOperator.ToString())
		' Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it
		ExampleAPI.AddParameter("strToken", strToken)
		' all parameters are passed as strings
		Try
				' name of the WebMethod to call (Case Sentitive again!)
			ExampleAPI.Invoke("getCommissionDetailsOperators")
		Finally
			ExampleAPI.PosInvoke()
		End Try

		Return ExampleAPI.ResultString
		' you can either return a string or an XML, your choice
	End Function
	Public Function dynamicWebMethod(intParmCount As Integer, al As ArrayList, strMethod As String, strUrl As String) As String
		ExampleAPI = New WebService(strUrl)

		ExampleAPI.PreInvoke()

		For i As Integer = 0 To intParmCount - 1

				' Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it
				' all parameters are passed as strings
			ExampleAPI.AddParameter(al(2 * i).ToString(), al(2 * i + 1).ToString())
		Next
		Try
				' name of the WebMethod to call (Case Sentitive again!)
			ExampleAPI.Invoke(strMethod)
		Finally
			ExampleAPI.PosInvoke()
		End Try

		Return ExampleAPI.ResultString
		' you can either return a string or an XML, your choice
	End Function
End Class

