using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainUI.Procedure.Test
{
    public class PeformanceTest : BaseTest
    {
        public bool Execute()
        {
            try
            {

                Thread thread = new Thread(new ThreadStart(CollectData));
                thread.Start();
                Delay(10, "1");
                bool isOK = true;
                return isOK;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void CollectData()
        {
            //试验进程

        }
    }
}
