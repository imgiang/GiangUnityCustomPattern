using UnityEngine;

namespace GiangCustom.Runtime.Internet
{
    public class CheckInternetConnection
    {
        public static bool CheckConnection()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }
}
