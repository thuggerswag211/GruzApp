using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.AppData
{
    internal class Connect
    {
        private static TestEntities c;

        public static TestEntities context
        {
            get
            {
                if (c == null)
                {

                    c = new TestEntities();
                }
                return c;
            }
        }
    }
}
