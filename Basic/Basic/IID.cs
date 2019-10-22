using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Basic
{
   public  interface IID
    {
        int ID { get; set;}
        string Directory { get; set; }
        string Prefix { get; set; }
        string Url { get; }
        string Path { get; }
        void ChangePrefix();
        void ResetPrefix();
        string TableToJson(DataTable DT);
        string RowToJson(DataRow DR);
        void SaveImage();
        void DeleteImage();
        HttpPostedFile FU { get; set;}
    }
}
