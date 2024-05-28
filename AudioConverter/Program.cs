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
            
            //Download archive path
            //Downloaded archive path
            var outfile = @"C:\Users\rafar\OneDrive\Área de Trabalho\vscode projects\C#\AudioConverter\AudioConverter\musics\teste.mp3";
            var outfileE = @"C:\Users\rafar\OneDrive\Área de Trabalho\vscode projects\C#\AudioConverter\AudioConverter\musics\";

            Console.WriteLine("--- Baixar a música pelo Youtube ---");
            Console.WriteLine("Insira o link da música:");
            var videoUrl = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Insira o nome da música:");
            string musicName = Console.ReadLine();
            Console.Clear();
            var path = @"C:\Users\rafar\OneDrive\Área de Trabalho\vscode projects\C#\AudioConverter\AudioConverter\musics\" + musicName + ".mp3";

            //Baixar video pelo Youtube
            YoutubeClient youtubeClient = new YoutubeClient();
            var video = await youtubeClient.Videos.GetAsync(videoUrl);
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            if (streamInfo != null)
            {
                Console.WriteLine("Video Encontrado:\n");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Título: " + video.Title);
                Console.WriteLine("Autor: " + video.Author);
                Console.WriteLine("--------------------------------");

                await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, path);
                Console.WriteLine("\nÁudio baixado com sucesso");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nenhum video encontrado");
            }

            //Lista de músicas na pasta musics
            Console.Clear();
            Console.WriteLine("--- Tocar a música ---");
            Console.WriteLine("Lista de músicas disponíveis:");

            string[] archives = Directory.GetFiles(outfileE, "*.*", SearchOption.AllDirectories);
            foreach (string a in archives)
            {
                int lastBar = a.LastIndexOf(@"\");
                string correctName = a.Substring(lastBar + 1);

                Console.WriteLine(correctName);
            }

            //Tocar de música
            Console.WriteLine("\nEscolha uma das músicas disponíveis para tocar: ");
            var musicToPlay = outfileE + Console.ReadLine() + ".mp3";

            Console.Clear();
            using (var audioOutput = new WaveOutEvent())
            {
                using (var audioFile = new AudioFileReader(musicToPlay))
                {
                    audioOutput.Init(audioFile);
                    audioOutput.Play();

                    Console.WriteLine("Musica Tocando...");
                    while (audioOutput.PlaybackState == PlaybackState.Playing)
                    {
                            Console.WriteLine(".");
                            Thread.Sleep(1000);
                    }
                }
            }

        }
    }
}