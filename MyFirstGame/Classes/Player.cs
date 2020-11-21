﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyFirstGame
{
    class Player : Enemy
    {
        // Action, Func

        public static event Func<Position, bool> Moved;

        public Player(Position position, Texture texture)
            : base(position, texture) { this.Print(); }

        public void WaitMoving()
        {
            while (true)
            {
                ConsoleKey consoleKey = Console.ReadKey(true).Key;
                this.Print();
                switch (consoleKey)
                {
                    case ConsoleKey.UpArrow:
                        if (Map.TextureByPosition(position.Up()).IsPassable())
                            this.Move("Up");                            
                        break;
                    case ConsoleKey.DownArrow: if (Map.TextureByPosition(position.Down()).IsPassable())
                            this.Move("Down");
                        break;
                    case ConsoleKey.LeftArrow: if (Map.TextureByPosition(position.Left()).IsPassable())
                            this.Move("Left");
                        break;
                    case ConsoleKey.RightArrow: if (Map.TextureByPosition(position.Right()).IsPassable())
                            this.Move("Right");
                        break;
                    case ConsoleKey.Escape: goto exit;
                    default:
                        this.Print();
                        Message message = new Message(3, 12);
                        message.Exeption("Incorrect Key. To Exit press ESC");
                        break;
                }


                this.Print();
            }
        exit:;
        }

        public void Hide()
        {
            //Texture.BackgroundTexture.Print(position);
        }

        public void Print()
        {
            texture.Print(position);
        }

        public void Move(string direction)
        {
            Hide();
            position.Step(direction);
            Moved(position);
            Print();
        }

        private void CanMove(Position position)
        {
            if (Map.TextureByPosition(position).IsPassable()) this.position = position;
        }
    }
}
