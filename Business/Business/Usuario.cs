using System;
using System.Collections.Generic;
using Basic;
using System.Security.Cryptography;
using System.Text;

namespace Business
{
    public class Usuario : ClassIDLogin<Usuario>,IUsuario
    {
        ISingletonGenrericLogin<Usuario> ISGL = Singleton.GetInstance;
        public Usuario()
        {
            IID IID = this;
            IID.Prefix = "Usuario";
            IID.Directory = "Usuarios";
            
        }
        public string Nombre { get ; set ; }
        public int DNI { get; set; }
        public DateTime FechaNacimiento { get; set ; }
        public string Direccion { get ; set ; }
        public string Mail { get ; set ; }
        public string Password { get ; set ; }
        public DateTime FechaAceptacion { get ; set ; }
        public string Rol { get ; set ; }
        public string FindByMail()
        {
            ISGL.FindByMail(this);
            return ISGL.Find(this);
        }

        public override string Add()
        {
            if(this.Password!="")
            {
                this.Password = Encriptar(this.Password);
            }
            ISGL.MailExists(this);
            ISGL.DNIExists(this);
            ISGL.Add(this);
            IID IID = this;
            IID.SaveImage();
            return this.ListToJson();
        }

        public string Encriptar(string password)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(password);
            byte[] result;

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();

            result = sha.ComputeHash(data);


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {

                // Convertimos los valores en hexadecimal
                // cuando tiene una cifra hay que rellenarlo con cero
                // para que siempre ocupen dos dígitos.
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }

            //Devolvemos la cadena con el hash en mayúsculas para que quede más chuli 🙂
            return sb.ToString().ToUpper();
        }

        public override string Erase()
        {
            ISGL.Erase(this);
            IID IID = this;
            IID.DeleteImage();
            return this.ListToJson();

        }

        public override string Find()
        {
            return ISGL.Find(this);
            
        }

        public override List<Usuario> List()
        {
            return ISGL.List(this);
        }

        public override string ListToJson()
        {
            return ISGL.ListToJson(this);
        }

        public override string Login()
        {
            this.Password = Encriptar(this.Password);
            return ISGL.Login(this);
        }

        public override string Modify()
        {
            if(this.Password!="")
            {
                this.Password = Encriptar(this.Password);
            }
            ISGL.Modify(this);
            IID IID = this;
            IID.SaveImage();
            return this.ListToJson();
        }
    }
}
