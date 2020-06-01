using Microsoft.AspNetCore.Mvc;
using System;
using DocsRd.Models;
using Nskd;
using System.Text;

namespace DocsRd.Controllers
{
    public class RdController : Controller
    {
        public Object Index()
        {
            Object result = "DocsRd.Controllers.RdController.Index()<br />";
            try
            {
                Guid.TryParse(Request.Form["sessionId"], out Guid sessionId);
                TempData["html"] = FileTree.RenderDirectoryTree(sessionId, null);
                result = PartialView(sessionId);
            }
            catch (Exception e) { result += e.ToString(); }
            return result;
        }
        public Object Fs(String cmd, String path)
        {
            path = Utility.UnEscape(path);
            Object result = $"DocsRd.Controllers.RdController.GetDirInf('{cmd}', '{path}')<br />";
            String html;
            switch (cmd)
            {
                case "GetFileInfo":
                    html = "GetFileInfo: '" + path + "'";
                    //html = FileTree.GetFileInfo(path);
                    result = html;
                    break;
                case "GetDirectoryInfo":
                    Guid sessionId = new Guid();
                    html = FileTree.RenderDirectoryTree(sessionId, path);
                    result = html;
                    break;
                case "DownloadFile":
                    FileData fd = FileData.GetFile(path);
                    result = File(fd.Contents, fd.ContentType, fd.Name);
                    break;
                default:
                    break;
            }
            return result;
        }
        public Object GetFsInfo(String cmd, String path, String type)
        {
            path = Utility.UnEscape(path);
            Object result = $"DocsRd.Controllers.RdController.Test('{cmd}', '{path}', '{type}')<br />";
            // type или "dir" или "file"
            RdModel.GetFsInfo(path, type);
            //DataGridView fileInfo = RdModel.GetFsInfo(path, type);
            //result = PartialView("~/Views/Shared/DataGridView/Edit.cshtml", fileInfo);
            return result;
        }
    }
}
