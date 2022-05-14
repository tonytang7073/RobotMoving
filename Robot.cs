using RobotMoving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotMoving
{
    public class Robot
    {
        public const int BOARDMAX = 5;
        public const string NORTH = "NORTH";
        public const string SOUTH = "SOUTH";
        public const string WEST = "WEST";
        public const string EAST = "EAST";
        public int? PositionX { get; private set; }
        public int? PositionY { get; private set; }

        public string Face { get; private set; }

        public Robot()
        {
            this.PositionX = null;
            this.PositionY = null;

        }


        private void Place(int x, int y, string face)
        {
            if (!IsOnTableTop() && IsValidFace(face)) { Face = face; } //only set Face for the first Place.
            Place(x, y);
        }

        private void Place(int x, int y)
        {
            if (IsInsideRange(x)) { PositionX = x; }
            if (IsInsideRange(y)) { PositionY = y; }

        }

        private void Move()
        {
            if (!IsOnTableTop()) { return; }

            int oldX = (int)PositionX;
            int oldY = (int)PositionY;

            switch (Face)
            {
                case NORTH: oldY++; break;
                case SOUTH: oldY--; break;
                case EAST: oldX++; break;
                case WEST: oldX--; break;

            }

            if (IsInsideRange(oldY)) { PositionY = oldY; }
            if (IsInsideRange(oldX)) { PositionX = oldX; }



        }

        private void Left()
        {
            if (!IsOnTableTop()) { return; }
            switch (Face)
            {
                case NORTH: Face = WEST; break;
                case SOUTH: Face = EAST; break;
                case EAST: Face = NORTH; break;
                case WEST: Face = SOUTH; break;
            }
        }

        private void Right()
        {
            if (!IsOnTableTop()) { return; }
            switch (Face)
            {
                case NORTH: Face = EAST; break;
                case SOUTH: Face = WEST; break;
                case EAST: Face = SOUTH; break;
                case WEST: Face = NORTH; break;
            }
        }


        private bool IsValidFace(string dirface)
        {

            List<string> validFaces = new List<string>()
            {
                NORTH,SOUTH,EAST,WEST
            };

            

            return validFaces.Contains(dirface.Trim().ToUpper());

        }

        private bool IsOnTableTop()
        {

            return PositionX != null && PositionY != null;

        }

        private bool IsInsideRange(int position)
        {
            return position <= BOARDMAX && position >= 0;

        }

        public string Report()
        {
            if (!IsOnTableTop()) { return null; }
            return String.Format("{0},{1},{2}", PositionX, PositionY, Face);
        }

        public void CommandExecute(RobotCommand cmd, string cmdArgs)
        {
            switch (cmd)
            {
                case RobotCommand.INVALID:
                    Console.WriteLine("Oops this is not a valid place command.!"); break;
                case RobotCommand.LEFT:
                    Left(); break;
                case RobotCommand.MOVE:
                    Move(); break;
                case RobotCommand.PLACE:
                    PlaceCommand(cmdArgs); break;
                case RobotCommand.REPORT:
                    ReportCommand(); break;
                case RobotCommand.RIGHT:
                    Right(); break;
            }
        }

        public void PlaceCommand(string arg)
        {

            string argtrim = arg.Trim();
            var argParts = argtrim.Split(',').ToList();
            int x, y;
            
            if (IsOnTableTop()) 
            { 
                if (!int.TryParse(argParts[0], out x) || !int.TryParse(argParts[1], out y))
                {
                    Console.WriteLine("Oops this is not a valid place command. It should be like: Place 1,2");
                    return;
                }
                Place(x, y); 
            }
            else
            {
                if (argParts.Count < 3 || !int.TryParse(argParts[0], out x) || !int.TryParse(argParts[1], out y) || !IsValidFace(argParts[2]))
                {
                    Console.WriteLine("Oops this is not a valid place command. It should be like: Place 1,2,North");
                    return;
                }
                Place(x, y, argParts[2].Trim().ToUpper());
            }


        }

        public void ReportCommand()
        {
            if (Report() != null)
            {
                Console.WriteLine("Output:" + Report());
            }
        }



    }
}
