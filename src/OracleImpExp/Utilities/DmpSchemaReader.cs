using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OracleImpExp.Utilities
{
    /// <summary>
    /// 从Oracle的DMP文件中读取用户名
    /// </summary>
    public static class DmpSchemaReader
    {
        /// <summary>
        /// 最大读取字节数 50M
        /// </summary>
        private const long MAX_READSIZE = 52428800;

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="readSize">读取字节数</param>
        /// <returns></returns>
        public static string Read(string filePath, long readSize = MAX_READSIZE)
        {
            string result = string.Empty;

            using (Stream stream = File.OpenRead(filePath))
            {
                stream.Seek(0, SeekOrigin.Begin);

                // 应该回退的字节数
                int rollbackSize;
                // 缓冲字节数
                int bufferSize = 1024;
                // 已读字节数
                long totalCount = 0;

                while (totalCount < stream.Length || totalCount < MAX_READSIZE)
                {
                    // 本次读取字节数
                    int current_read_size = (stream.Length - totalCount >= bufferSize) ? bufferSize : (int)(stream.Length - totalCount);
                    // 起始索引
                    long begin_index = stream.Length - (totalCount + current_read_size);

                    stream.Position = begin_index;

                    byte[] buffer = new byte[current_read_size];
                    totalCount += stream.Read(buffer, 0, buffer.Length);

                    if (TryGetSchema(buffer, out result, out rollbackSize))
                        break;
                    else
                        stream.Position += rollbackSize;
                }
            }

            return result;
        }

        private static readonly byte[] FALG_SCHEMA_LIST = new byte[] { 0x53, 0x43, 0x48, 0x45, 0x4D, 0x41, 0x5F, 0x4C, 0x49, 0x53, 0x54 };
        private static readonly byte[] FALG_SCHEMA_LIST_OVERTURN = new byte[] { 0x54, 0x53, 0x49, 0x4C, 0x5F, 0x41, 0x4D, 0x45, 0x48, 0x43, 0x53 };

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="schema"></param>
        /// <param name="rollbackSize">应该回退的字节数</param>
        /// <returns></returns>
        private static bool TryGetSchema(byte[] buffer, out string schema, out int rollbackSize)
        {
            schema = string.Empty;
            rollbackSize = 0;

            bool checking = false;
            int checkingStep = 0;

            for (int i = buffer.Length - 1; i > 0; i--)
            {
                // 判断是否进入检查流程
                if (buffer[i] == FALG_SCHEMA_LIST_OVERTURN[0] && !checking)
                {
                    checking = true;
                    checkingStep = 0;
                }

                if (checking)
                {
                    if (buffer[i] == FALG_SCHEMA_LIST_OVERTURN[checkingStep])
                    {
                        checkingStep++;

                        // 因该批次字节流不足导致的检查未能正常进行，记录应该回退的字节数
                        if (i == buffer.Length) rollbackSize = FALG_SCHEMA_LIST_OVERTURN.Length - checkingStep;
                    }
                    else
                        checking = false;

                    if (checkingStep == FALG_SCHEMA_LIST_OVERTURN.Length)
                    {
                        schema = ResoveSchema(buffer, i);

                        break;
                    }
                }
            }

            return !string.IsNullOrEmpty(schema);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private static string ResoveSchema(byte[] buffer, int startIndex)
        {
            List<byte> datas = new List<byte>();

            bool collect = false;
            for (int i = startIndex; i < buffer.Length; i++)
            {
                if (buffer[i] == 0x27 && !collect)
                {
                    collect = true;
                    continue;
                }

                if (buffer[i] == 0x27 && collect)
                    break;

                if (collect) datas.Add(buffer[i]);
            }

            return datas.Count > 0 ? Encoding.UTF8.GetString(datas.ToArray()) : string.Empty;
        }
    }
}
