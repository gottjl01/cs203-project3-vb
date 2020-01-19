Public Class frmMain

    Friend persister As PFW.CSIST203.Project3.Persisters.IPersistData
    Friend canCancel As Boolean = False

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        persister = New PFW.CSIST203.Project3.Persisters.Access.AccessPersister()
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        Dim selectedRow = Int32.Parse(txtRow.Text.Trim()) - 1
        Dim maximum = persister.CountRows()
        If selectedRow <= 0 Then
            ' Do Nothing
        Else
            Dim row = persister.GetRow(selectedRow)
            txtFirstname.Text = CType(row("First Name"), String)
            txtLastname.Text = CType(row("Last Name"), String)
            txtEmailAddress.Text = CType(row("E-mail Address"), String)
            txtBusinessPhone.Text = CType(row("Business Phone"), String)
            txtCompany.Text = CType(row("Company"), String)
            txtTitle.Text = CType(row("Job Title"), String)
            'txtRow.Text = selectedRow.ToString()
            txtRow.Text = CType(row("ID"), String)
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim maximum = persister.CountRows()
        Dim selectedRow = Int32.Parse(txtRow.Text.Trim()) + 1
        If selectedRow > maximum Then
            ' Do Nothing
        Else
            DisplayRow(selectedRow)
            txtRow.Text = selectedRow.ToString()
        End If
    End Sub

    Friend Sub DisplayRow(selectedRow As Integer)
        Dim table = persister.GetData()
        Dim row = table.Rows(selectedRow - 1)
        txtFirstname.Text = CType(row("First Name"), String)
        txtLastname.Text = CType(row("Last Name"), String)
        txtEmailAddress.Text = CType(row("E-mail Address"), String)
        txtBusinessPhone.Text = CType(row("Business Phone"), String)
        txtCompany.Text = CType(row("Company"), String)
        txtTitle.Text = CType(row("Job Title"), String)
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        If Not persister Is Nothing Then
            persister.Dispose()
            persister = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Handle the File -> Open dialog box used for selecting the excel file that is utilized by the front-end
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        OpenFileDialog.InitialDirectory = System.Environment.CurrentDirectory
        OpenFileDialog.FileName = String.Empty
        OpenFileDialog.Filter = persister.FileFilter
        Dim result = OpenFileDialog.ShowDialog()
        If result = DialogResult.OK Then
            LoadFile(OpenFileDialog.FileName)
        End If
    End Sub

    Friend Sub LoadFile(selectedFile As String)
        persister.Dispose()
        persister = New PFW.CSIST203.Project3.Persisters.Access.AccessPersister(selectedFile)

        If persister.CountRows() > 0 Then

            ' enable all of the fields for editing
            txtRow.Text = "1" ' reset back to the first item in the data table
            txtFirstname.Enabled = True
            txtLastname.Enabled = True
            txtEmailAddress.Enabled = True
            txtBusinessPhone.Enabled = True
            txtCompany.Enabled = True
            txtTitle.Enabled = True
            btnSave.Enabled = True
            DisplayRow(1)

        Else ' disable all fields from editing

            txtRow.Text = "0" ' reset back to zero
            txtFirstname.Enabled = False
            txtLastname.Enabled = False
            txtEmailAddress.Enabled = False
            txtBusinessPhone.Enabled = False
            txtCompany.Enabled = False
            txtTitle.Enabled = False
            btnSave.Enabled = False

            ' clear out all of the fields
            txtFirstname.Text = String.Empty
            txtLastname.Text = String.Empty
            txtEmailAddress.Text = String.Empty
            txtBusinessPhone.Text = String.Empty
            txtCompany.Text = String.Empty
            txtTitle.Text = String.Empty

        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim control = CType(sender, Control)
        Me.canCancel = True

        Try

            If (ValidateChildren(ValidationConstraints.Enabled)) Then

                ' retrieve the existing row from the persistent medium
                Dim row = persister.GetRow(Integer.Parse(Me.txtRow.Text.Trim()))

                ' change the column data of the row
                row("First Name") = txtFirstname.Text
                row("Last Name") = txtLastname.Text
                row("E-mail Address") = txtEmailAddress.Text
                row("Business Phone") = txtBusinessPhone.Text
                row("Company") = txtCompany.Text
                row("Job Title") = txtTitle.Text

                ' propagate the row back to the persister for updating
                persister.UpdateRow(row)

            End If

        Catch
            Throw
        Finally
            Me.canCancel = False
        End Try

    End Sub

    Friend Sub TxtFirstname_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTitle.Validating, txtLastname.Validating, txtFirstname.Validating, txtEmailAddress.Validating, txtCompany.Validating, txtBusinessPhone.Validating

        Dim control = CType(sender, Control)
        If String.IsNullOrWhiteSpace(control.Text) Then
            If canCancel Then
                e.Cancel = True
            End If
            ErrorProvider.SetError(control, "Value must be non-whitespace and non-empty")
        Else
            ErrorProvider.SetError(control, String.Empty)
        End If

    End Sub

End Class
