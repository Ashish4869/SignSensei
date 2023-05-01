using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public static class StartBackEnd 
{
    public static void RunBackEnd()
    {
        RunAI();
    }

    async static void RunAI()
    {
        await Task.Run(() =>
            {
                ProcessStartInfo frCreationInf = new ProcessStartInfo();
                frCreationInf.FileName = @"C:\Program Files\Git\git-bash.exe";
                frCreationInf.Arguments = "--hide ./Test.sh &";
                frCreationInf.UseShellExecute = false;
                var process = new Process();
                process.StartInfo = frCreationInf;
                process.Start();
            });
       
        //process.WaitForExit();
    }
}
