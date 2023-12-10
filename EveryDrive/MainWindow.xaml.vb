Imports EDCore.MainClass
Class MainWindow
    ReadOnly MyEDCore As New EDCore.MainClass
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        MyEDCore.CrateClient(by.Text， az.Text)
        Dim userDisplayName = MyEDCore.getUserInformationAsync.Result
        DisplayName.Text = userDisplayName
    End Sub
End Class
