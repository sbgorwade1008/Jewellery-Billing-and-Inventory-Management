Imports System.Data.SqlClient
Public Class Form1

    Sub Switch_panal(ByVal panel As Form)
        Panel3.Controls.Clear()
        panel.TopLevel = False
        Panel3.Controls.Add(panel)
        panel.Dock = DockStyle.Fill
        panel.Show()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub bill_Click(sender As Object, e As EventArgs) Handles bill.Click
        'Switch_panal(billing1)
        Switch_panal(graph1)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Switch_panal(Receipt_main)
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Switch_panal(account)
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Switch_panal(opening)
        opening.progresscircle.Value = 0
        opening.Timer1.Start()

        Switch_panal(Report_Dashboard)
        All_bill_summary.Show()

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Main_bill.Show()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        imp.Close()
        Switch_panal(imp)
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Switch_panal(Dash_view_1)
        Dash_view_1.Refresh()
    End Sub


End Class
