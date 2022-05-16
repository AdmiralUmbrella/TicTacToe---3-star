// ABDIEL WONG AVILA

//(***) Modify the code so that a player can play against a simple AI that just chooses random moves. The game should provide theoption of choosing "Single Player" mode or "Two Players" mode at thebeginning of the game. Should the player select the "Single Player" mode, the game should ask the player select who is going to makethe first move, the player or the  AI, at the beginning of each match.

//(***) Modify the code so that a player can save the game on a file, and, at a later time, the game can be loaded and resumed.

//(***) Modify the code so that players input is in the form of navigation commands such as "up", "down", "left", "right", "mark". The cursor should start at the center of the grid.


using System;
using System.IO;
public class TicTacToe
{
    public const string X = "X";
    public const string O = "O";
    public string[] board;
    public bool isPlayerOneTurn;
    public bool activeAI;
    public bool loadedGame;
    public int cursorPosition = 4;


    public static void Main(string[] args)
    {
        TicTacToe ttt = new TicTacToe();
        ttt.Start();
    }
    public TicTacToe()
    {
    }

    public void Start()
    {
        string input;
        Init(); // 1. Initialize Variables and Environment
        ShowGameStartScreen(); // 2. Show Game Start Screen
        do
        {
            ShowBoard(); // 3. Show Board
            do
            {
                ShowInputOptions(); // 4. Show Input Options
                input = GetInput(); // 5. Get Input
            }
            while (!IsValidInput(input)); // 6. Validate Input
            ProcessInput(input); // 7. Process Input
            UpdateGameState(); // 8. Update Game State
        }
        while (!IsGameOver()); // 9. Check Termination Condition
        ShowGameOverScreen(); // 10. Show Game Over Screen
    }

