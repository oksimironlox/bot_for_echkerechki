using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot
{
    class WritingFiles
    {
        async public static void Write(Message message, ITelegramBotClient bot)
        {
            if (message.Photo != null)
            {
                DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\received\Photos");
                int countFiles = directory.GetFiles().Length;
                string photoId = message.Photo.Last().FileId;
                string destinationFilePath = $@"..\..\..\..\received\Photos\{message.Chat.FirstName}{countFiles}.jpg";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                var file = await bot.GetInfoAndDownloadFileAsync(photoId, fileStream);
            } else if(message.Text != null)
            {
                string path = $@"..\..\..\..\received\Text\{message.Chat.FirstName}.txt";

                System.IO.File.AppendAllText(path, $"{message.Text}\n");
            } else if(message.Video != null)
            {
                DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\received\Videos");
                int countFiles = directory.GetFiles().Length;
                string videoId = message.Video.FileId;
                string destinationFilePath = $@"..\..\..\..\received\Videos\{message.Chat.FirstName}{countFiles}.mp4";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                var file = await bot.GetInfoAndDownloadFileAsync(videoId, fileStream);
            } else if(message.Voice != null)
            {
                DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\received\Voices");
                int countFiles = directory.GetFiles().Length;
                string voiceId = message.Voice.FileId;
                string destinationFilePath = $@"..\..\..\..\received\Voices\{message.Chat.FirstName}{countFiles}.ogg";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                var file = await bot.GetInfoAndDownloadFileAsync(voiceId, fileStream);
            } else if(message.Audio != null)
            {
                DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\received\Audios");
                int countFiles = directory.GetFiles().Length;
                string audioId = message.Audio.FileId;
                string destinationFilePath = $@"..\..\..\..\received\Audios\{message.Chat.FirstName}{countFiles}.mp3";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                var file = await bot.GetInfoAndDownloadFileAsync(audioId, fileStream);
            } else if(message.VideoNote != null)
            {
                DirectoryInfo directory = new DirectoryInfo(@"..\..\..\..\received\VideoNotes");
                int countFiles = directory.GetFiles().Length;
                string videoNoteId = message.VideoNote.FileId;
                string destinationFilePath = $@"..\..\..\..\received\VideoNotes\{message.Chat.FirstName}{countFiles}.mp4";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                var file = await bot.GetInfoAndDownloadFileAsync(videoNoteId, fileStream);
            }
        }
        public static void WriteSubscribersJSON(List<DataSubscribers> subscribers, string path)
        {
            string json = JsonConvert.SerializeObject(subscribers, Formatting.Indented);
            System.IO.File.WriteAllText(path, json);
        }

    }
}
