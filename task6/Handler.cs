using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Handler
    {
        protected readonly CultureInfo culture = new CultureInfo("en-US");

        public string ShowCommands()
        {
            var result = "";
            foreach (var command in Enum.GetNames(typeof(Commands)))
            {
                result += command;
                result += " ";
            }

            return result;
        }

        protected bool TryConvertToInt(string value, ref int result)
        {
            try
            {
                result = Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected bool TryConvertToFloat(string value, ref float result)
        {
            try
            {
                result = Convert.ToSingle(value, culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
