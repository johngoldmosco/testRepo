Imports Microsoft.VisualBasic

Public Class Inventory

    Dim clsOdbc As New ODBC

    Public Function AddInvCategory(ByVal strCatName As String) As String

        Dim strQuery As String = "INSERT INTO inv_category(inv_cat_name) VALUES ('" & strCatName & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "Item Category Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try


    End Function

    Public Function AddInvLocation(ByVal strLocName As String) As String

        Dim strQuery As String = "INSERT INTO inv_item_location(inv_location_name) VALUES ('" & strLocName & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "Inventory Location Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try


    End Function

    Public Function AddVendor(ByVal strVendorName As String, ByVal strOwnerName As String, ByVal strVendorContact As String, ByVal strVendorEmail As String, ByVal strVendorAddress As String) As String

        Dim strQuery As String = "INSERT INTO inv_vendors(vendor_name,vendor_owner_name,vendor_contact_number,vendor_email,vendor_address) VALUES ('" & strVendorName & "','" & strOwnerName & "','" & strVendorContact & "','" & strVendorEmail & "','" & strVendorAddress & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "Vendor Details Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try


    End Function

    Public Sub FillGridViewData(ByVal strQuery As String, ByVal gvGridView As GridView)

        Dim ds As New Data.DataSet

        Try

            ds = clsOdbc.getDataSet(strQuery)
            If (ds.Tables(0).Rows.Count > 0) Then
                gvGridView.DataSource = ds
                gvGridView.DataBind()
            Else
                CommonMessages.ShowAlertMessage("Sorry, No Records Found!")
                gvGridView.DataSource = Nothing
                gvGridView.DataBind()
            End If
        Catch ex As Exception

        Finally
            ds.Dispose()
        End Try

    End Sub

    Public Function AddItem(ByVal strItemSKU As String, ByVal strItemCat As String, ByVal strVendorName As String, ByVal strStockLoc As String, ByVal strItemName As String, ByVal strItemDesc As String, ByVal strItemQuant As String, ByVal strItemPurchased As String, ByVal strItemUnitPrice As String, ByVal strItemTotalPrice As String, ByVal strItemSellingPrice As String, ByVal strPurchaseDate As String) As String

        Dim strQuery As String = "INSERT INTO inv_items(item_sku,inv_cat_id,vendor_id,inv_location_id,item_name,item_description,item_quantity,item_purchased,item_unit_price,item_total_price,item_selling_price,item_purchase_date) VALUES ('" & strItemSKU & "','" & strItemCat & "','" & strVendorName & "','" & strStockLoc & "','" & strItemName & "','" & strItemDesc & "','" & strItemQuant & "','" & strItemPurchased & "','" & strItemUnitPrice & "','" & strItemTotalPrice & "','" & strItemSellingPrice & "','" & strPurchaseDate & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "Purchase Item Details Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try


    End Function

    Public Function AddCustomer(ByVal strCustFirstName As String, ByVal txtCustLastName As String, ByVal txtCustContact As String, ByVal txtCustEmail As String, ByVal txtCustAddress As String) As String

        Dim strQuery As String = "INSERT INTO inv_customers(cust_company_name,cust_client_name,cust_contact,cust_email,cust_address) VALUES ('" & strCustFirstName & "','" & txtCustLastName & "','" & txtCustContact & "','" & txtCustEmail & "','" & txtCustAddress & "')"

        Try
            clsOdbc.executeNonQuery(strQuery)
            Return "Customer Details Successfully Added!"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function

End Class
