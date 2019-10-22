using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.IO;
namespace Basic
{
    public abstract class ClassID <T>: IID, IAbstract<T>
    {
        IID IId;
        public ClassID(){ IId=this;}
        public int ID { get; set; }
        string IID.Directory { get; set; }
        string IID.Prefix { get ; set; }
        public string Url { get {

                IId.ResetPrefix();
                if(File.Exists(IId.Path)){ return "Imagenes/" + IId.Directory + "/" + IId.Prefix +this.ID+ ".jpg"; }
                IId.ChangePrefix();
                if (File.Exists(IId.Path)) { return "Imagenes/" + IId.Directory + "/" + IId.Prefix +this.ID+ ".jpg"; }
                IId.ResetPrefix();
                return "Imagenes/" + IId.Directory + "/" + IId.Prefix+ "default.jpg"; }
            }

        string IID.Path
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "imagenes\\" + IId.Directory + "\\" + IId.Prefix + this.ID + ".jpg"; }
        }

        HttpPostedFile IID.FU { get ; set; }

        public abstract string Add();
        public abstract string Erase();
        public abstract string Find();
        public abstract List<T> List();
        public abstract string ListToJson();
        public abstract string Modify();

        void IID.ChangePrefix()
        {
            if (IId.Prefix.EndsWith("_")) IId.Prefix = IId.Prefix.Remove(IId.Prefix.Length - 1);
            else IId.Prefix += "_";
        }

        void IID.DeleteImage()
        {
            IId.ResetPrefix();
            if (File.Exists(IId.Path)) { File.Delete(IId.Path);IId.ChangePrefix(); return; }
            IId.ChangePrefix();
            if (File.Exists(IId.Path)) { File.Delete(IId.Path); IId.ChangePrefix(); return; }
            IId.ResetPrefix();
        }

        void IID.ResetPrefix()
        {
            if (IId.Prefix.EndsWith("_")) IId.Prefix = IId.Prefix.Remove(IId.Prefix.Length - 1);
        }

       public string RowToJson(DataRow DR)
        {
            string Q = "\"";
            string Text = "{";
            for (int i = 0; i < DR.Table.Columns.Count; i++)
            {
                Text += Q + DR.Table.Columns[i].ColumnName + Q + ":" + Q + DR[i].ToString()+Q+",";
            }
            if(IId.Directory !=null)
            {
                int OlsID = this.ID;
                this.ID = int.Parse(DR["ID"].ToString());
                Text += Q + "URL" + Q + ":" + Q + IId.Url + Q + ",";

                this.ID = OlsID;
            }
            Text = Text.Remove(Text.Length - 1) + "}";
            return Text;
        }

        void IID.SaveImage()
        {
            if (IId.FU == null) return;
            if (IId.FU.FileName == "") return;
            IId.DeleteImage();
            IId.FU.SaveAs(IId.Path);
        }

        public string TableToJson(DataTable DT)
        {
            if (DT.Rows.Count == 0) return "[]";
            string Text = "[";
            foreach(DataRow dr in DT.Rows)
            {

                Text += IId.RowToJson(dr) + ",";

            }
            Text = Text.Remove(Text.Length - 1)+"]";
            return Text;
        }
    }
}
