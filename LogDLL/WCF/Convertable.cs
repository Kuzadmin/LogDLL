using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDLL.WCF
{
    static class Convertable
    {
        public static string Convert(string request, string uniqleId, TypeOperation operation)
        {
            switch (operation)
            {
                case TypeOperation.Insert:
                    {
                        if (String.IsNullOrEmpty(request) == false)
                            return Convertable.Insert(request.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                        else
                            return Convertable.Insert(uniqleId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    
                case TypeOperation.Update:
                    {
                        string[] mas = request.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] tab = uniqleId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        return Convertable.Update(mas, tab);
                    }
                    
                case TypeOperation.Remove:
                    {
                        if (String.IsNullOrEmpty(request) == false)
                            return Convertable.Remove(request.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                        else
                            return Convertable.Remove(uniqleId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                default:
                    return "";
            }
        }

        static private string Insert(string[] mass)
        {
            StringBuilder columnName = new StringBuilder();
            StringBuilder value = new StringBuilder();
            string temp = "";
            foreach (string str in mass)
            {
                int pos = str.IndexOf("=");
                temp = str.Substring(0, pos);
                columnName.Append(temp + ",");
                temp = str.Substring(pos + 1);
                value.Append("'"+ temp + "',");
            }
            //columnName.Remove(columnName.Length - 1, 1);

            
            columnName.Append("dt_insert,dt_update");
            string format_date = "yyyy-MM-dd HH:mm:ss.fff";
            temp = DateTime.Now.ToString(format_date);
            value.Append("'" + temp + "','" + temp + "'");
            //value.Remove(value.Length - 1, 1);
            return "(" + columnName + ") VALUES(" + value + ")";
        }

        static private string Update(string[] mass, string[] tab)
        {
            StringBuilder columnName = new StringBuilder();
            StringBuilder value = new StringBuilder();
            string temp = "";
            foreach (string str in mass)
            {
                int pos = str.IndexOf("=");
                string local = str.Substring(0, pos);
                string local1 = str.Substring(pos + 1);
                columnName.Append( local + "='" + local1 + "',");
            }

            foreach (string str in tab)
            {
                int pos = str.IndexOf("=");
                string local = str.Substring(0, pos);
                string local1 = str.Substring(pos + 1);

                value.Append(local + "='" + local1 + "' and ");
            }

            //columnName.Remove(columnName.Length - 1, 1);
            //
           
            string format_date = "yyyy-MM-dd HH:mm:ss.fff";
            temp = DateTime.Now.ToString(format_date);
            columnName.Append("dt_update='" + temp + "'");
            value.Remove(value.Length - 5, 5);
            return "set " + columnName + " Where " + value;
        }

        static private string Remove(string[] tab)
        {
            StringBuilder value = new StringBuilder();
            foreach (string str in tab)
            {
                int pos = str.IndexOf("=");
                string local = str.Substring(0, pos);
                string local1 = str.Substring(pos + 1);

                value.Append(local + "='" + local1 + "' and ");
            }
            value.Remove(value.Length - 5, 5);
            return "Where " + value;
        }
    }
}
