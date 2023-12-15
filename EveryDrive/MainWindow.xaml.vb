Imports Microsoft.Graph
Class MainWindow
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        EDCore.MainClass.InitializeGraphModule()
        Dim UserInfo = EDCore.MainClass.GetUserInformationAsync().Result
        USERNAME.Content = $"用户名, {UserInfo?.DisplayName}!"
        EMAIL.Content = $"邮箱: {UserInfo?.Mail & UserInfo?.UserPrincipalName}"
        MsgBox(UserInfo)
        'If UserInfo.GetType.Name = "String" Then
        '    MsgBox(UserInfo)
        'Else
        '    USERNAME.Content = $"用户名, {UserInfo?.DisplayName}!"
        '    EMAIL.Content = $"邮箱: {UserInfo?.Mail & UserInfo?.UserPrincipalName}"
        'End If

    End Sub
End Class
