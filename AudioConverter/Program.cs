using NAudio;
using NAudio.Wave;
using System.Net;
using WMPLib;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace AudioConverter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Baixar música");
                Console.WriteLine("2 - Tocar música");
                int downloadOrPlay = int.Parse(Console.ReadLine());

                if (downloadOrPlay == 1)
                {
                    Console.Clear();

                    //Baixar música
                    Console.WriteLine("--- Baixar a música pelo Youtube ---");
                    Console.WriteLine("Insira o link da música:");
                    var videoUrl = Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine("Insira o nome da música:");
                    string musicName = Console.ReadLine();
                    Console.Clear();

                    var musicNamePath = @"C:\Users\rafar\OneDrive\Área de Trabalho\vscode projects\C#\mp3-player-downloader\AudioConverter\musics\" + musicName + ".mp3";

                    await Music.DownloadMusicAsync(videoUrl, musicNamePath);
                 
                }
                else if (downloadOrPlay == 2)
                {
                    Console.Clear();
                    var musicsFolderPath = @"C:\Users\rafar\OneDrive\Área de Trabalho\vscode projects\C#\mp3-player-downloader\AudioConverter\musics\";

                    //Listar músicas 
                    Console.Clear();
                    Console.WriteLine("--- Tocar a música ---");
                    Console.WriteLine("Lista de músicas disponíveis:");

                    Music.ShowMusics(musicsFolderPath);

                    //Tocar música
                    Console.WriteLine("\nEscolha uma das músicas disponíveis para tocar: ");
                    var musicToPlay = musicsFolderPath + Console.ReadLine() + ".mp3";

                    Music.PlayMusic(musicToPlay);



                    Console.Clear();
                }
            }
            
        }
    }
}