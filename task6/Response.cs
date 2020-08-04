using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Response : Handler
    {
        private readonly int _command;
        private readonly string[] _data;

        public Response(int command, string[] data)
        {
            _command = command;
            _data = data;
        }

        public int GetCommand()
        {
            return _command;
        }

        public int GetDataSize()
        {
            return _data.Length;
        }

        public bool TryStringFromData(int position, ref string result)
        {
            if (position >= _data.Length)
                return false;
            result = _data[position];
            return true;
        }

        public bool TryFloatFromData(int position, ref float result)
        {
            if (position >= _data.Length)
                return false;
            return TryConvertToFloat(_data[position], ref result);
        }

        public bool TryIntFromData(int position, ref int result)
        {
            if (position >= _data.Length)
                return false;
            return TryConvertToInt(_data[position], ref result);
        }
    }
}
