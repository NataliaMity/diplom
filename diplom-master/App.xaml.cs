using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MityaginaNP
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly UX.Entity.BDEntities DataBase = new UX.Entity.BDEntities();
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMzMjkxMEAzMjMwMmUzNDJlMzBkYTJzNlhQZEtGVjJ4S3UrVmZRTVNLUzBUZWY2cUljckN5TnhNTVRsZ1pzPQ==;Mgo DSMBaFt/QHRqVVhkVFpHaV1AQmFJfFBmQ2lcd1R1cUU3HVdTRHRcQl5iTH5Vc01mXHhcc30=;Mgo DSMBMAY9C3t2VVhkQlFacldJXnxLfEx0RWFab1h6cV1MZVlBNQtUQF1hSn5Qd0BjX3pWdXFSQmJV;Mgo DSMBPh8sVXJ0S0J XE9AflRBQmFLYVF2R2BJeVR1fF9DZUwgOX1dQl9gSX1Sc0VlXHdbdXNTQmk=;MTMzMjkxNEAzMjMwMmUzNDJlMzBsWFc2K2djWHlDQXM1anRDR0UxVXI3SHIyZTJNMW9FL201WnJudWJMcWR3PQ==;NRAiBiAaIQQuGjN/V0Z WE9EaFtKVmBWfFFpR2NbfE52flBOallUVAciSV9jS31TdURmWXlbeHFQTmFUWQ==;ORg4AjUWIQA/Gnt2VVhkQlFacldJXnxLfEx0RWFab1h6cV1MZVlBNQtUQF1hSn5Qd0BjX3pWdXFdRWdU;MTMzMjkxN0AzMjMwMmUzNDJlMzBIelBQbHdJZnJrblJJT1RjUW5ET1luZmlKYmFubzRFeDBOWnRTZG1EMVhrPQ==;MTMzMjkxOEAzMjMwMmUzNDJlMzBnUnZEd01oU1JVTms0V0RnQkVNdWxJVndKV2JRRzFnd2JLMFR6dk41aGRrPQ==;MTMzMjkxOUAzMjMwMmUzNDJlMzBpT2Vvc29RNk1ZQUNCN1pJQ2RTM1dBVW1Bc1pydytFWHp6Uy9TVVpQRmVzPQ==;MTMzMjkyMEAzMjMwMmUzNDJlMzBtY2pDS25wKzVTcE84ZC8xNC9iaEVBV3NpSUF6TTBFN0RRSnArOXNrcnJjPQ==;MTMzMjkyMUAzMjMwMmUzNDJlMzBkYTJzNlhQZEtGVjJ4S3UrVmZRTVNLUzBUZWY2cUljckN5TnhNTVRsZ1pzPQ==");
        }

        private void PART_ContentHost_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
