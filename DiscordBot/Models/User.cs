using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    internal class User
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string? Roles { get; set; }
        public ulong ChannelId { get; set; }
    }
}
