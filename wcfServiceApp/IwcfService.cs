using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace wcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IwcfService
    {

        [OperationContract]
        bool DBis();

        [OperationContract]
        bool addNewUser(string user);

        [OperationContract]
        int getUserId(string user);

        [OperationContract]
        int addNewText(string user);

        [OperationContract]
        Dictionary<int, string> getTexts(string user);

        [OperationContract]
        bool updateText(int textID, string text);

        [OperationContract]
        bool userExist(string user);

        [OperationContract]
        string firstText(string user);

        [OperationContract]
        int firstTextID(string user);

        // TODO: Add your service operations here
    }


    
}
