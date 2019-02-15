using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LogDLL.WCF
{
    [ServiceContract]
    interface ILogService
    {
        [OperationContract]  //(Name ="GetReport")
        bool RequestDB(string TableName, TypeOperation OperationType, string UniqueID, string Request);
        //bool RequestDB(SQL sql);

        [OperationContract]  //(Name ="GetReport")
        bool RequestDBSimple(string TableName, string OperationType, string UniqueID, string Request);
        //bool RequestDBSimple(SQLSimple sql);
    }
}
