using System;
using System.IO;
using System.Drawing;
using System.Windows.Input;
using System.Xml;
using System.Text;

using System.Web;

namespace FileHelper
{
   
    public class file
    {
        private bool haspathbeenset = false;
        private string _path = "";
        public string path { 
            get
            {
                if (_path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
                    return _path;
                }
              
            }
            set
            {
                if (haspathbeenset == true)
                {
                    File.Move(_path, path);
                }
                _path = path;
            }
        }
     
        public file(string path, fctype type)
        {
            this._path = path;
            if (type == fctype.create)
            {
                File.Create(path);
            } else
            {

            }
            haspathbeenset = true;
        }
        public string text
        {
            get
            {
                string _text = File.ReadAllText(path);
                return _text;
            }
            set
            {
                File.AppendAllText(path, text);
            }
        } 
        public byte[] bytes { 
            get 
            {
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
 return File.ReadAllBytes(path);
                }
               
            }
            set
            {
                
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
                     File.WriteAllBytes(path, bytes);
                }
               
            }
        }
        public string name
        {
            get
            {
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
 FileInfo f = new FileInfo(path);
                return f.Name;
                }
               
            }
        }
        public string[] lines 
        {
            get
            {
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
return File.ReadAllLines(path);
                }
                
            }
            set
            {
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
 File.WriteAllLines(path, lines);
                }
               
            }
        }
        public long size { 
            get
            {
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                }
                else
                {
                    FileInfo f = new FileInfo(path);
                    return f.Length;
                }

            } 
        }

        public FileType type;

        public Image image {
            get
            {
                if (type == FileType.jpg || type == FileType.png || type == FileType.bmp)
                {
                    return Bitmap.FromFile(_path);
                }
                else
                {
                    throw new Exception("File is not an image file or filetype not set.");
                }
                
            }
        }
        
        public FileAttributes attributes { 
            get
            {
                if (path == "")
                {
                    throw new ArgumentException("Path is empty; file object has no set path.");

                } else
                {
  return File.GetAttributes(path);
                }
              

            }
        }
        public void Delete()
        {
            haspathbeenset = false;
            File.Delete(path);
            this.path = "";
            haspathbeenset = false;

        }
        public void reinitalize(string path, fctype type)
        {
            haspathbeenset = false;
            this.path = path;
            if (type == fctype.create)
            {
                File.Create(path);
            }
            else
            {
                
            }
            haspathbeenset = true;
        }
        
        
        public override string ToString()
        {
            return Path.GetFileName(path);
        }
        public void CopyTo(string path)
        {
            File.Copy(this.path, path);
        }
        public void Decrypt()
        {
            File.Decrypt(path);
        }
        public void Encrypt()
        {
            File.Encrypt(path);
        }
        public bool Exists()
        {
            if (this.name == "")
            {
                return false;
            } else
            {
                return true;
            }
        }
        
        
    }
    public enum FileType
    {
        exe,
        txt,
        png,
        jpg,
        bmp
    }
    public class folder
    {
        public string path = "";
        public file[] contents
        {
            get
            {
                string[] fcs = Directory.GetFiles(path);

                file[] tempc = new file[fcs.Length];
                int i = 0;

                foreach (string f in fcs)
                {
                    tempc[i] = new file(f, fctype.normal);
                    i++;
                }
                return tempc;
            }
            set
            {
                
                string[] fcs = Directory.GetFiles(path);
                
                foreach (file f in contents)
                {
                    f.path = path + "\\" + f.name;
                }
                foreach (string f in fcs)
                {
                    File.Delete(f);
                }

            }
        }
    }
    public enum fctype
    {
        create = 1,
        normal = 2
    }
    static class fms
    {
        //fms contains little code currently; the "file" and "folder" classes have the most often used System.IO methods built-in.
        static void convert(file f, string newfilepath)
        {
            string newpath = newfilepath;
            byte[] bytes = f.bytes;
            string path = f.path;
            f.Delete();
            file fn = new file(newfilepath, fctype.create);
            fn.bytes = bytes;
         
            f.reinitalize(fn.path, fctype.normal);
        }
    }
    
    namespace security
    {
        public enum encodings
        {
            reverse = 1,
            symbol = 2,
            random = 3,
            builtin = 4
        }
        public static class securityagent
        {
            static void encrypt(encodings e,file f)
            {
                if (e == encodings.builtin)
                {
                    File.Encrypt(f.path);
                }
            }
            static void decrypt(encodings e, file f)
            {
                if (e == encodings.builtin)
                {
                    File.Decrypt(f.path);
                }
            }
        }

    }
       
        
    
}
