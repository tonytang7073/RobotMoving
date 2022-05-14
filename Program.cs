using System;
using System.Linq;

namespace RobotMoving
{
    class Program
    {
        static void Main(string[] args)
        {
            var exit = false;
            Console.WriteLine("Welcome to the Robot Moving App! ");
            Console.WriteLine("Type the valid Command:PLACE X,Y,NORTH /Move/Left/Right/Report ");
            Robot robotA = new Robot();
            while (exit == false)
            {
                string cmdargs;
                robotA.CommandExecute(CommParse(Console.ReadLine(), out cmdargs), cmdargs);
            }

        }


        public static RobotCommand CommParse(string commandString, out string cmdargs)
        {
            string commtrim = commandString.Trim();
            var commandParts = commtrim.Split(' ').ToList();
            var commandName = commandParts[0];
            cmdargs = commtrim.Substring(commtrim.IndexOf(' ') + 1);
            switch (commandName.Trim().ToUpper())
            {
                case "PLACE": return RobotCommand.PLACE;
                case "MOVE": return RobotCommand.MOVE;
                case "LEFT": return RobotCommand.LEFT;
                case "RIGHT": return RobotCommand.RIGHT;
                case "REPORT": return RobotCommand.REPORT;
                default: return RobotCommand.INVALID;
            }
        }
    }
}
