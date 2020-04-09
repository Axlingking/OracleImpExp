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
                    if (current_read_size <= 0) break;

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

        // ASCII: schemas=ISPM4 
        //   HEX: 73 63 68 65 6D 61 73 3D 49 53 50 4D 34 20

        // ASCII: schemas=
        //   HEX: 73 63 68 65 6D 61 73 3D
        private static readonly byte[] HEX_SCHEMAS = new byte[] { 0x73, 0x63, 0x68, 0x65, 0x6D, 0x61, 0x73, 0x3D };

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
            bool hit = false;
            List<byte> datas = new List<byte>();

            for (int i = 0; i < buffer.Length; i++)
            {
                // 是否命中 schemas= 标识
                if (!hit)
                {
                    // 判断是否进入检查流程
                    if (!checking && buffer[i] == HEX_SCHEMAS[0])
                    {
                        checking = true;
                        checkingStep = 0;
                    }

                    if (checking)
                    {
                        if (buffer[i] == HEX_SCHEMAS[checkingStep])
                        {
                            checkingStep++;
                        }
                        else
                            checking = false;

                        if (checkingStep == HEX_SCHEMAS.Length)
                        {
                            hit = true;
                        }

                        // 因该批次字节流不足导致的检查未能正常进行，记录应该回退的字节数
                        if (i == buffer.Length) rollbackSize = 256;
                    }
                }
                else
                {
                    // 检测到空格位置
                    if (buffer[i] == 0x20)
                    {
                        schema = Encoding.UTF8.GetString(datas.ToArray());
                        break;
                    }

                    datas.Add(buffer[i]);

                    // 因该批次字节流不足导致的检查未能正常进行，记录应该回退的字节数
                    if (i == buffer.Length) rollbackSize = 256;
                }
            }

            return !string.IsNullOrEmpty(schema);
        } 
    }
}
