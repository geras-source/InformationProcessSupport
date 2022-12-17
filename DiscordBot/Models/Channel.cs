using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    internal class Channel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string CategoryType { get; set; }
    }
}