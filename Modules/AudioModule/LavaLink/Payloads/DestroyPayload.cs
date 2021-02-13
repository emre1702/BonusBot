using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.LavaLink.Payloads
{
    internal class DestroyPayload : LavaPayload
    {
        public DestroyPayload(ulong id) : base(id, "destroy")
        {
        }
    }
}