using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abdal_Security_Group_App
{
    class GlobalUnlockChilkat
    {
        public void unlock()
        {

            // Start Global Chilkat Unlock
            Chilkat.Global glob = new Chilkat.Global();

            bool success = glob.UnlockBundle("nahaan.CBX1125_FUiiNrsmOG2G");
            if (success != true)
            {
                //encoder.listBox1.Items.Add(glob.LastErrorText);
                MessageBox.Show(glob.LastErrorText);

            }

            int status = glob.UnlockStatus;
            if (status == 2)
            {

                //listBox1.Items.Add("Unlocked using purchased unlock code.");
                //MessageBox.Show("Unlocked using purchased unlock code.");
            }
            else
            {
                //listBox1.Items.Add("Unlocked in trial mode.");
                MessageBox.Show("Unlocked in trial mode.");
            }

            // End Global Chilkat Unlock
        }
    }
}
