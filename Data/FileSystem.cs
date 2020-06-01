using Nskd;
using System;
using System.Data;

namespace DocsRd.Data
{
    public class Fs
    {
        private static String dataServicesHost = "127.0.0.1"; // localhost

        public static DataSet GetDirectoryInfo(Guid sessionId, String path)
        {
            DataSet ds = null;
            RequestPackage rqp = new RequestPackage();
            rqp.SessionId = sessionId;
            rqp.Command = "GetDirectoryInfo";
            rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("alias", "docs_rd"),
                    new RequestParameter("path", path)
                };
            ResponsePackage rsp = ExecuteInFs(rqp);
            ds = rsp.Data;
            return ds;
        }
        public static Byte[] GetFileContents(Guid sessionId, String path = null)
        {
            Byte[] contents = null;
            RequestPackage rqp = new RequestPackage();
            rqp.SessionId = sessionId;
            rqp.Command = "GetFileContents";
            rqp.Parameters = new RequestParameter[2];
            rqp.Parameters[0] = new RequestParameter("alias", "docs_rd");
            rqp.Parameters[1] = new RequestParameter("path", path);
            ResponsePackage rsp = ExecuteInFs(rqp);
            if (rsp.Data != null)
            {
                if (rsp.Data.Tables.Count > 0)
                {
                    DataTable dt = rsp.Data.Tables[0];
                    if (dt.Columns.Count > 0)
                    {
                        if ((dt.Columns[0].ColumnName == "contents") && (dt.Columns[0].DataType == typeof(String)))
                        {
                            if (dt.Rows.Count > 0)
                            {
                                String base64String = dt.Rows[0][0] as String;
                                if (!String.IsNullOrWhiteSpace(base64String))
                                {
                                    contents = System.Convert.FromBase64String(base64String);
                                }
                            }
                        }
                    }
                }
            }
            return contents;
        }
        public static ResponsePackage ExecuteInFs(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11005/");
            return rsp;
        }
        public static DataSet Execute(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11002/");
            return rsp.Data;
        }
        public static DataTable GetFirstTable(DataSet ds)
        {
            DataTable dt = null;
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public static Object GetScalar(DataSet ds)
        {
            Object r = null;
            DataTable dt = GetFirstTable(ds);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count >= 0)
            {
                r = dt.Rows[0][0];
            }
            return r;
        }
        public static DataTable GetFsInfoCommon(String fileId, String link, String type = null)
        {
            RequestPackage rqp = new RequestPackage();
            rqp.Command = "[dbo].[file_info_get]";
            rqp.Parameters = new RequestParameter[] {
                new RequestParameter("file_id", fileId ),
                new RequestParameter("link", link ),
                new RequestParameter("type", type )
            };
            DataTable dt = GetFirstTable(Execute(rqp));
            return dt;
        }
    }
}