
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace Bot
{
    class ReadingFile
    {
        public static void ReadAllFiles()
        {
            ReadHelloAnswer();
            ReadByeAnswer();
            ReadPhoto();
            ReadTikTok();
            ReadVoice();
            //ReadSubscribersJSON();
        }

        private static void ReadHelloAnswer()
        {
            string pathHelloAnswer = @"..\..\..\..\hello.txt";
            string[] helloAnswer = File.ReadAllLines(pathHelloAnswer);
            foreach (string hi in helloAnswer)
            {
                Program.HelloAnswer.Add(hi);
            }
        }

        private static void ReadByeAnswer()
        {
            string pathHelloAnswer = @"..\..\..\..\bye.txt";
            string[] byeAnswer = File.ReadAllLines(pathHelloAnswer);
            foreach (string bye in byeAnswer)
            {
                Program.ByeAnswer.Add(bye);
            }
        }

        private static void ReadPhoto()
        {
            DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\photo");
            FileInfo[] photos = directory.GetFiles();
            foreach (FileInfo photo in photos)
            {
                Program.Photos.Add(photo);
            }
        }

        private static void ReadTikTok()
        {
            DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\tiktok");
            FileInfo[] tiktokes = directory.GetFiles();
            foreach (FileInfo tiktok in tiktokes)
            {
                Program.TikTok.Add(tiktok);
            }
        }

        private static void ReadVoice()
        {
            DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\voice");
            FileInfo[] voices = directory.GetFiles();
            foreach (FileInfo voice in voices)
            {
                Program.Voices.Add(voice);
            }
        }
        static void ReadSubscribersJSON()
        {
            string path = @"..\..\..\..\subscribers.json";
            var jsonPersonString = File.ReadAllText(path);
            Program.Subscribes = JsonConvert.DeserializeObject<List<DataSubscribers>>(jsonPersonString.Replace(';', ' '));
        }
    }
}
