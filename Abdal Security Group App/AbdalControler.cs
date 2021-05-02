using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abdal_Web_Traffic_Generator
{
    class AbdalControler
    {
        private static bool v_unauthorized_process = false;
        
        public static bool unauthorized_process
        {
            get { return v_unauthorized_process; }
            set { v_unauthorized_process = value; }
        }
    }
}
