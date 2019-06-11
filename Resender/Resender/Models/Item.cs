using System;
using Resender.Services;

namespace Resender.Models
{
    public class Item : IDataBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Text { get; set; }
    }
}