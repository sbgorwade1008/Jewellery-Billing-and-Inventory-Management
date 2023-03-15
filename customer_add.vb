Imports System.Data.SqlClient
Public Class Button1
    Dim sqlconn As New SqlConnection("Server=LAPTOP-PHESJ194\SQLEXPRESS;Database=bar_db;Trusted_Connection=True")
    Dim sqlcommand As SqlCommand
    Private Sub customer_add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        f_nave.Checked = True
        m_nave.Checked = True
        a_nave.Checked = True

        'TODO: This line of code loads data into the 'Bar_dbDataSet2.customer_View' table. You can move, or remove it, as needed.
        Me.Customer_ViewTableAdapter.Fill(Me.Bar_dbDataSet2.customer_View)
        'TODO: This line of code loads data into the 'Bar_dbDataSet.barcode_store' table. You can move, or remove it, as needed.
        Me.Barcode_storeTableAdapter.Fill(Me.Bar_dbDataSet.barcode_store)
        'TODO: This line of code loads data into the 'Bar_dbDataSet.customer_dtl' table. You can move, or remove it, as needed.
        Me.Customer_dtlTableAdapter.Fill(Me.Bar_dbDataSet.customer_dtl)

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles save.Click
        Try
            sqlconn.Open()
            sqlcommand = New SqlCommand("SELECT [name]FROM [dbo].[customer_dtl] where [name]='" + TextBox1.Text + "';", sqlconn)
            Dim myreader As SqlDataReader
            myreader = sqlcommand.ExecuteReader
            myreader.Read()
            Dim gro, mob, op_d, f_st, m_st, a_st As String
            Dim op_m, op_fn, op_amt As Single
            gro = cb_group.Text
            op_d = op_date.Value.ToString("yyyy-MM-dd")
            mob = mob_in.Text

            If f_jama.Checked = True Then
                f_st = "JAMA"
            Else
                f_st = "NAVE"
            End If

            If m_jama.Checked = True Then
                m_st = "JAMA"
            Else
                m_st = "NAVE"
            End If

            If a_jama.Checked = True Then
                a_st = "JAMA"
            Else
                a_st = "NAVE"
            End If

            If op_amt_in.Text <> "" Then
                op_amt = op_amt_in.Text
            Else
                op_amt = 0
            End If

            If op_majuri.Text <> "" Then
                op_m = op_majuri.Text
            Else
                op_m = 0
            End If

            If op_fine.Text <> "" Then
                op_fn = op_fine.Text
            Else
                op_fn = 0
            End If

            If (myreader.HasRows) Then
                MessageBox.Show("Same Name Not Allowed")
                sqlconn.Close()
            Else
                sqlconn.Close()
                sqlconn.Open()
                sqlcommand = sqlconn.CreateCommand()
                sqlcommand.CommandText = ("INSERT INTO [dbo].[customer_dtl]([name],[city],[group_dt],[mobile_no],[op_fine],[op_majuri],[op_amount],[op_date],[f_status],[m_status],[a_status],[current_datetime])VALUES('" + TextBox1.Text + "','" + TextBox2.Text + "','" & gro & "','" & mob & "','" & op_fn & "','" & op_m & "','" & op_amt & "','" & op_d & "','" & f_st & "','" & m_st & "','" & a_st & "',GETDATE());")
                sqlcommand.ExecuteNonQuery()
                customer_add_Load(e, e)
                MessageBox.Show("Successfully inserted.")
                sqlconn.Close()
                op_amt_in.Text = ""
                op_majuri.Text = ""
                op_fine.Text = ""
                cb_group.Text = ""
                mob_in.Text = ""
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If


            sqlconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            sqlconn.Close()
        End Try
        sqlconn.Close()
    End Sub

    Private Sub f_jama_CheckedChanged(sender As Object, e As EventArgs) Handles f_jama.CheckedChanged
        If f_jama.Checked = True Then
            op_fine.BackColor = Color.LawnGreen
        Else
            op_fine.BackColor = Color.OrangeRed
        End If
    End Sub



    Private Sub m_jama_CheckedChanged_1(sender As Object, e As EventArgs) Handles m_jama.CheckedChanged
        If m_jama.Checked = True Then
            op_majuri.BackColor = Color.LawnGreen
        Else
            op_majuri.BackColor = Color.OrangeRed
        End If
    End Sub

    Private Sub a_jama_CheckedChanged_1(sender As Object, e As EventArgs) Handles a_jama.CheckedChanged
        If a_jama.Checked = True Then
            op_amt_in.BackColor = Color.LawnGreen
        Else
            op_amt_in.BackColor = Color.OrangeRed
        End If
    End Sub

    Private Sub f_nave_CheckedChanged(sender As Object, e As EventArgs) Handles f_nave.CheckedChanged
        If f_jama.Checked = True Then
            op_fine.BackColor = Color.LawnGreen
        Else
            op_fine.BackColor = Color.OrangeRed
        End If
    End Sub

    Private Sub m_nave_CheckedChanged(sender As Object, e As EventArgs) Handles m_nave.CheckedChanged
        If m_jama.Checked = True Then
            op_majuri.BackColor = Color.LawnGreen
        Else
            op_majuri.BackColor = Color.OrangeRed
        End If
    End Sub

    Private Sub a_nave_CheckedChanged(sender As Object, e As EventArgs) Handles a_nave.CheckedChanged
        If a_jama.Checked = True Then
            op_amt_in.BackColor = Color.LawnGreen
        Else
            op_amt_in.BackColor = Color.OrangeRed
        End If
    End Sub

    Private Sub clear_Click(sender As Object, e As EventArgs) Handles clear.Click
        op_amt_in.Text = ""
        op_majuri.Text = ""
        op_fine.Text = ""
        cb_group.Text = ""
        mob_in.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs) Handles delete.Click

    End Sub

    Function name_cst()
        Dim cust_name As String
        cust_name = Guna2DataGridView1.CurrentRow.Cells(1).Value
        Return cust_name
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If name_cst() <> "" Then
            wastage_in.Close()
            wastage_in.Show()
        Else
            MessageBox.Show("Select customer from Table")
        End If

    End Sub


End Class