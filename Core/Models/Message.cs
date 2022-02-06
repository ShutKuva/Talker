using Core.DbCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    [DbAutoCreator.AutoDB]
    public class Message : BaseEntity
    {
        public Message()
        {

        }

        public Message(string text, DateTime writtenAt, int chatId)
        {
            Text = text;
            WrittenAt = writtenAt;
            ChatId = chatId;
        }

        [Required]
        [MaxLength(4096)]
        public string Text { get; set; }

        public DateTime WrittenAt { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }
    }
}
