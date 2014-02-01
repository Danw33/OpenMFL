Module MultiVideo
    Sub MultiConvert()
        disclaimer()
    End Sub 'MultiConvert
    Sub MultiBurn()
        disclaimer()
    End Sub 'MultiBurn
    Sub MultiConvertandBurn()
        disclaimer()
    End Sub 'MultiConvert and MultiBurn
    Private Sub disclaimer()
        MessageBox.Show("please note that multivideo features (such as the one that your are about to run) are still in their testing and development phases, there is a good chance that they may cause unresponsiveness of your pc, fail to burn disks or burn coasters, fail to convert or convert incorrectly, and more." + vbCrLf + "We WILL NOT be held responsible for any harm done while using these BETA Features!" + vbCrLf + "Continue at your own risk!", "BETA Disclaimer Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
    End Sub 'Disclaimer messasge (because of beta features)
End Module
