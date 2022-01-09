using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }

        public string ParticipantsNumber { get; set; }

        public bool Locked { get; set; }

        public string InvitationLink { get; set; }
    }
}
