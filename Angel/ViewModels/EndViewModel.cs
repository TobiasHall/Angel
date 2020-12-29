using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using static Angel.DataSaverExtension;

namespace Angel
{
    public class EndViewModel : BaseViewModel
    {        
        public int NrOfDace { get; set; }
        public int NrOfPike { get; set; }
        public int NrOfPerc { get; set; }
        public int NrOfChar { get; set; }
        public int NrOfTrout { get; set; }
        public int TotalFishes { get; set; }
        public decimal TotalWeight { get; set; } = 0;
        public int TotalScore { get; set; }

        private Player player;
        private int gameTimer;

        //test av spara och öppna
        private Player returnPlayer;
        private List<Player> players = new List<Player>();
        private string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\Data\highscore.txt");


        //string pdfFolder;        

        //#if DEBUG
        //    DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName);
        //    pdfFolder = directoryInfo.FullName + @"\PdfFile\Auto Refill Reminder List.pdf";
        //    System.Diagnostics.Process.Start(pdfFolder);
        //#endif

        //#if (!DEBUG)
        //    pdfFolder = Environment.CurrentDirectory + @"\PdfFile\Auto Refill Reminder List.pdf";
        //    Process.Start(pdfFolder);
        //#endif




        public EndViewModel(Player player, int gameTimer)
        {
            NewGameCommand = new RelayCommand(GetGameView, CanExecute);
            HighscoreViewCommand = new RelayCommand(GetHighscoreView, CanExecute);
            MainMenuViewCommand = new RelayCommand(GetMainMenuView, CanExecute);
            ExitGameCommand = new RelayCommand(CloseApplication, CanExecute);

            this.player = player;
            this.gameTimer = gameTimer;
            SetLabels();

            SavePlayerToFile();
            GetListOfPlayers();
        }

        private void SavePlayerToFile()
        {
            try
             {
                // Write the contents of the variable someClass to a file.
                WriteToBinaryFile<Player>("test.bin", player);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetListOfPlayers()
        {
            try
            {
                //Read the file contents back into a variable.
                //Player object1 = ReadFromBinaryFile<Player>(path);
                //players.Add(object1);

                returnPlayer = (Player)ReadFromBinaryFile("test.bin");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetGameView(object parameter)
        {
            Main.Content = new GameView(player, gameTimer);
        }

        private void SetLabels()
        {
            foreach (Fish fish in player.FishBucket)
            {
                switch (fish.FishId)
                {
                    case (int)FishEnum.Dace:
                        NrOfDace++;
                        break;
                    case (int)FishEnum.Pike:
                        NrOfPike++;
                        break;
                    case (int)FishEnum.Perch:
                        NrOfPerc++;
                        break;
                    case (int)FishEnum.Char:
                        NrOfChar++;
                        break;
                    case (int)FishEnum.Trout:
                        NrOfTrout++;
                        break;
                    default:
                        break;
                }
                TotalFishes++;
                TotalWeight += fish.Weight;
            }
        }
    }
}
