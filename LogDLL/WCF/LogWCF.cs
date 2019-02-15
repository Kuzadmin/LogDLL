using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using System.Xml.Linq;

namespace LogDLL.WCF
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    class LogWCF : ILogService
    {
        private ConfigClass conf;

        public LogWCF(ConfigClass _conf)
        {
            conf = _conf;
        }
        

        private bool RequestDbPrivate(string tableName, TypeOperation typeOperation, string UniqueID, string Request)
        {
            MessageList mes_list = new MessageList();
            MessageLog logs = new MessageLog(MessageLog.INFO, "RequestDB", "Создание потока");
            mes_list.Add_Message(logs);

            string sql = Convertable.Convert(Request, UniqueID, typeOperation);
            if (string.IsNullOrEmpty(sql) == true)
                return false;

            string ttt = conf.GetValue("DB_CONN");

            SQLiteConnection connection = new SQLiteConnection(conf.GetValue("DB_CONN"));
            connection.Open();

            switch (typeOperation)
            {
                case TypeOperation.Insert:
                    sql = "INSERT INTO " + tableName + " " + sql;
                    break;
                case TypeOperation.Update:
                    sql = "update " + tableName + " " + sql;
                    break;
                case TypeOperation.Remove:
                    sql = "delete from " + tableName + " " + sql;
                    break;
            }
            logs = new MessageLog(MessageLog.INFO, "RequestDB", sql);
            mes_list.Add_Message(logs);
            bool rets = false;
            try
            {
                int ret = new SQLiteCommand(sql, connection).ExecuteNonQuery();
                connection.Close();
                rets = true;
            }
            catch (Exception e)
            {
                rets = false;
                logs = new MessageLog(MessageLog.ERROR, "RequestDB", e.Message);
                mes_list.Add_Message(logs);
                logs = new MessageLog(MessageLog.ERROR, "RequestDB", e.StackTrace);
                mes_list.Add_Message(logs);
            }
            logs = new MessageLog(MessageLog.INFO, "RequestDB", "Запрос сформирован и отправлен"); 
            mes_list.Add_Message(logs);

            Logger.getInstance().Write(mes_list);

            return rets;
        }

        public bool RequestDB(string TableName, TypeOperation OperationType, string UniqueID, string Request)
        //public bool RequestDB(SQL sql)
        {

            return this.RequestDbPrivate(TableName, OperationType, UniqueID, Request);
        }

        public bool RequestDBSimple(string TableName, string OperationType, string UniqueID, string Request)
        //public bool RequestDBSimple(SQLSimple sql )
        {
            string typeOper = OperationType.ToUpper().Trim();
            TypeOperation _typeOperation = TypeOperation.Insert;
            if (typeOper.Equals("INSERT"))
                _typeOperation = TypeOperation.Insert;
            else if (typeOper.Equals("UPDATE"))
                _typeOperation = TypeOperation.Update;
            else if (typeOper.Equals("REMOVE"))
                _typeOperation = TypeOperation.Remove;
            return this.RequestDbPrivate(TableName, _typeOperation, UniqueID, Request);
        }
    }
}
