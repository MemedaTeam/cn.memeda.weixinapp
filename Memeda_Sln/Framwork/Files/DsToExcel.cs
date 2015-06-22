using System.Data;
using System.Text;
using System.Web;

namespace Framework.File
{
    /// <summary>
    /// 数据导出
    /// </summary>
    public class DsToExcel
    {
        /// <summary>
        /// DataSet导出Excel
        /// </summary>
        /// <param name="arrTitle">列标题，若为null，则直接取dataset列标题</param>
        /// <param name="ds">要导出的DataSet</param>
        /// <param name="fileName">Excel文件名，不需要传入扩展名</param>
        public static void CreateExcel(string[] arrTitle, DataSet ds, string fileName)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(" <html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            strb.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            strb.Append("xmlns=\"http://www.w3.org/TR/REC-html40\"");
            strb.Append(" <head> <meta http-equiv='Content-Type' content='text/html; charset=gb2312'>");
            strb.Append(" <style>");
            strb.Append(".xl26");
            strb.Append(" {mso-style-parent:style0;");
            strb.Append(" font-family:\"Times New Roman\", serif;");
            strb.Append(" mso-font-charset:0;");
            strb.Append(" mso-number-format:\"@\";}");
            strb.Append(" </style>");
            strb.Append(" <xml>");
            strb.Append(" <x:ExcelWorkbook>");
            strb.Append("  <x:ExcelWorksheets>");
            strb.Append("  <x:ExcelWorksheet>");
            strb.Append("    <x:Name>Sheet1 </x:Name>");
            strb.Append("    <x:WorksheetOptions>");
            strb.Append("    <x:DefaultRowHeight>285 </x:DefaultRowHeight>");
            strb.Append("    <x:Selected/>");
            strb.Append("    <x:Panes>");
            strb.Append("      <x:Pane>");
            strb.Append("      <x:Number>3 </x:Number>");
            strb.Append("      <x:ActiveCol>1 </x:ActiveCol>");
            strb.Append("      </x:Pane>");
            strb.Append("    </x:Panes>");
            strb.Append("    <x:ProtectContents>False </x:ProtectContents>");
            strb.Append("    <x:ProtectObjects>False </x:ProtectObjects>");
            strb.Append("    <x:ProtectScenarios>False </x:ProtectScenarios>");
            strb.Append("    </x:WorksheetOptions>");
            strb.Append("  </x:ExcelWorksheet>");
            strb.Append("  <x:WindowHeight>6750 </x:WindowHeight>");
            strb.Append("  <x:WindowWidth>10620 </x:WindowWidth>");
            strb.Append("  <x:WindowTopX>480 </x:WindowTopX>");
            strb.Append("  <x:WindowTopY>75 </x:WindowTopY>");
            strb.Append("  <x:ProtectStructure>False </x:ProtectStructure>");
            strb.Append("  <x:ProtectWindows>False </x:ProtectWindows>");
            strb.Append(" </x:ExcelWorkbook>");
            strb.Append(" </xml>");
            strb.Append("");
            strb.Append(" </head> <body> <table align=\"center\" style='border-collapse:collapse;table-layout:fixed'> <tr>");

            if (ds.Tables.Count > 0)
            {
                //写列标题  
                if (arrTitle != null && arrTitle.Length > 0)
                {
                    foreach (string strCol in arrTitle)
                    {
                        strb.Append(" <td> <b>" + strCol + " </b> </td>");
                    }
                    strb.Append(" </tr>");
                }
                else
                {
                    int columncount = ds.Tables[0].Columns.Count;
                    for (int columi = 0; columi < columncount; columi++)
                    {
                        strb.Append(" <td> <b>" + ds.Tables[0].Columns[columi] + " </b> </td>");
                    }
                    strb.Append(" </tr>");
                }

                //写数据     
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strb.Append(" <tr>");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        strb.Append(" <td class='xl26'>" + ds.Tables[0].Rows[i][j].ToString() + " </td>");
                    }
                    strb.Append(" </tr>");
                }
            }
            strb.Append(" </body> </html>");

            var htc = HttpContext.Current;
            htc.Response.Clear();
            htc.Response.Buffer = true;
            htc.Response.Charset = "GB2312";
            htc.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ".xls");
            htc.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文   
            htc.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。   
            //htc.EnableViewState = false;
            htc.Response.Write(strb);
            htc.Response.End();
        }
    }
}