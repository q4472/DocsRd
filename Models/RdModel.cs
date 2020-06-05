using System;
using System.Collections;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace DocsRd.Models
{
    public class RdModel
    {
        public static DataTable GetFsInfo(String path)
        {
            DataTable dt = null;
            String link = "{docs_rd}" + path;
            Byte[] hash = (MD5.Create()).ComputeHash(Encoding.Unicode.GetBytes(link));
            StringBuilder sb = new StringBuilder();
            foreach (Byte b in hash) { sb.AppendFormat("{0:x2}", b); }
            String id = sb.ToString();
            dt = Data.Fs.GetFsInfo(id);
            return dt;
        }
        public static void SetFsInfo(Hashtable data)
        {
            Data.Fs.SetFsInfo(data);
        }
    }
}