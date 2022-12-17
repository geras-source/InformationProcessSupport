using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Channels
    {
        private int i = 0;
        public List<ulong> userId = new List<ulong>();
        public string ?ChannelName { get; set; }

        public ulong SetUsers
        {
            get { return userId[i++]; }
            set { userId.Add(value); }
        }

        public void RemoveUser(ulong value)
        {
            userId.Remove(value);
        }
    }
}
