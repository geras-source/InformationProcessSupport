using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Services;
using Microsoft.Extensions.Hosting.Internal;
using System.Xml.Linq;
using System.Data;
using InformationProcessSupport.Core.StatisticsCollector;
using Irony.Parsing;

namespace DiscordBot.Modules
{
    //[Group("sample")]
    public class Configuration : ModuleBase<SocketCommandContext>
    {
        private readonly IStatisticCollectorServices _statistic;

        public Configuration(IStatisticCollectorServices statistic)
        {
            _statistic = statistic;
        }
        [Command("info")]
        public async Task Info(SocketGuildUser? socketGuildUser = null)
        {
            var parsedDate = DateTime.Parse("2023-04-19");
            var res = await _statistic.CreateReportByDate(parsedDate);
            var memoryStream = new MemoryStream();
            res.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            //var embed = new EmbedBuilder()
            //    .WithColor(Color.DarkPurple)
            //    .WithTitle("Information")
            //    .WithImageUrl(Context.Guild?.GetUser(266642180720820224).GetAvatarUrl())
            //    .AddField("Комманды", "!test 'Channel name'\n !mute \n", false)
            //    .AddField("Created by:", Context.Guild?.GetUser(266642180720820224), true)
            //    .AddField("Powered by:", ".NET Framework", true);

            await Context.Channel.SendFileAsync(memoryStream, "test");
        }
        //[Command("test"/*, RunMode = RunMode.Async*/)]  
        //private async Task CommandsHandler(string ch)
        //{

            //foreach (var channels in CommandHandler.userId)
            //{
            //    int i = 0;
            //    if (ch == channels.ChannelName)
            //    {
            //        var channel = Context.Guild/*.VoiceChannels.FirstOrDefault(x => x.Name.Equals(channels.ChannelName))*/; //надо потестить
            //        foreach (var id in channels.userId)
            //        {
            //            var users = channel?.GetUser(id);
                        
            //            var nickName = users?.Nickname;
            //            if (nickName != null && ch == channels.ChannelName)
            //            {
            //                i++;
            //                userNickname.Add(nickName);
            //                userId.Add(i);
            //            }
            //        }

            //        var idQueue = string.Join("\n", userId);
            //        var allUsersNickname = string.Join("\n", userNickname);
            //        var voiceChannel = Context.Guild.VoiceChannels.FirstOrDefault(x => x.Name.Equals(channels.ChannelName));
            //        if (userId.Count != 0)
            //        {
            //            var embed = new EmbedBuilder()
            //                .WithColor(Color.DarkMagenta)
            //                .WithTitle(voiceChannel?.ToString())
            //                //.WithImageUrl(Context.User.GetAvatarUrl())
            //                .AddField("Место в очереди:", idQueue, true)
            //                .AddField("Имя пользователя:", allUsersNickname, true);

            //            await Context.Channel.SendMessageAsync(embed: embed.Build());
            //        }

            //        userNickname.Clear();
            //        userId.Clear();
            //        break;
            //    }
        //    }
        //}
        [Command("set")]
        private async Task Echo()
        {
            if (Context.Message.Author.Username == "Ярость Бури")
            {
                await Context.Channel.SendMessageAsync("");
            }

        }
    }
}