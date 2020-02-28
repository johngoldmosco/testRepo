Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports System.Collections
Imports System.Security.Cryptography
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports MySql.Data
Imports MySql
Imports MySql.Data.MySqlClient

Public Class ODBC
    Public con As New OdbcConnection
    Public cmd As New OdbcCommand

    Public strCon As String
    Private _path As String
    Private _filterAttribute As String
    Public Shared LastError As New System.Exception

    Public Sub openAGet(ByRef conn As OdbcConnection) ' if Var_connectiontype is not supplied then default value "P" is supplied for pooled connection
        Try
            'strCon = ConfigurationSettings.AppSettings("PooledConnectionString")
            strCon = System.Configuration.ConfigurationManager.AppSettings("PooledConnectionString")
            conn = New OdbcConnection(strCon)
            conn.Open()
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        End Try
    End Sub

    Public Sub getConn(Optional ByVal Var_connectiontype As String = "P") ' if Var_connectiontype is not supplied then default value "P" is supplied for pooled connection

        Try
            If Var_connectiontype = "U" Then
                ' strCon = ConfigurationSettings.AppSettings("UnPooledConnectionString")

                strCon = System.Configuration.ConfigurationManager.AppSettings("UnPooledConnectionString")
            Else
                'strCon = ConfigurationSettings.AppSettings("PooledConnectionString")
                strCon = System.Configuration.ConfigurationManager.AppSettings("PooledConnectionString")
            End If
            con = New OdbcConnection(strCon)
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        End Try
    End Sub


    Public Sub OpenConn()
        Dim Var_errmsg As String
        If con.State <> ConnectionState.Open Or con.State <> ConnectionState.Fetching Or ConnectionState.Executing Then
            Try
                con.Open()
            Catch odbcEx As OdbcException
                Throw New Exception(odbcEx.Message)
            Catch appEx As Exception
                Var_errmsg = appEx.Message.ToString
                If Var_errmsg = "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached." Then
                    getConn("U") '"U" parameter is being passed for unpooled connections in case the connection pooling limit has been reached
                    OpenConn()
                End If
            End Try
        End If
    End Sub

    'Example
    'clsdal.getDataReader("select * from product_mstr")  
    Public Function getDataReader(ByVal pstrQuery As String) As OdbcDataReader
        Dim datareader As OdbcDataReader = Nothing
        Try
            getConn()
            OpenConn()
            cmd = New OdbcCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120
            cmd.CommandText = pstrQuery
            cmd.Connection = con
            datareader = cmd.ExecuteReader()
            Return datareader
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        End Try
        closeConn()
        datareader.Close()
        cmd.Dispose()
    End Function

    'Example
    'Dim ds As DataSet = Nothing
    'ds = CType(clsdal.getDataSet("select * from product_mstr"), DataSet)
    'GridView1.DataSource = ds
    'GridView1.DataBind()
    Public Function getDataSet(ByVal pstrQuery As String) As DataSet
        Dim ds As DataSet = Nothing
        Dim dapt As OdbcDataAdapter = Nothing
        Try
            getConn()
            OpenConn()
            dapt = New OdbcDataAdapter(pstrQuery, con)
            ds = New DataSet
            dapt.Fill(ds)
            Return (ds)
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            dapt.Dispose()
            ds.Dispose()
        End Try
    End Function

    'Example
    'Dim ds As DataSet = Nothing
    'ds = CType(clsdal.getDataSet("select * from product_mstr", "productmstr"), DataSet)
    'GridView1.DataSource = ds.Tables("productmstr").DefaultView
    'GridView1.DataBind()
    Public Function getDataSet(ByVal pstrQuery As String, ByVal pstrtable As String) As DataSet
        Dim ds As DataSet = Nothing
        Dim dapt As OdbcDataAdapter = Nothing
        Try
            getConn()
            OpenConn()
            dapt = New OdbcDataAdapter(pstrQuery, con)
            ds = New DataSet
            dapt.Fill(ds, pstrtable)
            Return (ds)
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            dapt.Dispose()
            ds.Dispose()
        End Try
    End Function

    'Example
    'Dim int As Integer
    'Int = clsdal.executeScalar("select * from product_mstr")
    Public Function executeScalar_int(ByVal pstrQuery As String) As Integer
        Dim cmd As OdbcCommand = Nothing
        Dim retval As Integer
        Try
            getConn()
            OpenConn()
            cmd = New OdbcCommand(pstrQuery, con)
            retval = cmd.ExecuteScalar
            Return retval
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            cmd.Dispose()
        End Try
    End Function

    Public Function executeScalar_dbl(ByVal pstrQuery As String) As Double
        Dim cmd As OdbcCommand = Nothing
        Dim retval As Double
        Try
            getConn()
            OpenConn()
            cmd = New OdbcCommand(pstrQuery, con)
            retval = cmd.ExecuteScalar
            Return retval
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            cmd.Dispose()
        End Try
    End Function

    Public Function executeScalar_str(ByVal pstrQuery As String) As String
        Dim cmd As OdbcCommand = Nothing
        Dim retval As String
        Try
            getConn()
            OpenConn()
            cmd = New OdbcCommand(pstrQuery, con)
            retval = cmd.ExecuteScalar
            Return retval
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            cmd.Dispose()
        End Try
    End Function

    'Example
    'int = clsdal.executeNonQuery("insert into temp1 values(1,2,3,4,5)")
    'MsgBox(int & " seccussfully inserted")
    Public Function executeNonQuery(ByVal pstrQuery As String) As Integer
        Dim cmd As OdbcCommand = Nothing
        Dim retval As Integer
        Try
            getConn()
            OpenConn()
            cmd = New OdbcCommand(pstrQuery, con)
            retval = cmd.ExecuteNonQuery()
            Return retval
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            cmd.Dispose()
        End Try
    End Function

    Public Sub closeConn()
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
                con.Dispose()
            End If
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        End Try
    End Sub

    'Example
    'clsdal.getMultiTableDataSet("select * from product_mstr", "prom", ds)
    'clsdal.getMultiTableDataSet("select * from product_mstr_mak", "promm", ds)
    'clsdal.getMultiTableDataSet("select * from status_mstr", "stam", ds)
    'clsdal.getMultiTableDataSet("select * from status_mstr_mak", "stamm", ds)
    Public Function getMultiTableDataSet(ByVal pstrQuery As String, ByVal pstrtable As String, Optional ByVal pstrDataSet As DataSet = Nothing) As DataSet
        Dim adpt As OdbcDataAdapter = Nothing
        Dim ds As New DataSet
        Try
            getConn()
            OpenConn()
            adpt = New OdbcDataAdapter(pstrQuery, con)
            adpt.SelectCommand = New OdbcCommand(pstrQuery, con)
            If IsNothing(pstrDataSet) = True Then
                ds = New DataSet
                adpt.Fill(ds, pstrtable)
            Else
                adpt.Fill(pstrDataSet, pstrtable)
                ds = pstrDataSet
            End If
            Return (ds)
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            adpt.Dispose()
            ds.Dispose()
        End Try
    End Function

    Public Sub GetPath(ByVal path As String)
        _path = path
    End Sub

    Public Sub GetPathHome(ByVal path As String)
        _path = path
    End Sub

    'Public Function IsAuthenticated(ByVal domain As String, ByVal username As String, ByVal pwd As String) As Boolean
    '    Dim domainAndUsername As String = domain + "\" + username + ""

    '    Dim entry As New DirectoryEntry(_path, domainAndUsername, pwd)

    '    Try
    '        ' Bind to the native AdsObject to force authentication.

    '        Dim obj As [Object] = entry.NativeObject
    '        Dim search As New DirectorySearcher(entry)
    '        search.Filter = "(SAMAccountName=" + username + ")"
    '        search.PropertiesToLoad.Add("cn")
    '        Dim result As SearchResult = search.FindOne()
    '        If IsNothing(result) = True Then
    '            Return False
    '        End If
    '        _path = result.Path
    '        _filterAttribute = CType(result.Properties("cn")(0), [String])
    '    Catch ex As Exception
    '        Throw New Exception("Error authenticating user. " + ex.Message)
    '    End Try
    '    Return True


    '    ' Update the new path to the user in the directory

    'End Function 'IsAuthenticated


    Public Shared Sub ConvertXlsNew(ByVal ds As GridView, ByVal response As HttpResponse)
        Dim style As String
        style = "<style> .text { mso-number-format:\@; } </style> "
        'first let's clean up the response.object
        response.Clear()
        response.Charset = ""

        response.AddHeader("content-disposition", "attachment;filename=Download.xls")
        'response.Cache.SetCacheability(HttpCacheability.NoCache)

        'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        'Dim dg As New DataGrid
        'set the datagrid datasource to the dataset passed in
        'dg.DataSource = ds.Tables(0)
        'bind the datagrid
        'dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        Dim HtmlForm As New HtmlForm
        ds.Parent.Controls.Add(HtmlForm)
        HtmlForm.Attributes("runat") = "server"
        HtmlForm.Controls.Add(ds)
        HtmlForm.RenderControl(htmlWrite)
        'all that's left is to output the html
        response.Write(style)
        response.Write(stringWrite.ToString)
        response.End()
    End Sub

    Public Shared Sub ConvertXls(ByVal ds As DataSet, ByVal response As HttpResponse)
        'first let's clean up the response.object
        response.Clear()
        response.Charset = ""

        response.AddHeader("content-disposition", "attachment;filename=Download.xls")
        'response.Cache.SetCacheability(HttpCacheability.NoCache)

        'set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        'instantiate a datagrid
        Dim dg As New DataGrid
        'set the datagrid datasource to the dataset passed in
        dg.DataSource = ds.Tables(0)
        'bind the datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'all that's left is to output the html
        response.Write(stringWrite.ToString)
        response.End()
    End Sub

    'Bhushan
    'Get DataTable
    'Create Object of This Class like OBDC obj=new ODBC Then Use It
    'Eg :- DataTable dt=obj.getDataTable("Select * From Customer");
    Public Function getDataTable(ByVal pstrQuery As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim dapt As OdbcDataAdapter = Nothing
        Try
            getConn("p")
            OpenConn()
            dapt = New OdbcDataAdapter(pstrQuery, con)
            dt = New DataTable()
            dapt.Fill(dt)
            Return (dt)
        Catch odbcEx As OdbcException
            Throw New Exception(odbcEx.Message)
        Finally
            closeConn()
            dapt.Dispose()
            dt.Dispose()
        End Try
    End Function

    Public Function GetStringForSQL(ByVal inputSQL As String) As String
        Return inputSQL.Replace("'", "''")
    End Function

  


End Class
