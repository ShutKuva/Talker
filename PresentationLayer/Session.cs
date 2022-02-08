using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public enum Location
    {
        Unlogged,
        Main,
        InRoom,
        InChat,
    }

    public class Session
    {
        public Location MyLocation { get; set; }

        public User LoggedUser { get; set; }

        public int RoomId { get; set; }

        public int ChatId { get; set; }

        public Session()
        {
            MyLocation = Location.Unlogged;
            LoggedUser = null;
        }

        public void PushBack()
        {
            if ((int) MyLocation >= 1)
            {
                MyLocation--;
            }
        }
    }
}
