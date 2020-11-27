﻿using System;
using System.Collections.Generic;

namespace MyFirstGame
{
    // Some base class that contains everything in game.
    public class Game
    {
        public void Start()
        {
            const string FileName = "D:/IT/VS Project/MyFirstGame/FileMenager/Levels/MapLevel1.txt";
            SceneLevel curentLevel = new SceneLevel(FileName);

        }
    }

    class SceneLevel
    {
        public SceneLevel(string fileName)
        {
            FileReader fileReader = new FileReader();
            GameObject[,] tileMap = MapFiller(fileReader.LoadFromFile(fileName)); 


        }

        private GameObject[,] MapFiller(List<string> inputFile)
        {
            GameObject[,] tileMap = new GameObject[1, 1];

            for (int i = 0; i < inputFile.Count; i++)
            {
                ResizeArray(ref tileMap, i, inputFile[i].Length);
                FillArrayLine(ref tileMap, inputFile[i], i);

            }
            

            return tileMap;
        }
        private void FillArrayLine(ref GameObject[,] inputArray, string inputString, int lineNumber)
        {
            for (int i = 0; i < inputString.Length; i++)
            {
                inputArray[lineNumber, i] = SymbolDecoder(inputString[i], lineNumber, i);
            }
        }

        private void ResizeArray(ref GameObject[,] inputArray, int newM, int newN)
        {
            GameObject[,] newArray = new GameObject[newM, newN];
            for (int m = 0; m < newArray.GetLength(0); m++)
                for (int n = 0; n < newArray.GetLength(1); n++)
                    newArray[m, n] = newArray[m, n];
            inputArray = newArray;
        }

        private GameObject SymbolDecoder(char symbol, int x, int y)
        {
            Position position = new Position(x, y);

            Dictionary<char, Texture.Name> SymbolDictionary = new Dictionary<char, Texture.Name>()
            {
                {' ', Texture.Name.nothing},
                {'X', Texture.Name.wall},
                {'_', Texture.Name.space}
            };
            Texture texture = new Texture(SymbolDictionary[symbol]);

            List<Texture.Name> passeblTextureList = new List<Texture.Name>()
            {
                Texture.Name.wall,
            };

            return new GameObject(position, texture, IsPasseble());

            bool IsPasseble()
            {
                if (!passeblTextureList.Contains(SymbolDictionary[symbol])) return true;
                else return false;
            }
        }
    }
}