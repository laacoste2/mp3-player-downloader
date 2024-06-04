using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace AudioConverter
{
    internal static class Music
    {
        public static async Task DownloadMusicAsync(string videoUrl, string musicNamePath)
        {
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
                Console.WriteLine("Data de Publicação: " + video.UploadDate);
                Console.WriteLine("Duração: " + video.Duration);
                Console.WriteLine("--------------------------------");

                if(musicNamePath.Length > 0)
                {
                    await youtubeClient.Videos.Streams.DownloadAsync(streamInfo, musicNamePath);
                    Console.WriteLine("\nÁudio baixado com sucesso");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("O arquivo deve ter um nome");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Nenhum video encontrado");
            }
        }

        public static void ShowMusics(string musicsFolderPath)
        {
            string[] archives = Directory.GetFiles(musicsFolderPath, "*.*", SearchOption.AllDirectories);
            foreach (string a in archives)
            {
                int lastBar = a.LastIndexOf(@"\");
                string correctName = a.Substring(lastBar + 1);

                Console.WriteLine(correctName);
            }

        }

        public static void PlayMusic(string musicToPlay)
        {
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
                    Console.WriteLine("A música terminou");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}
