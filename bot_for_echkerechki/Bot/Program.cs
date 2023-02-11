using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    class Program
    {
        private static string _token { get; set; } = "5645576345:AAEBxavYZTciXMlzr-z9bLAn82hST3YVDGo";
        private static TelegramBotClient _client;
        private static string _myText = "че надо?";
        private static Stream _stream;
        private static long _chatID;
        private static string _name;
        public static List<DataSubscribers> Subscribes = new List<DataSubscribers>();
        public static List<string> HelloAnswer = new List<string>();
        public static List<string> ByeAnswer = new List<string>();
        public static List<FileInfo> Photos = new List<FileInfo>();
        public static List<FileInfo> TikTok = new List<FileInfo>();
        public static List<FileInfo> Voices = new List<FileInfo>();


        static void Main(string[] args)
        {
            ReadingFile.ReadAllFiles();
            _client = new TelegramBotClient(_token);
            _client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async private static Task Update(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            Message message = update.Message;
            _chatID = message.Chat.Id;
            _name = message.Chat.FirstName;

            Random random = new Random(); int index;

            //WritingFiles.Write(message, bot);

            NewSubscribe();

            DataSubscribers subscriber = null;
            foreach (DataSubscribers sub in Subscribes)
            {
                if (sub.Id == _chatID)
                {
                    subscriber = sub;
                    break;
                }
            }

            Rewrite(subscriber);

            ReplyKeyboardMarkup keyboardActions = new(new[]
            {
                            new KeyboardButton[] { "иди нахуй", "что делать?", "подписаться"},
                            new KeyboardButton[] { "привет", "пока", "тик ток"},
                            new KeyboardButton[] {"катя иди нахуй"},
            })
            {
                ResizeKeyboard = true
            };

            if (message.Text != null)
            {
                Console.WriteLine($"{_name} | {message.Text} ");
                switch (message.Text.ToLower())
                {
                    case "иди нахуй":
                    case "пошел нахуй":
                        _myText = "сам иди нахуй";
                        await bot.SendTextMessageAsync(_chatID, _myText, null, null, null, null, null, null, null, keyboardActions);
                        return;
                    case "привет":
                        int ran = random.Next(1, 4);
                        if (ran == 1)
                        {
                            index = random.Next(Voices.Count);
                            string pathToHiVoices = $@"..\..\..\..\voice\{Voices[index].Name}";
                            Message hiVoice;
                            using (var stream = System.IO.File.OpenRead(pathToHiVoices))
                            {
                                hiVoice = await bot.SendVoiceAsync(_chatID, stream, null, null, null, 1);
                            }
                        }
                        else if (ran == 2)
                        {
                            index = random.Next(HelloAnswer.Count);
                            _myText = HelloAnswer[index];
                            await bot.SendTextMessageAsync(_chatID, _myText, null, null, null, null, null, null, null, keyboardActions);
                        }
                        else
                        {
                            string pathToHiVoices = @"..\..\..\..\tiktok\privet.mp4";
                            Message hiVideo;
                            using (var stream = System.IO.File.OpenRead(pathToHiVoices))
                            {
                                hiVideo = await bot.SendVideoAsync(_chatID, stream);
                            }
                        }
                    return;
                    case "что делать?":
                        _myText = "отправь гс, фотку, музыку или напиши че нибудь";
                        await bot.SendTextMessageAsync(_chatID, _myText, null, null, null, null, null, null, null, keyboardActions);
                        return;
                    case "пока":
                        index = random.Next(ByeAnswer.Count);
                        _myText = ByeAnswer[index];
                        await bot.SendTextMessageAsync(_chatID, _myText, null, null, null, null, null, null, null, keyboardActions);
                        return;
                    case "катя иди нахуй":
                        string pathToVoice = @"..\..\..\..\voice\ilya.ogg";
                        Message voice;
                        using (var stream = System.IO.File.OpenRead(pathToVoice))
                        {
                            voice = await bot.SendVoiceAsync(_chatID, stream, null, null, null, 1);
                        }
                        return;
                    case "подписаться":
                        string pathToPhoto = @"..\..\..\..\echkerechki.jpg";
                        _stream = System.IO.File.OpenRead(pathToPhoto);
                        await bot.SendPhotoAsync(_chatID, new Telegram.Bot.Types.InputFiles.InputOnlineFile(_stream, "echkerechki.jpg"), caption: "<a href=\"https://t.me/samye_krutie_podrugi\">эщкерещки</a>", parseMode: ParseMode.Html);
                        _stream.Close();
                        return;
                    case "тик ток":
                        index = random.Next(subscriber.TikTok.Count);
                        string name = TikTok[index].Name;
                        string pathToTikTok = $@"..\..\..\..\tiktok\{name}";
                        _stream = System.IO.File.OpenRead(pathToTikTok);
                        await bot.SendVideoAsync(_chatID, new Telegram.Bot.Types.InputFiles.InputOnlineFile(_stream));
                        _stream.Close();
                        subscriber.Delete(index, "tiktok");
                        WritingFiles.WriteSubscribersJSON(Subscribes, @"..\..\..\..\subscribers.json");
                        return;
                    default:
                        _myText = "иди нахуй долбаеб";
                        await bot.SendTextMessageAsync(_chatID, _myText, null, null, null, null, null, null, null, keyboardActions);
                        return;
                }
            }

            if (message.Photo != null)
            {
                if(subscriber.Photos.Count != 0)
                {
                    index = random.Next(subscriber.Photos.Count);
                    string name = Photos[index].Name;
                    string pathToPhoto = $@"..\..\..\..\photo\{name}";
                    _stream = System.IO.File.OpenRead(pathToPhoto);
                    await bot.SendTextMessageAsync(_chatID, "неплохая фотка но наша лучше");
                    await bot.SendPhotoAsync(_chatID, new Telegram.Bot.Types.InputFiles.InputOnlineFile(_stream));
                    _stream.Close();
                    subscriber.Delete(index, "photo");
                    WritingFiles.WriteSubscribersJSON(Subscribes, @"..\..\..\..\subscribers.json");
                }

                return;
            }

            if (message.Voice != null)
            {
                await bot.SendTextMessageAsync(_chatID, "я ебал твою телку");
                return;
            }

            if (message.Audio != null)
            {
                string pathToVoice = @"..\..\..\..\voice\voice.ogg";
                Message voice;
                using (var stream = System.IO.File.OpenRead(pathToVoice))
                {
                    voice = await bot.SendVoiceAsync(_chatID, stream, null, null, null, 36);
                }

                await bot.SendTextMessageAsync(message.Chat.Id, "патимейкер лучше");
                return;
            }

            
        }

        private static void NewSubscribe()
        {
            if (Subscribes.Count != 0)
            {
                bool flag = true;
                foreach (DataSubscribers subscriber in Subscribes)
                {
                    if (subscriber.Id == _chatID)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Subscribes.Add(new DataSubscribers(_chatID, _name, Photos, TikTok));
                }
            }
            else
            {
                Subscribes.Add(new DataSubscribers(_chatID, _name, Photos, TikTok));
            }
        }

        private static void Rewrite(DataSubscribers subscriber)
        {
            if(subscriber.Photos.Count == 0)
            {
                subscriber.Photos = Photos;
            }
            if(subscriber.TikTok.Count == 0)
            {
                subscriber.TikTok = TikTok;
            }
            WritingFiles.WriteSubscribersJSON(Subscribes, @"..\..\..\..\subscribers.json");
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
