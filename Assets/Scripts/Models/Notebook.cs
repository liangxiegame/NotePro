using System;

namespace NotePro
{
    public class Notebook
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name = string.Empty;
    }
}