using libsqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace wcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class wcfService : IwcfService
    {
        SQLite baza = new SQLite();

        public int addNewText(string user)
        {
            return baza.AddNewText(user);
        }

        public bool addNewUser(string user)
        {
            return baza.AddNewUser(user);
        }

        public bool DBis()
        {
            return baza.DBis();
        }

        public string firstText(string user)
        {
            return baza.FirstText(user);
        }

        public int firstTextID(string user)
        {
            return baza.FirstTextID(user);
        }

        public Dictionary<int, string> getTexts(string user)
        {
            return baza.GetTexts(user);
        }

        public int getUserId(string user)
        {
            return baza.GetUserId(user);
        }

        public bool updateText(int textID, string text)
        {
            return baza.UpdateText(textID, text);
        }

        public bool userExist(string user)
        {
            return baza.UserExist(user);
        }
    }
}
