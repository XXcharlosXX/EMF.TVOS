using EMFTV.Models;
using UIKit;

namespace EMFTV
{
    public class Application
    {
        public static CloudData CData;
        public static ApplicationConfig Config;
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
