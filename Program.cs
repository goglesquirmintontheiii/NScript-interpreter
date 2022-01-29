using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CScolors;
using NS;
using AppLogger;
using FileHelper;
using Pastel;
using System.Drawing;

namespace luacompilerV1
{
    class Program
    {
        
        static void Main(string[] args)
        {

            

            //VARIABLES

            bool displaydebug = false;
            bool readingfile = false;
            string nil = "";
            Stringval[] stringvals = { };
            Function[] Functions = { };
            file[] codefiles = { };
            Numval[] intvals = { };
            string currentcode = "";
            string argument = "";
            string returned = "";
            bool infunc = false;
            string currentname = "";
            string currentiofile = "";
            string currentpath = "";
            string input = "";

            //METHODS

            string editline()
            {
               
                string cur = "";
                bool end = false;
                bool inq = false;
                bool com = false;
                while (!end)
                {
                    ConsoleKeyInfo ck = Console.ReadKey();
                    Console.Clear();
                    if (ck.Key == ConsoleKey.Enter)
                    {
                        end = true;                      
                    }
                    else if (ck.Key == ConsoleKey.Backspace)
                    {
                        Console.Clear();
                        string nws = "";
                        char[] curchar = cur.ToCharArray();
                        int ind = 0;
                        foreach (char c in curchar)
                        {
                            if (ind != cur.Length - 1)
                            {
                                nws = nws + c.ToString();
                            }
                            ind++;
                        }
                        cur = nws;
                        
                    }
                    else
                    {
                        
                        string numbers = "1234567890";
                        string ch = ck.KeyChar.ToString();
                        
                        if (ch == "\"")
                        {
                            cur = cur + ch.Pastel(Color.FromArgb(100, 200, 200));
                            inq = !inq;
                        }
                        else
                        {
                            if (numbers.Contains(ch) && !inq)
                            {
                                if (com)
                                {
                                    cur = cur + ch.Pastel(Color.FromArgb(100, 100, 100));
                                }
                                else
                                {
                                    cur = cur + ch.Pastel(Color.FromArgb(100, 100, 200));
                                }
                                
                            }
                            else if (inq)
                            {
                                if (com)
                                {
                                    cur = cur + ch.Pastel(Color.FromArgb(100, 100, 100));
                                }
                                else
                                {
                                    cur = cur + ch.Pastel(Color.FromArgb(100, 200, 200));

                                }
                               
                            }
                            else
                            {
                                if (com)
                                {
                                    cur = cur + ch.Pastel(Color.FromArgb(100, 100, 100));
                                }
                                else
                                {
                                    cur = cur + ch;
                             
                                }

                            }
                        }
                        
                    }
                    string[] keywords = { "end ", "if", "then ", "else " };
                    foreach (string k in keywords)
                    {
                        cur = cur.Replace(k,k.Pastel(Color.FromArgb(200,150,75)));
                    }
                    if (cur.StartsWith("##"))
                    {
                        cur = cur.Pastel(Color.FromArgb(100, 100, 100));
                        com = true;
                    }
                    Console.Write(cur);
                }
                Console.Write("\n");
                return cur;
            }
            Stringval[] stringinsertval(Stringval[] array, Stringval val)
            {
                int n = array.Length;
                Stringval[] arr = array;
               

                // initial array of size 10 
               // for (i = 0; i < n; i++)
               //     arr[i] = i + 1;

                // print the original array 
               // for (i = 0; i < n; i++)
                 //   Console.Write(arr[i] + " ");
               // Console.WriteLine();

                // element to be inserted 
                Stringval x = val;

                // position at which element  
                // is to be inserted 
                int pos = array.Length;

                // create a new array of size n+1 
                Stringval[] newarr = new Stringval[n + 1];

                // insert the elements from the  
                // old array into the new array 
                // insert all elements till pos 
                // then insert x at pos 
                // then insert rest of the elements 
                int on = 0;
                foreach (Stringval i in newarr)
                {
                    if (on == n)
                    {
                        newarr[on] = val;
                    }
                    else
                    {
                        arr[on] = i;
                    }
                    on++;
                }
                return newarr;
            }
            Numval[] intinsertval(Numval[] array, Numval val)
            {
                int n = array.Length;
                Numval[] arr = array;


                // initial array of size 10 
                // for (i = 0; i < n; i++)
                //     arr[i] = i + 1;

                // print the original array 
                // for (i = 0; i < n; i++)
                //   Console.Write(arr[i] + " ");
                // Console.WriteLine();

                // element to be inserted 
                Numval x = val;

                // position at which element  
                // is to be inserted 
                int pos = array.Length;

                // create a new array of size n+1 
                Numval[] newarr = new Numval[n + 1];

                // insert the elements from the  
                // old array into the new array 
                // insert all elements till pos 
                // then insert x at pos 
                // then insert rest of the elements 
                int on = 0;
                foreach (Numval i in newarr)
                {
                    if (on == n)
                    {
                        newarr[on] = val;
                    }
                    else
                    {
                        arr[on] = i;
                    }
                    on++;
                }
                return newarr;
            }
            void setstrval(string name, string value)
            {
                int index = 0;
                int ci = 0;
                foreach (Stringval s in stringvals)
                {
                    if (s.name == name.Replace("\\", ""))
                    {
                        index = ci; 
                    }
                    ci++;
                }
                if (ci == 0)
                {
                    Console.WriteLine("Variable \"" + name.Replace("\\", "") + "\" does not exist! Did you make a typo?");
                }
                else
                {
                    stringvals[index] = new Stringval(name.Replace("\\", ""), value);
                }
            }
            string[] inserttoarray(string[] array, string val)
            {
                int n = array.Length;
                string[] arr = array;


                // initial array of size 10 
                // for (i = 0; i < n; i++)
                //     arr[i] = i + 1;

                // print the original array 
                // for (i = 0; i < n; i++)
                //   Console.Write(arr[i] + " ");
                // Console.WriteLine();

                // element to be inserted 
                string x = val;

                // position at which element  
                // is to be inserted 
                int pos = array.Length;

                // create a new array of size n+1 
                string[] newarr = new string[n + 1];

                // insert the elements from the  
                // old array into the new array 
                // insert all elements till pos 
                // then insert x at pos 
                // then insert rest of the elements 
                int on = 0;
                foreach (string i in arr)
                {
                    
                        newarr[on] = i;
                        
                    on++;
                }
                newarr[n] = val;
                return newarr;
            }
            int getpos(string name, string array) 
            {
                int pos = 0;
                bool found = false;
                int onpos = 0;
                if (array == "all")
                {
                    foreach (Numval n in intvals)
                    {
                        if (n.name == name)
                        {
                            found = true;
                            pos = onpos;
                        }
                        onpos++;
                    }
                    onpos = 0;
                    foreach (Stringval n in stringvals)
                    {
                        if (n.name == name)
                        {
                            found = true;
                            pos = onpos;
                        }
                        onpos++;
                    }
                } else if (array == "strings")
                {
                    foreach (Stringval n in stringvals)
                    {
                        if (n.name == name)
                        {
                            found = true;
                            pos = onpos;
                        }
                        onpos++;
                    }
                } else if (array == "nums")
                {
                    foreach (Numval n in intvals)
                    {
                        if (n.name == name)
                        {
                            found = true;
                            pos = onpos;
                        }
                        onpos++;
                    }
                }
                return pos;
            }
            string[] tostringarrayfromchar(char[] obj)
            {
                string[] newarr = new string[obj.Length];
                int pos = 0;
                foreach (char o in obj)
                {
                    newarr[pos] = o.ToString();
                    pos++;
                }
                return newarr;
            }
            string[] tostringarray(object[] obj)
            {
                string[] newarr = new string[obj.Length];
                int pos = 0;
                foreach (object o in obj)
                {
                    newarr[pos] = o.ToString();
                    pos++;
                }
                return newarr;
            }
            Function[] insertfunc(Function[] array, Function val)
            {
                int n = array.Length;
                Function[] arr = array;


                // initial array of size 10 
                // for (i = 0; i < n; i++)
                //     arr[i] = i + 1;

                // print the original array 
                // for (i = 0; i < n; i++)
                //   Console.Write(arr[i] + " ");
                // Console.WriteLine();

                // element to be inserted 
                Function x = val;

                // position at which element  
                // is to be inserted 
                int pos = array.Length;

                // create a new array of size n+1 
                Function[] newarr = new Function[n + 1];

                // insert the elements from the  
                // old array into the new array 
                // insert all elements till pos 
                // then insert x at pos 
                // then insert rest of the elements 
                int on = 0;
                foreach (Function i in newarr)
                {
                    if (on == n)
                    {
                        newarr[on] = val;
                    }
                    else
                    {
                        arr[on] = i;
                    }
                    on++;
                }
                return newarr;
            }
            string withoutpar(string str)
            {
                string res = ""; // result
                char[] chars = str.ToCharArray();
                bool ip = false; // in parinthesis 
                int ex = 0; // closing parinthesis exceptions
                bool ignore = false; // ignore parenthesis if in a string
                try
                {
                    foreach (char c in chars)
                    {
                        if (c.ToString() == "(")
                        {
                            if (!ip && !ignore)
                            {
                                ip = true;
                            }
                            else if (!ignore)
                            {
                                ex++;
                            }
                        }
                        else if (c.ToString() == ")" && !ignore)
                        {
                            if (ex == 0)
                            {
                                ip = false;
                                res += ")";
                            }
                            else
                            {
                                ex--;
                            }
                        }
                        else if (c.ToString() == "\"")
                        {
                            ignore = !ignore;
                        }
                        if (ip)
                        {
                            res += c.ToString();
                        }

                    }
                    string[] debuginfo =
                    {
                    "start:" + str,
                    "result:" + res
                };
                    //writedebuginfo(debuginfo);
                    return str.Replace(res, ""); // replace the parinthesis and whats in them and then return the new string
                }
                catch
                {
                    Console.WriteLine("Error. Expected \"(\" when looking for function call. Code compiling will be continued but may encounter further errors.");
                    return str;
                }
                
              
            }
            string inpar(string str)
            {
                char[] split = str.ToCharArray();
                string print = "";
                bool foundchar = false;
                bool closed = false;
                int exceptions = 0;
                bool fm = false;
                foreach (char i in split)
                {
                    if (i != Convert.ToChar("(") && foundchar && i != Convert.ToChar(")"))
                    {
                        print = print + Convert.ToString(i);
                    }
                    if (i == Convert.ToChar(")"))
                    {

                        if (exceptions > 0)
                        {
                            exceptions--;
                        }
                        else
                        {
                            foundchar = false;
                            closed = true;
                        }
                     

                    }
                    if (i == Convert.ToChar("("))
                    {

                        closed = false;
                        foundchar = true;
                        if (fm == true)
                        {
                            exceptions++;
                        }
                        else
                        {
                            fm = true;
                        }


                    }

                }
                if (closed)
                {
                    return print;

                }
                else
                {
                    Console.WriteLine("Code compiling error: expected \")\".");
                    return "";
                }
              
            }
            string intab(string str)
            {
                char[] split = str.ToCharArray();
                string print = "";
                bool foundchar = false;
                
                foreach (char i in split)
                {
                    if (i != Convert.ToChar("{") && foundchar && i != Convert.ToChar("}"))
                    {
                        print = print + Convert.ToString(i);
                    }
                    if (i == Convert.ToChar("}"))
                    {

                        foundchar = false;

                    }
                    if (i == Convert.ToChar("{"))
                    {


                        foundchar = true;



                    }

                }
                return print;
            }        
            Tuple<string, bool> find(string str, string sym)
            {
                char[] split = str.ToCharArray();
                string print = "";
                bool foundchar = false;
                bool re = false;
                bool closed = false;
                foreach (char i in split)
                {
                    if (i != Convert.ToChar(sym) && foundchar)
                    {
                        print = print + Convert.ToString(i);
                    }
                    if (i == Convert.ToChar(sym))
                    {
                        re = true;
                        if (foundchar)
                        {
                            foundchar = false;
                            closed = true;
                        }
                        else if (closed == false)
                        {
                            foundchar = true;
                        }
                    }

                }
                if (closed)
                {
                    return Tuple.Create(print, re && foundchar == false);
                }
                else
                {
                    Console.WriteLine($"Code compiling error: expected \"{sym}\".");
                      return Tuple.Create("", re && foundchar == false); 
                }
                
            }
            string[] tostringarray1(Numval[] array)
            {
                string[] newarr = new string[array.Length];
                int on = 0;
                foreach (Numval i in array)
                {
                    newarr[on] = i.value.ToString();
                    on++;
                }
                return newarr;
            }
            string[] tostringarray2(Stringval[] array)
            {
                string[] newarr = new string[array.Length];
                int on = 0;
                foreach (Stringval i in array)
                {
                    newarr[on] = i.value;
                    on++;
                }
                return newarr;
            }
            Object[] getitems(string str)
            {
                if (inpar(str).Contains("{"))
                {
                    string par = inpar(str);
                    string tab = intab(par);
                    string[] spl = tab.Split(Convert.ToChar(","));
                    int on = 0;
                    foreach (string i in spl)
                    {
                        spl[on] = find(i, "\"").Item1;
                        on++;
                    }
                    return spl;
                }
                else
                {
                    string inpa = inpar(str);
                    
                    if (inpa.Contains("NS:help"))
                    {
                        string[] help = {
                            "NS is a low-level programming language intended to be different from normal languages such as LUA.",
                            "NS is based off LUA with some C# and python in the mix.",
                            "NS is probably the most flexible programming language, being able to run incomplete code or malformed code such as: \"print(\"aaa\"",
                            "To create a string variable, use: new string(\"[insert value here]\")",
                            "To create an int variable, use: new int([insert value here])",
                            "To see all variables of a certain type, use: print([variable type]:all)",
                            
                            "In the compiler's files is also a list of all code features",
                            "Methods/functions can be created using: new function([function name])",
                            "Functions/Methods can then be called using: [name of function]([argument])",
                            "--NS is NOT an \"official\" language.--"
                        };
                        return help;
                    }
                    if (inpa.Contains("int:all"))
                    {
                        string[] newarr = new string[intvals.Length];
                        int on = 0;
                        foreach (Numval i in intvals)
                        {
                            newarr[on] = i.value.ToString();
                            on++;
                        }
                        return newarr;
                    }
                    else if (inpa.Contains("string:all"))
                    {

                        return stringvals;

                    }
                    else
                    {

                        string stri = find(inpa, "\"").Item1;
                        string[] newst = { stri };
                        return newst;
                    }


                }
              
            }
            object getval(string text)
            {
                if (text.Contains("\""))
                {
                    return find(text, "\"");
                }
                else
                {
                    if (text.Contains("[") && text.Contains("]"))
                    {
                        return text.Replace("[", "").Replace("]","").Split(Convert.ToChar(","));
                    }
                    else
                    {
                        if (text.Contains("."))
                        {
                            return Convert.ToDouble(text);
                        }
                        else
                        {
                            return Convert.ToDouble(Convert.ToInt32(text));
                        }
                        
                    }
                }
            }
            string getcode(string name)
            {
                string code = "nilcode";
               foreach (Function f in Functions)
                {
                    if (f.name == name)
                    {
                        code = f.code;
                    }
                }
                return code;
            }
            void writedebuginfo(string[] debuginfo)
            {
                Console.WriteLine("debug info {");
                foreach (string i in debuginfo)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("}");
            }
            string getstrbynname(string name)
            {
                string res = "nil";
                foreach (Stringval s in stringvals)
                {
                    if (name.Replace("\\","") == s.name)
                    {
                        res = s.value;
                    }
                }
                if (res == "nil")
                {
                    Console.WriteLine($"Value \"{name.Replace("\\","")}\" does not exist.");
                }
                return res;
            }

            // MAIN COMPILER

            void compile(string msg)
            {
                bool normalcompile = true;
                string[] codesmsg = { };

                //handle variables
               
                msg = msg.Replace("/currentpath", currentpath);
                foreach (Stringval s in stringvals)
                {
                    msg = msg.Replace("/" + s.name, "\"" + s.value + "\"");
                }
                foreach (Numval n in intvals)
                {
                    msg = msg.Replace("/" + n.name, n.value.ToString());
                }

                string temprem = "";

                if (!msg.StartsWith("##")) // comment implementation
                {
                    try
                    {
                        if (!msg.StartsWith("\\"))
                        {
                            temprem = withoutpar(msg);
                        }
                        
                    }
                    catch
                    {

                    }
                }
               
              
                
                
                if (!msg.StartsWith("##")) // comment implementation
                {
                    // For calling functions
                   // Console.WriteLine(withoutpar(msg));
                    if (!msg.StartsWith("\\"))
                    {

                        //Console.WriteLine("function_call");
                        Console.WriteLine(">wpmsg");
                        if (getcode(withoutpar(msg)) != "nilcode" && msg.Contains("("))
                        {
                           // Console.WriteLine("found_code");
                            argument = inpar(msg); // Set argument variable for use

                            msg = getcode(withoutpar(msg.Replace(":", "")));
                            codesmsg = msg.Split(Convert.ToChar(";"));

                        }

                    }
                   
                        if (infunc && msg == "end")
                        {
                            infunc = false;
                            Functions = insertfunc(Functions, new Function(currentname, currentcode));

                        }
                        else if (infunc)
                        {
                            if (readingfile == false)
                            {
                                Console.WriteLine("|");
                            }

                            currentcode = currentcode + msg + ";";
                        }
                        else
                        {
                            codesmsg = msg.Split(Convert.ToChar(";"));

                        }

                    

                }
               

                // MAIN COMPILING CODE

                foreach (string i2 in codesmsg) // i2 is the code being ran seperated by ";"
                {

                   

                    if (!i2.StartsWith("##")) // comment implementation, put variable handling inside if statement to save memory
                    {

                        //Handle variables

                        msg = i2;
                        msg = msg.Replace("/currentpath", currentpath);
                        msg = msg.Replace("/in", $"\"{input}\""); // for input
                        foreach (Stringval s in stringvals)
                        {
                            msg = msg.Replace("/" + s.name, s.value);
                        }
                        foreach (Numval n in intvals)
                        {
                            msg = msg.Replace("/" + n.name, n.value.ToString());
                        }
                        msg = msg.Replace("/arg", $"\"{argument}\"");
                        msg = msg.Replace("/ret", $"\"{returned}\"");
                        string wp = "";
                        if (!msg.StartsWith("\\"))
                        {
                            Console.WriteLine(">wpmsg");
                            wp = withoutpar(msg); // introduce variable to make writing else if statements easier
                        }

                        

                        if (wp == "print")
                        {


                            object spl = getval(inpar(msg));
                            Console.WriteLine(spl);

                        }
                        else if (wp == "new string")
                        {
                            string[] arg = inpar(msg).Split(Convert.ToChar(","));
                            string strname = find(arg[0], "\"").Item1;
                            if (strname == "")
                            {
                                strname = arg[0];
                            }
                            string strval = find(arg[1], "\"").Item1;
                            if (strval == "")
                            {
                                strval = arg[1];
                            }
                            stringvals = stringinsertval(stringvals, new Stringval(strname, strval));
                            Console.WriteLine("Inserted value " + strname + " into list of string variables.");
                        }
                        else if (wp == "new num")
                        {
                            string[] arg = inpar(msg).Split(Convert.ToChar(","));
                            string strname = find(arg[0], "\"").Item1;
                            if (strname == "")
                            {
                                strname = arg[0];
                            }
                            string strval = find(arg[1], "\"").Item1;
                            if (strval == "")
                            {
                                strval = arg[1];
                            }
                            intvals = intinsertval(intvals, new Numval(strname, Convert.ToInt32(strval)));


                            Console.WriteLine("Inserted value " + strname + " into list of int variables.");
                        }
                        else if (wp == "new function")
                        {
                            infunc = true;
                            currentcode = "";
                            string message = "";
                            currentname = inpar(msg);
                              Console.WriteLine("|"); // disabled for new (better i guess) prefixed writing
                          //  Console.Write(">");
                        }
                        else if (wp == "math")
                        {
                            string[] arg = inpar(msg).Split(Convert.ToChar(","));
                            double var = 0;
                            double cv = 0;

                            double f = 0;
                            string sym = "";
                            string[] arg2 = tostringarrayfromchar(arg[1].ToCharArray());
                            foreach (string i in arg2)
                            {
                                if (i != " ")
                                {
                                    if (cv == 0)
                                    {



                                        f = Convert.ToInt32(i);

                                    }
                                    if (cv == 1)
                                    {
                                        sym = i;
                                    }
                                    if (cv == 2)
                                    {
                                        double tv;


                                        tv = Convert.ToInt32(i);

                                        if (sym == "+")
                                        {
                                            var = f + tv;

                                        }
                                        else if (sym == "-")
                                        {
                                            var = f - tv;

                                        }
                                        else if (sym == "*")
                                        {
                                            var = f * tv;

                                        }
                                        else if (sym == "/")
                                        {
                                            var = f / tv;

                                        }
                                        else if (sym == "%")
                                        {
                                            var = f % tv;
                                            tv = var;
                                        }
                                        else if (sym == "%of")
                                        {
                                            var = (f * 0.01) * tv;
                                            tv = var;
                                        }
                                        else if (sym == ">>")
                                        {

                                            var = Convert.ToDouble(Convert.ToInt32(f) >> Convert.ToInt32(tv));
                                            tv = var;
                                        }
                                        else if (sym == "<<")
                                        {
                                            var = Convert.ToDouble(Convert.ToInt32(f) << Convert.ToInt32(tv));
                                            tv = var;
                                        }

                                    }
                                    cv++;
                                }

                            }
                            int n = 0;
                            int cn = 0;
                            foreach (Numval num in intvals)
                            {
                                if (num.name + "\\" == arg[0])
                                {
                                    n = cn;
                                }
                                cn++;
                            }
                            intvals[cn] = new Numval(intvals[cn].name, var);
                        } //not needed anymore
                        else if (wp == "io.compile")
                        {
                            string[] lines = System.IO.File.ReadAllLines(find(inpar(msg), "\"").Item1);
                            if (readingfile && find(inpar(msg), "\"").Item1 != currentiofile)
                            {
                                readingfile = true;
                                foreach (string l in lines)
                                {
                                    compile(l);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: file loop detected. Line not compiled.");
                            }


                        }
                        else if (wp == "io.editfile")
                        {
                            string path = find(inpar(msg), "\"").Item1;
                            try
                            {
                                if (path.EndsWith(".ns") || path.EndsWith(".nsproj") || path.EndsWith(".nspj"))
                                {
                                    string[] lines1 = System.IO.File.ReadAllLines(path);
                                }
                                else
                                {
                                    throw new Exception("NotNs");
                                }



                            }
                            catch (Exception e)
                            {
                                if (e is ArgumentException ex)
                                {
                                    Console.WriteLine("Project file does not exist! Would you like to create it? Y/N");
                                    string an = Console.ReadLine();
                                    if (an.ToLower() == "y" || an.ToLower() == "yes")
                                    {
                                        file f = new file(path, fctype.create);
                                        f.lines = new string[] { "This is your new project file!", "Start typing some code!" };
                                    }
                                    else
                                    {
                                        Console.WriteLine("You have chosen not to create a new project file.");
                                    }
                                }
                                else
                                {
                                    if (e.Message == "NotNs")
                                    {

                                    }
                                    if (displaydebug)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                            }

                            currentpath = path;
                            config(false);
                        }
                        else if (msg == "debug.enable()")
                        {
                            Console.WriteLine("Compiler debugging enabled. Use debug.Disable() to disable compiler debugging.");
                            displaydebug = true;
                        }
                        else if (msg == "debug.disable()")
                        {
                            Console.WriteLine("Compiler debugging disabled. Use debug.Enable() to enable compiler debugging.");
                            displaydebug = false;
                        }
                        else if (wp == "getin")
                        {
                            string inp = find(inpar(msg), "\"").Item1;
                            Console.Write("in>" + inp);
                            input = Console.ReadLine();
                        }
                        else if (wp == "sfunc")
                        {
                            string[] _args = inpar(msg).Split(Convert.ToChar(","));
                            string[] convars = { };

                            Console.WriteLine(">Looks like \"sfunc(var,strings)\" isn't implemented yet.");
                        } //do not code, this is not needed
                        else if (wp == "sys.bcolor")
                        {
                            object cc = typeof(ConsoleColor).GetProperty(find(inpar(msg), "\"").Item1).GetValue(typeof(ConsoleColor));
                            if (cc is ConsoleColor c)
                            {
                                Console.BackgroundColor = c;
                            }
                        }
                        else if (wp == "sys.tcolor")
                        {
                            object cc = typeof(ConsoleColor).GetProperty(find(inpar(msg), "\"").Item1).GetValue(typeof(ConsoleColor));
                            if (cc is ConsoleColor c)
                            {
                                Console.ForegroundColor = c;
                            }
                        }
                        else if (wp == "return")
                        {
                            returned = inpar(msg);
                            break;
                        }
                        else
                        {
                            bool isvarsetting = false;

                            string type = "";
                            foreach (Stringval st in stringvals)
                            {
                                if (st.name == msg.Split(Convert.ToChar("="))[0].Replace(" ", ""))
                                {
                                    type = "string";
                                    isvarsetting = true;
                                }
                            }
                            foreach (Numval nm in intvals)
                            {
                                if (nm.name == msg.Split(Convert.ToChar("="))[0].Replace(" ", ""))
                                {
                                    type = "num";
                                    isvarsetting = true;
                                }
                            }
                            if (isvarsetting)
                            {
                                string[] arg = msg.Split(Convert.ToChar("="));
                                double var = 0;
                                double cv = 0;

                                double f = 0;
                                string sym = "";
                                string[] arg2 = tostringarrayfromchar(arg[1].ToCharArray());

                                if (type == "string")
                                {
                                    bool firststr = true;
                                    sym = "+";
                                    string[] eq = arg[1].Split(Convert.ToChar("+"));
                                    string curstr = "";
                                    if (eq.Length == 1)
                                    {
                                        eq = arg[1].Split(Convert.ToChar("-"));
                                        sym = "-";
                                    }
                                    foreach (string s in eq)
                                    {
                                        if (!firststr)
                                        {
                                            if (sym == "+")
                                            {
                                                curstr = curstr + find(s, "\"").Item1;
                                            }
                                            else if (sym == "-")
                                            {
                                                curstr = curstr.Replace(find(s, "\"").Item1, "");
                                            }
                                        }
                                        else
                                        {
                                            curstr = find(s, "\"").Item1;
                                            firststr = false;
                                        }
                                    }
                                    string varname = msg.Split(Convert.ToChar("="))[0].Replace(" ", "");
                                    int index = 0;
                                    foreach (Stringval s in stringvals)
                                    {
                                        if (s.name == varname)
                                        {
                                            s.value = curstr;
                                            stringvals[index] = s;
                                        }
                                        index++;
                                    }
                                }
                                else if (type == "num")
                                {
                                    foreach (string i in arg2)
                                    {
                                        if (i != " ")
                                        {
                                            if (cv == 0)
                                            {



                                                f = Convert.ToInt32(i);

                                            }
                                            if (cv == 1)
                                            {
                                                sym = i;
                                            }
                                            if (cv == 2)
                                            {
                                                double tv;


                                                tv = Convert.ToInt32(i);
                                                if (displaydebug)
                                                {
                                                    Console.WriteLine("Found number in mathmatic equation: " + tv.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (sym == "+")
                                                {
                                                    var = f + tv;

                                                }
                                                else if (sym == "-")
                                                {
                                                    var = f - tv;

                                                }
                                                else if (sym == "*")
                                                {
                                                    var = f * tv;

                                                }
                                                else if (sym == "/")
                                                {
                                                    var = f / tv;

                                                }
                                                else if (sym == "%")
                                                {
                                                    var = f % tv;
                                                    tv = var;
                                                }
                                                else if (sym == "%of")
                                                {
                                                    var = (f * 0.01) * tv;
                                                    tv = var;
                                                }
                                                else if (sym == ">>")
                                                {

                                                    var = Convert.ToDouble(Convert.ToInt32(f) >> Convert.ToInt32(tv));
                                                    tv = var;
                                                }
                                                else if (sym == "<<")
                                                {
                                                    var = Convert.ToDouble(Convert.ToInt32(f) << Convert.ToInt32(tv));
                                                    tv = var;
                                                }

                                            }
                                            cv++;
                                        }

                                    }
                                    string[] debuglist = { getpos(arg[0], "nums").ToString() };
                                    writedebuginfo(debuglist);
                                    intvals[getpos(arg[0], "nums")].value = var;
                                }
                            }
                          


                        }
                    }
                    
                            
                }
              
            }
            void config(bool ro)
            {
                string[] lines1 = System.IO.File.ReadAllLines(currentpath);
                bool pfunc = false;
                foreach (string i in lines1)
                {
                    if (i.StartsWith("new function("))
                    {
                        pfunc = true;
                        Console.WriteLine(i);
                    }
                    else if (i == "end")
                    {
                        pfunc = false;
                        Console.WriteLine(i);
                    } else if (pfunc)
                    {
                        Console.WriteLine(i);
                    }
                    else
                    {
                        Console.WriteLine(i);
                    }
                    
                }
                while (true)
                {
                    string read = Console.ReadLine();
                    if (read == "compile()")
                    {
                        Console.Clear();
                        Console.WriteLine(">>COMPILE BEGIN<<");
                        string[] lines = System.IO.File.ReadAllLines(currentpath);
                        string blank = "";
                        foreach (string i in lines)
                        {
                            blank = blank + "; " + i;
                        }
                        compile(blank);
                        Console.WriteLine(">>COMPILING COMPLETE, PRESS ENTER<<");
                        Console.ReadLine();
                        Console.Clear();
                        lines1 = System.IO.File.ReadAllLines(currentpath);
                        foreach (string i in lines1)
                        {
                            Console.WriteLine(i);
                        }
                    }
                    else
                    {
                        if (ro == false)
                        {
                            string[] lines = System.IO.File.ReadAllLines(currentpath);
                            lines = inserttoarray(lines, read);

                            System.IO.File.WriteAllLines(currentpath, lines);
                        }
                        else
                        {
                            Console.WriteLine("File readonly! You can not modify this file!");
                        }
                      
                    }
                }
            }
            void compileloop()
            {
                string[] lines = System.IO.File.ReadAllLines(args[0]);
                string blank = "";
                foreach (string i in lines)
                {
                    blank = blank + "; " + i;
                }
                compile(blank);
                Console.WriteLine("Press enter to close compiler, or input \"continuecompile\" to compile other code.");
                string t = Console.ReadLine();
                if (t == "")
                {

                }
                else
                {
                    Console.Title = "NS compiler V0.3 (incomplete build)";
                    while (true)
                    {
                        string message = Console.ReadLine();
                        compile(message);


                    }
                }
            }
            if (args.Length > 0)
            {
                currentpath = args[0];
                Console.Title = "NS compiler V0.3 -- File: " + args[0];
                Console.WriteLine("Configure file or compile file? (Type \"config\" or \"compile\".) You may compile the current file using compile() while editing.");
                string ty = Console.ReadLine();
                if (ty == "config")
                {
                    config(false);
                } else if (ty == "compile")
                {
                    compileloop();
                } else
                {
                    Console.WriteLine("OpenType missing or invalid. Compiler will now shut down.");
                    System.Threading.Thread.Sleep(6000);
                }
                
            }
            else
            {
                Console.Title = "NS compiler V0.3 (incomplete build)";
                while (true)
                {
                    Console.Write("");
                    string message = Console.ReadLine();
                    compile(message);
                }
            }
          
        }
    }
}
