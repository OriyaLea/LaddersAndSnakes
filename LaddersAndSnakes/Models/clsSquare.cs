using System;
using System.Collections.Generic;
using System.Text;

namespace LaddersAndSnakes.Models
{
    public class clsSquare //Object with property - detail of the box
    {
        public int Number { get; set; }
        public Boolean StartLadder { get; set; } //Beginning of a ladder
        public Boolean EndLadder { get; set; }//End of ladder
        public Boolean StartSnake { get; set; }//The beginning of a snake
        public Boolean EndSnake { get; set; }//Snake end
        public Boolean Gold { get; set; }//gold


        public bool SpecialSqaur()//Check whether the box is occupied - whether there is an existing event
        {
            return Gold == true || StartLadder == true || EndLadder == true || StartSnake == true || EndSnake == true;
        }

    }
    public enum SqaurType//Parameter type
    {
        ladder,
        snake,
        gold
    }

    public enum Players//Parameter type
    {
        PlayerA,
        PlayerB
    }
}
