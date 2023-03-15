Imports System.Data.SqlClient
Public Class Item_add
    Dim sqlconn As New SqlConnection("Server=LAPTOP-PHESJ194\SQLEXPRESS;Database=bar_db;Trusted_Connection=True")
    Dim sqlcommand As SqlCommand
    Public Function id_get()
        Try
            Dim imax As Int32
            Dim i_id As String
            Dim myreader As SqlDataReader
            sqlconn.Close()
            sqlconn.Open()
            sqlcommand = New SqlCommand("SELECT max([b_int]) FROM [dbo].[Item_details] ;", sqlconn)
            myreader = sqlcommand.ExecuteReader
            myreader.Read()
            If myreader.IsDBNull(0) Then
                imax = 1
                i_id = "ITM00" + imax.ToString
                idlb.Text = i_id
            Else
                imax = myreader.GetInt32(0) + 1
                i_id = "ITM00" + imax.ToString
                idlb.Text = i_id
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim myreader As SqlDataReader
        Dim imax As Int32
        Dim i_id, name, igroup, mt_group, mjs As String
        Dim w, t, f, pc As Double
        Try
            sqlconn.Close()
            sqlconn.Open()
            sqlcommand = New SqlCommand("SELECT max([b_int]) FROM [dbo].[Item_details] ;", sqlconn)
            myreader = sqlcommand.ExecuteReader
            myreader.Read()
            If myreader.IsDBNull(0) Then
                imax = 1
                i_id = "ITM00" + imax.ToString
            Else
                imax = myreader.GetInt32(0) + 1
                i_id = "ITM00" + imax.ToString
            End If

            If it_name.Text = "" Then
                MessageBox.Show("Item name should not be null !")
            ElseIf it_group.Text = "" Then
                MessageBox.Show("Item Group should not be null !")
            ElseIf metal_gr.Text = "" Then
                MessageBox.Show("Metal Group should not be null !")
            ElseIf mj_status.Text = "" Then
                MessageBox.Show("Majuri Status should not be null !")
            Else
                name = it_name.Text
                igroup = it_group.Text
                mt_group = metal_gr.Text
                mjs = mj_status.Text
                If op_wet.Text = "" Then
                    w = 0
                Else
                    w = op_wet.Text
                End If
                If tanch.Text = "" Then
                    t = 0
                Else
                    t = tanch.Text
                End If

                If op_fine.Text = "" Then
                    f = 0
                Else
                    f = op_fine.Text
                End If

                pc = pcs.Text
                Try
                    sqlconn.Close()
                    sqlconn.Open()
                    sqlcommand = New SqlCommand("SELECT [name] FROM [dbo].[Item_details] where [name]='" & name & "';", sqlconn)
                    myreader = sqlcommand.ExecuteReader
                    myreader.Read()
                    If myreader.HasRows Then
                        MessageBox.Show("Name already exists!")
                    Else
                        sqlconn.Close()
                        sqlconn.Open()
                        sqlcommand = sqlconn.CreateCommand()
                        sqlcommand.CommandText = ("INSERT INTO [dbo].[Item_details]([id],[b_int],[name],[it_group],[m_group],[mj_status],[op_PCS],[op_weight],[tanch],[op_fine],[datetime]) VALUES('" & i_id & "','" & imax & "','" & name & "','" & igroup & "','" & mt_group & "','" & mjs & "','" & pc & "','" & w & "','" & t & "','" & f & "',GETDATE());")
                        sqlcommand.ExecuteNonQuery()
                        sqlconn.Close()
                        id_get()
                        db_view()
                        'Clear All record when added to db
                        it_name.Text = ""
                        it_group.Text = ""
                        metal_gr.Text = ""
                        mj_status.Text = ""
                        op_fine.Text = ""
                        op_wet.Text = ""
                        tanch.Text = ""
                        pcs.Text = ""
                        it_name.Focus()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message )
        End Try


    End Sub
    Public Sub db_view()
        Try
            sqlconn.Close()
            sqlconn.Open()
            Dim quer As String = "SELECT [id] as ID,[name] as Item,[it_group] as Main_Group,[m_group] as Metal,[mj_status] as Majurui_St,[op_PCS] as PCS ,[op_weight] as Op_Weight,[tanch] as Tanch,[op_fine] as Op_Fine FROM [dbo].[Item_details] order by [datetime];"
            Dim com As New SqlCommand(quer, sqlconn)
            Dim adater As New SqlDataAdapter(com)
            Dim table As New DataTable()
            sqlconn.Close()
            adater.Fill(table)
            dgv.DataSource = table
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub Item_add_Load(sender As Object, e As EventArgs) Handles Me.Load
        id_get()
        db_view()

    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        it_name.Text = ""
        it_group.Text = ""
        metal_gr.Text = ""
        mj_status.Text = ""
        op_fine.Text = ""
        op_wet.Text = ""
        tanch.Text = ""
        pcs.Text = ""

    End Sub

    Private Sub op_wet_TextChanged(sender As Object, e As EventArgs) Handles op_wet.TextChanged
        Dim w, t, f As Double
        Try
            If tanch.Text <> "" Then
                t = tanch.Text
                If op_wet.Text <> "" Then
                    w = op_wet.Text
                    f = w / 100 * t
                    op_fine.Text = Math.Round(f, 1)
                Else
                    op_fine.Text = ""
                End If
            Else
                op_fine.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub tanch_TextChanged(sender As Object, e As EventArgs) Handles tanch.TextChanged
        Dim w, t, f As Double
        Try
            If op_wet.Text <> "" Then
                w = op_wet.Text
                If tanch.Text <> "" Then
                    t = tanch.Text
                    If t > 100 Then
                        MessageBox.Show("Tanch should not be greater then 100%")
                        tanch.Text = ""
                        tanch.Focus()
                    End If
                    f = w / 100 * t
                    op_fine.Text = Math.Round(f, 1)
                Else
                    op_fine.Text = ""
                End If
            Else
                op_fine.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs) Handles delete.Click
        Dim it_id As String

        it_id = dgv.CurrentRow.Cells(0).Value
        If it_id <> "" Then
            sqlconn.Close()
            sqlconn.Open()
            sqlcommand = sqlconn.CreateCommand()
            sqlcommand.CommandText = ("DELETE FROM [dbo].[Item_details] WHERE id='" & it_id & "';")
            sqlcommand.ExecuteNonQuery()
            sqlconn.Close()
            id_get()
            db_view()
        End If

    End Sub
End Class