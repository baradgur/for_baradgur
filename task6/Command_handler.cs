using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6
{
    class Command_handler : Handler
    {
        public Response Request(string value)
        {
            string[] words = value.Split(' ');
            int command;

            if (CheckValidCommand(words[0]))
            {
                command = ProcessCommand(words[0]);
            }
            else
            {
                command = (int)EXCEPTION.WRONG_COMMAND;
            }

            var data = new string[words.Length - 1];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = words[i + 1];
            }
            return new Response(command, data);
        }

        private int ProcessCommand(string command)
        {
            foreach (var enum_command in Enum.GetValues(typeof(Commands)))
            {
                if (Enum.GetName(typeof(Commands), enum_command) == command)
                    return (int)enum_command;
            }
            throw new ArgumentException("command not found");
        }

        private bool CheckValidCommand(string command)
        {
            return Enum.GetNames(typeof(Commands)).Contains(command);
        }
    }
}
