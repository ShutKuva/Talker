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
    }

    public class Session
    {
        public Location MyLocation { get; set; }

        public User LoggedUser { get; set; }

        public Session()
        {
            MyLocation = Location.Unlogged;
            LoggedUser = null;
        }
    }
}
