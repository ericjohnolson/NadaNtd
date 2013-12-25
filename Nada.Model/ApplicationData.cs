using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Nada.Model
{
    public class ApplicationData
    {
        private static ApplicationData instance;

        private ApplicationData() { }

        public MembershipUser CurrentUser { get; set; }

        public int GetUserId()
        {
            return (int)ApplicationData.Instance.CurrentUser.ProviderUserKey;
        }

        public static ApplicationData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationData();
                }
                return instance;
            }
        }
    }
}
