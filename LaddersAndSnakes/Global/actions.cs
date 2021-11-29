using LaddersAndSnakes.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace LaddersAndSnakes.Global
{
    public class Actions
    {
        List<clsSquare> gameBoard = new List<clsSquare>();//Defining a clsSquare list - Defining the game board

        Dictionary<int, int> laddersDic = new Dictionary<int, int>();//Setting a log for the scales when each scale has a key and value which is basically the beginning of a scale and the end of a scale so there will be no confusion between the scales
        Dictionary<int, int> snakesDic = new Dictionary<int, int>();//Set a log for the snakes when each snake has a key and value which is basically the beginning of a snake and the end of a snake so there will be no confusion between the snakes
        List<int> goldList = new List<int>();//Define a list for the gold squares

        int playerA = 0, playerB = 0;//Boot both players

        //function about init the game - the game board is refresh.
        public void InitGame()
        {
            string laddersSum = null;
            string snakesSum = null;
            while (string.IsNullOrEmpty(laddersSum)) // chack if the client insert number.
            {
                Console.WriteLine("Amount of ladders:");//The user must enter the number of scales
                laddersSum = Console.ReadLine();
                Console.WriteLine("Amount of snakes:");//The user must enter the number of snakes
                snakesSum = Console.ReadLine();
            }
            int ladderNum= System.Convert.ToInt16(laddersSum);//When receiving in readline you get a string so conversion to int
            int snakeNum = System.Convert.ToInt16(snakesSum);//When receiving in readline you get a string so conversion to int

            const int goldSum = 2;//Fixed amount of gold squares 
            for (int i = 0; i < 100; i++)//Go over the game board and add the clsSquare object in each slot, and number the array by Number
            {
                gameBoard.Add(new clsSquare()
                {
                    Number = i + 1
                });



            }
            for (int i = 0; i < ladderNum; i++)//The array array is the number of the parameter entered by the user for the number of scales and in each run the call function calls two numbers
            {
                Random2Numbers(SqaurType.ladder);
            }
            for (int i = 0; i < snakeNum; i++)//The array is set as the number of the parameter entered by the user for the number of snakes and in each run calls the guerrilla function two numbers
            {
                Random2Numbers(SqaurType.snake);
            }
            for (int i = 0; i < goldSum; i++)//The array runs twice as the goldSum number and in each run the calling function calls two numbers
            {
                Random2Numbers(SqaurType.gold);
            }

        }
        //function about random of 2 numbers and put the number to the true places
       
        public void Random2Numbers(SqaurType typeS)
        {
            Random ran = new Random();
            bool isDiffrentLines = false;//A variable whose job is to constantly check that the two lottery numbers are not equal


            int getRanNum1 = ran.Next(1,100) -1;//random number from 1 to 100
            int getRanNum2 = ran.Next(1,100) -1;//random number from 1 to 100

            while (!isDiffrentLines)//As long as false
            {
                while (gameBoard[getRanNum1].SpecialSqaur())//As long as the square is occupied you will grill the number
                    getRanNum1 = ran.Next(1, 100);
                while (gameBoard[getRanNum2].SpecialSqaur()) //As long as the square is occupied you will grill the number
                    getRanNum2 = ran.Next(1, 100);
                if (typeS == SqaurType.ladder || typeS == SqaurType.snake)//If your type is a ladder or a snake
                {
                    isDiffrentLines = (getRanNum2 - 1) / 10 != (getRanNum1 - 1) / 10;//Check if the numbers are not in the same row because the ladder must go up in the ranks and the snake must go down the ranks
                }
                else
                    isDiffrentLines = true;//If you are gold

                if (!isDiffrentLines)
                   getRanNum2 = ran.Next(1, 100);
            }

            if (getRanNum1>getRanNum2)//Check if the first number is larger than the second number
            {
                int tmp = getRanNum1;//Auxiliary variable to switch between the two numbers
                getRanNum1 = getRanNum2;
                getRanNum2 = tmp;
            }

            switch (typeS)//A statement that the slot is occupied by true so there will be no reuse
            {
                case Models.SqaurType.ladder:
                    gameBoard[getRanNum1].StartLadder = true;
                    gameBoard[getRanNum2].EndLadder = true;
                    laddersDic.Add(getRanNum1, getRanNum2);//Insert into the Dictionary Beginning and End
                    break;
                case Models.SqaurType.snake:
                    gameBoard[getRanNum2].StartSnake = true;
                    gameBoard[getRanNum1].EndSnake = true;
                    snakesDic.Add(getRanNum2, getRanNum1);//Insert into the Dictionary Beginning and End
                    break;
                case Models.SqaurType.gold:
                    gameBoard[getRanNum1].Gold = true;
                    gameBoard[getRanNum2].Gold = true;
                    goldList.Add(getRanNum1);//Add to list of gold
                    goldList.Add(getRanNum2);//Add to list of gold
                    break;
                default:
                    break;
            }

           
        }

        //function about start the game.
        public void StartGame()
        {

            Players player = Players.PlayerA;
            int index = playerA;//The game begins with player a

            while (playerA < 100 || playerB < 100)//As long as none of the players reached 100
            {
                Console.WriteLine("Drop the dice by pressing enter");//Throwing dice
                Console.ReadLine();
                int steps = ThrowingDice();//The variable steps gets the sum of the dice rolls from the function
                Console.WriteLine("The sum of the cubes is{0}",steps );//Input the user the amount of dice that came out of the dice roll
                Console.ReadLine();
                index += steps;//the place + what get from the dices
                if (index>=100)//if the result is bigger of the game board 100+... so go out and winner
                {
                    break;
                }
                if (gameBoard[index].StartLadder) // if he get start ladders he go to the end ladders
                    index = laddersDic[index];

                if (gameBoard[index].StartSnake)// if he get start snake he go down to the tail

                    index = snakesDic[index];

                if (gameBoard[index].Gold) // if he get gold, if its good for him to change, he change the place with the another player
                    if (index < playerB)
                    {
                        int helper = index;
                        index = playerB;
                        playerB = helper;
                    }

                if (player == Players.PlayerA)// if the player A its the time to change
                {
                    playerA = index;

                    player = Players.PlayerB;
                    index = playerB;
                }
                else // if its B change too.
                {
                    playerB = index;

                    Console.WriteLine("Player A in {0}, Player B in {1}", playerA, playerB);
                    Console.ReadLine();
                    player = Players.PlayerA;
                    index = playerA;
                }
            }

            // the winner
            Console.WriteLine("Player {0} is WINNER!!!!", player == Players.PlayerA ? "A" : "B");


        }
        //function about throw the docr and return the result 
        public int ThrowingDice()
        {
            Random ran = new Random();
            int dice1 = ran.Next(1, 6);
            int dice2 = ran.Next(1, 6);
            return dice1+dice2;

        }

    }
}
