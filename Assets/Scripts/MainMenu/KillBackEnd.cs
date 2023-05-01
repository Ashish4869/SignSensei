using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public static class KillBackEnd 
{
    public static void QuitBackEnd()
    {
        ProcessStartInfo frCreationInf = new ProcessStartInfo();
        frCreationInf.FileName = @"C:\Program Files\Git\git-bash.exe";
        frCreationInf.Arguments = "--hide ./Quit.sh";
        frCreationInf.UseShellExecute = true;
        var process = new Process();
        process.StartInfo = frCreationInf;
        process.Start();
    }
}