    public void Init()
    {
        board = new string[9];
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = i.ToString();
        }
        isPlayerOneTurn = true;
        loadedGame = false;
    }

    public async void ShowGameStartScreen()
    {
        bool validGamemode = true;
        bool validWhoStarts = true;
        bool validNewOrLoad = true;
        Console.WriteLine("Welcome to Tic-Tac-Toe!");
        Console.WriteLine("\nInput \"new game\" to start a new game.\n\nInput \"load game\" to load a game.");
        while (validNewOrLoad)
        {
            string newOrLoad = Console.ReadLine().Trim().ToLower();
            if (newOrLoad == "load game")
            {
                loadedGame = true;
                Console.Clear();
                Console.WriteLine("Loading your game...\n\nPress any key to continue");
                Console.ReadKey();
                validNewOrLoad = false;
            }
            else if (newOrLoad == "new game")
            {
                Console.Clear();
                Console.WriteLine("Starting new game...\n\nPress any key to continue");
                Console.ReadKey();
                validNewOrLoad = false;
            }
            else
            {
                Console.WriteLine("Please, input \"new game\" to start a new game.\n\nInput \"load game\" to load a game.");
            }
        }

        if (!loadedGame)
        {
            Console.Clear();
            Console.Write("\nSingleplayer\n\nMultiplayer\n\nInput a gamemode: ");
            while (validGamemode)
            {
                string gameMode = Console.ReadLine().Trim().ToLower();
                if (gameMode == "singleplayer")
                {
                    activeAI = true;
                    Console.WriteLine($"You choose {gameMode}\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    validGamemode = false;
                }
                else if (gameMode == "multiplayer")
                {
                    activeAI = false;
                    Console.WriteLine($"You choose {gameMode}\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    validGamemode = false;
                }
                else
                {
                    Console.Write("Please, input a gamemode:");
                }
            }

            if (activeAI == true)
            {
                Console.WriteLine("Input who's gonna start firts:\n\nPlayer 1\n\nIA");
                Console.SetCursorPosition(31, 0);
                while (validWhoStarts)
                {
                    string whoStarts = Console.ReadLine().Trim().ToLower();
                    if (whoStarts == "player 1" || whoStarts == "ia")
                    {
                        Console.Clear();
                        Console.WriteLine($"{whoStarts} will be doing the firts move.\nPress any key to continue");
                        Console.ReadKey();
                        if (whoStarts == "player 1")
                        {
                            isPlayerOneTurn = true;
                        }
                        else if (whoStarts == "ia")
                        {
                            isPlayerOneTurn = false;
                        }
                        validWhoStarts = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("Please, input who's gonna start firts:\n\nPlayer 1\n\nIA");
                        Console.SetCursorPosition(39, 0);
                    }
                }
            }
        }
        else
        {
            LoadGame();
        }
    }

    public void ShowBoard()
    {
        string b = board[0] + "|" + board[1] + "|" + board[2] + "\n" +
        "-----\n" +
        board[3] + "|" + board[4] + "|" + board[5] + "\n" +
        "-----\n" +
        board[6] + "|" + board[7] + "|" + board[8] + "\n";
        Console.WriteLine("\n" + b);
    }

    public void ShowInputOptions()
    {
        if (activeAI == false)
        {
            if (isPlayerOneTurn)
            {
                Console.WriteLine("Is player 1 turn!");
            }
            else if (!isPlayerOneTurn)
            {
                Console.WriteLine("Is player 2 turn!");
            }

            Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else one direction: up, down, right, left.");
        }
        else
        {
            if (isPlayerOneTurn)
            {
                Console.WriteLine("Is player 1 turn!");
                Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else one direction: up, down, right, left.");
            }
            else if (!isPlayerOneTurn)
            {
                Console.WriteLine("It's IA turn!");
            }
        }
    }

    public string GetInput()
    {
        if (activeAI == false)
        {
            try
            {
                string inputToString = Console.ReadLine().Trim().ToLower();

                return inputToString;
            }
            catch (Exception e)
            {
                return "An error occurred.";
            }
        }
        else
        {
            if (isPlayerOneTurn)
            {
                try
                {
                    string inputToString = Console.ReadLine().Trim().ToLower();

                    return inputToString;
                }
                catch (Exception e)
                {
                    return "An error occurred.";
                }
            }
            else
            {
                return "Easter Egg = found";
            }
        }
    }

    public bool IsValidInput(string input)
    {
        if (activeAI == false)
        {
            if (input != "up" && input != "down" && input != "left" && input != "right" && input != "mark" && input != "save")
            {
                Console.Write("Please input one direction (up, down, right, left)\nInput save to save your game: ");
                return false;
            }
            else if (!IsEmpty(cursorPosition) && input == "mark")
            {
                Console.WriteLine($"Position {cursorPosition} has already been played.\n");
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (isPlayerOneTurn)
            {
                if (input != "up" && input != "down" && input != "left" && input != "right" && input != "mark" && input != "save")
                {
                    Console.Write("Please input one direction (up, down, right, left)\nInput save to save your game: ");
                    return false;
                }
                else if (!IsEmpty(cursorPosition) && input == "mark")
                {
                    Console.WriteLine($"Position {cursorPosition} has already been played.\n");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
    public bool IsEmpty(int a)
    {
        return (board[a] != X && board[a] != O);
    }

    public void ProcessInput(string f)
    {
        if (activeAI == false)
        {
            if (isPlayerOneTurn == true)
            {
                do
                {
                    if (f == "up")
                    {
                        if (f == "up" && cursorPosition == 0 || f == "up" && cursorPosition == 1 || f == "up" && cursorPosition == 2)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition + 3;
                        }
                        cursorPosition = cursorPosition - 3;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "down")
                    {
                        if (f == "down" && cursorPosition == 6 || f == "down" && cursorPosition == 7 || f == "down" && cursorPosition == 8)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition - 3;
                        }
                        cursorPosition = cursorPosition + 3;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "right")
                    {
                        if (f == "right" && cursorPosition == 8)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition - 1;
                        }
                        cursorPosition = cursorPosition + 1;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "left")
                    {
                        if (f == "left" && cursorPosition == 0)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition + 1;
                        }
                        cursorPosition = cursorPosition - 1;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "mark")
                    {
                        board[cursorPosition] = X;
                        isPlayerOneTurn = false;
                    }

                    else if (f == "save")
                    {
                        SaveGame();
                        Console.WriteLine("Press ENTER to continue or any key to quit the game");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            ShowBoard();
                            Console.WriteLine("Your game have been succesfully saved...");
                            ShowInputOptions();
                            f = Console.ReadLine().Trim().ToLower();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }

                    else
                    {
                        f = Console.ReadLine().Trim().ToLower();
                    }
                } while (isPlayerOneTurn == true);
            }

            else if (isPlayerOneTurn == false)
            {
                do
                {
                    if (f == "up")
                    {
                        if (f == "up" && cursorPosition == 0 || f == "up" && cursorPosition == 1 || f == "up" && cursorPosition == 2)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition + 3;
                        }
                        cursorPosition = cursorPosition - 3;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "down")
                    {
                        if (f == "down" && cursorPosition == 6 || f == "down" && cursorPosition == 7 || f == "down" && cursorPosition == 8)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition - 3;
                        }
                        cursorPosition = cursorPosition + 3;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "right")
                    {
                        if (f == "right" && cursorPosition == 8)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition - 1;
                        }
                        cursorPosition = cursorPosition + 1;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "left")
                    {
                        if (f == "left" && cursorPosition == 0)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition + 1;
                        }
                        cursorPosition = cursorPosition - 1;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "mark")
                    {
                        board[cursorPosition] = O;
                        isPlayerOneTurn = true;
                    }

                    else if (f == "save")
                    {
                        SaveGame();
                        Console.WriteLine("Press ENTER to continue or any key to quit the game");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            ShowBoard();
                            Console.WriteLine("Your game have been succesfully saved...");
                            ShowInputOptions();
                            f = Console.ReadLine().Trim().ToLower();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }

                    else
                    {
                        f = Console.ReadLine().Trim().ToLower();
                    }

                } while (isPlayerOneTurn == false);
            }
        }
        else
        {
            if (isPlayerOneTurn == true)
            {
                do
                {
                    if (f == "up")
                    {
                        if (f == "up" && cursorPosition == 0 || f == "up" && cursorPosition == 1 || f == "up" && cursorPosition == 2)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition + 3;
                        }
                        cursorPosition = cursorPosition - 3;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "down")
                    {
                        if (f == "down" && cursorPosition == 6 || f == "down" && cursorPosition == 7 || f == "down" && cursorPosition == 8)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition - 3;
                        }
                        cursorPosition = cursorPosition + 3;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "right")
                    {
                        if (f == "right" && cursorPosition == 8)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition - 1;
                        }
                        cursorPosition = cursorPosition + 1;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "left")
                    {
                        if (f == "left" && cursorPosition == 0)
                        {
                            Console.WriteLine("You've reached the limit of the board");
                            cursorPosition = cursorPosition + 1;
                        }
                        cursorPosition = cursorPosition - 1;
                        Console.WriteLine($"Your current position is {cursorPosition}\nInput \"mark\" to mark, else input other direction\nInput save to save your game: ");
                        f = Console.ReadLine().Trim().ToLower();
                    }

                    else if (f == "mark")
                    {
                        board[cursorPosition] = X;
                        isPlayerOneTurn = false;
                    }

                    else if (f == "save")
                    {
                        SaveGame();
                        Console.WriteLine("Press ENTER to continue or any key to quit the game");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            ShowBoard();
                            Console.WriteLine("Your game have been succesfully saved...");
                            ShowInputOptions();
                            f = Console.ReadLine().Trim().ToLower();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }

                    else
                    {
                        f = Console.ReadLine().Trim().ToLower();
                    }
                } while (isPlayerOneTurn == true);
            }
            else
            {
                Random rng = new Random();
                int i = rng.Next(board.Length);
                while (!isPlayerOneTurn)
                {
                    if (IsEmpty(i))
                    {
                        board[i] = O;
                        isPlayerOneTurn = true;
                    }
                    else
                    {
                        i = rng.Next(board.Length);
                    }
                }
            }
        }
    }

    public void UpdateGameState()
    {
        cursorPosition = 4;
    }

    public void SaveGame()
    {
        File.WriteAllLinesAsync("boardstate.txt", board);

        using (FileStream fs = new FileStream("gamestate.txt", FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                ShowBoard();
                bool gameState = isPlayerOneTurn;
                bool singlePlayer = activeAI;

                bw.Write(gameState);
                bw.Write(singlePlayer);
            }
        }
    }

    public void LoadGame()
    {
        string[] boardState = File.ReadAllLines("boardstate.txt");
        board = boardState;

        using (FileStream fs = new FileStream("gamestate.txt", FileMode.Open))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                bool gameState = br.ReadBoolean();
                bool singlePlayer = br.ReadBoolean();

                isPlayerOneTurn = gameState;
                activeAI = singlePlayer;
            }
        }
    }

    public bool IsGameOver()
    {
        return CheckWin(X) || CheckWin(O) || CheckDraw();
    }
    public bool CheckWin(string mark)
    {
        return CheckLine(mark, 0, 1, 2) || CheckLine(mark, 3, 4, 5) ||
        CheckLine(mark, 6, 7, 8) || CheckLine(mark, 0, 3, 6) ||
        CheckLine(mark, 1, 4, 7) || CheckLine(mark, 2, 5, 8) ||
        CheckLine(mark, 0, 4, 8) || CheckLine(mark, 2, 4, 6);
    }
    public bool CheckLine(string mark, int a, int b, int c)
    {
        return (board[a] == mark && board[b] == mark && board[c] == mark);
    }
    public bool CheckDraw()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (IsEmpty(i)) { return false; }
        }
        return true;
    }

    public void ShowGameOverScreen()
    {
        ShowBoard();
        if (CheckWin(X))
        {
            Console.WriteLine("Player 1 won!\n");
        }
        else if (CheckWin(O))
        {
            if (activeAI == false)
            {
                Console.WriteLine("Player 2 won!\n");
            }
            else
            {
                Console.WriteLine("IA won!");
            }
        }
        else
        {
            Console.WriteLine("Draw!\n");
        }
    }
}
// <-- NOT HERE.