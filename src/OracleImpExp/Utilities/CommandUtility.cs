using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OracleImpExp.Utilities
{
    /// <summary>
    /// 控制台辅助类
    /// </summary>
    public static class CommandUtility
    {
        /// <summary>
        /// 在控制台中执行命令。（阻塞、一次性读取输出）
        /// </summary>
        /// <param name="commonds">命令</param> 
        /// <param name="workingDirectory">工作目录</param>
        /// <param name="outputEncoding">输出编码</param>
        /// <returns></returns>
        public static string Execute(List<string> commonds, string workingDirectory = "", Encoding outputEncoding = null)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"cmd.exe";
            process.StartInfo.Arguments = "/k";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            if (outputEncoding != null)
                process.StartInfo.StandardOutputEncoding = outputEncoding;
            if (!string.IsNullOrWhiteSpace(workingDirectory))
                process.StartInfo.WorkingDirectory = workingDirectory;

            string output = string.Empty;

            if (process.Start())
            {
                StreamWriter input = process.StandardInput;
                input.AutoFlush = true;

                foreach (string command in commonds)
                {
                    input.WriteLine(command);
                }
                input.WriteLine(@"exit");

                output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
            }

            return output;
        }

        /// <summary>
        /// 在控制台中执行命令。（阻塞、一次性读取输出）
        /// </summary>
        /// <param name="command">命令</param> 
        /// <param name="workingDirectory">工作目录</param>
        /// <param name="outputEncoding">输出编码</param>
        /// <returns></returns>
        public static string Execute(string command, string workingDirectory = "", Encoding outputEncoding = null)
        {
            return Execute(new List<string> { command }, workingDirectory, outputEncoding);
        }

        /// <summary>
        /// 在控制台中执行命令（同步读取），异步返回输出
        /// </summary>
        /// <param name="commonds">命令</param>
        /// <param name="callBack">输出回调</param>
        /// <param name="workingDirectory">工作目录</param>
        /// <param name="outputEncoding">输出编码</param>
        public static void Execute3(List<string> commonds, Action<string> callBack = null, string workingDirectory = "", Encoding outputEncoding = null)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"cmd.exe";
            process.StartInfo.Arguments = "/k";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            if (outputEncoding != null)
                process.StartInfo.StandardOutputEncoding = outputEncoding;
            if (!string.IsNullOrWhiteSpace(workingDirectory))
                process.StartInfo.WorkingDirectory = workingDirectory;

            process.Start();

            StreamWriter input = process.StandardInput;
            StreamReader ouput = process.StandardOutput;

            input.AutoFlush = true;
            foreach (string command in commonds)
            {
                input.WriteLine(command);
            }
            input.WriteLine(@"exit");

            while (!ouput.EndOfStream)
            {
                callBack?.Invoke(ouput.ReadLine());
            }
        }

        /// <summary>
        /// 在控制台中执行命令（同步），异步返回输出
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="callBack">输出回调</param>
        /// <param name="workingDirectory">工作目录</param>
        /// <param name="outputEncoding">输出编码</param>
        public static void Execute3(string command, Action<string> callBack = null, string workingDirectory = "", Encoding outputEncoding = null)
        {
            Execute3(new List<string> { command }, callBack, workingDirectory, outputEncoding);
        }

        /// <summary>
        /// 在控制台中执行命令。（阻塞、异步读取输出）
        /// </summary>
        /// <param name="commonds">命令</param>
        /// <param name="callBack">输出回调</param>
        /// <param name="workingDirectory">工作目录</param>
        /// <param name="outputEncoding">输出编码</param>
        public static void Execute2(List<string> commonds, Action<string> callBack = null, string workingDirectory = "", Encoding outputEncoding = null)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            if (outputEncoding != null)
                process.StartInfo.StandardOutputEncoding = outputEncoding;
            if (!string.IsNullOrWhiteSpace(workingDirectory))
                process.StartInfo.WorkingDirectory = workingDirectory;
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null) callBack?.Invoke(e.Data);
            };
            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null) callBack?.Invoke(e.Data);
            };

            if (process.Start())
            {
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                StreamWriter input = process.StandardInput;
                input.AutoFlush = true;

                foreach (string command in commonds)
                {
                    input.WriteLine(command);
                }
                input.WriteLine(@"exit");

                process.WaitForExit();
            }
        }

        /// <summary>
        /// 在控制台中执行命令。（阻塞、异步读取输出）
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="callBack">输出回调</param>
        /// <param name="workingDirectory">工作目录</param>
        /// <param name="outputEncoding">输出编码</param>
        public static void Execute2(string command, Action<string> callBack = null, string workingDirectory = "", Encoding outputEncoding = null)
        {
            Execute2(new List<string> { command }, callBack, workingDirectory, outputEncoding);
        }
    }
}
