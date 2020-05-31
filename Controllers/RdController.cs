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
                String alias = "docs_rd";
                TempData["alias"] = alias;
                TempData["html"] = FileTree.RenderDirectoryTree(sessionId, alias, null);
                result = PartialView(sessionId);
            }
            catch (Exception e) { result += e.ToString(); }
            return result;
            /*
            String alias = "docs_rd";
            if (Request.Form.Count > 1) // session_id, cmd
            {
                Dictionary<String, String> pars = new Dictionary<String, String>(Request.Form.Count);
                for (int i = 0; i < Request.Form.Count; i++)
                {
                    pars.Add(Request.Form.Keys[i], Request.Form.GetValues(i)[0]);
                }
                String cmd = pars["cmd"];
                if (!String.IsNullOrWhiteSpace(cmd))
                {
                    switch (cmd)
                    {
                        case "GetFsInfo":
                            String path = Utility.UnEscape(pars["path"]);
                            String type = pars["type"]; // или "dir" или "file"
                            DataGridView fileInfo = RdModel.GetFsInfo(alias, path, type);
                            result = PartialView("~/Views/Shared/DataGridView/Edit.cshtml", fileInfo);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Guid sessionId = new Guid();
                TempData["alias"] = alias;
                TempData["html"] = FileTree.RenderDirectoryTree(sessionId, alias, null);
                result = View("Index");
            }
            return result;
*/
        }
        public Object Fs(String cmd, String alias, String path)
        {
            Object result = $"DocsRd.Controllers.RdController.GetDirInf('{cmd}', '{alias}', '{path}')<br />";
            String html;
            path = Utility.UnEscape(path);
            switch (cmd)
            {
                case "GetFileInfo":
                    html = "GetFileInfo: '" + path + "'";
                    //html = FileTree.GetFileInfo(alias, path);
                    result = html;
                    break;
                case "GetDirectoryInfo":
                    Guid sessionId = new Guid();
                    html = FileTree.RenderDirectoryTree(sessionId, alias, path);
                    result = html;
                    break;
                case "DownloadFile":
                    FileData fd = FileData.GetFile(alias, path);
                    result = File(fd.Contents, fd.ContentType, fd.Name);
                    break;
                default:
                    break;
            }
            return result;
        }
        public Object Test(String cmd, String alias, String path, String type)
        {
            Object result = $"DocsRd.Controllers.RdController.Test('{cmd}', '{alias}', '{Utility.UnEscape(path)}', '{type}')<br />";
            return result;
        }
    }
}
