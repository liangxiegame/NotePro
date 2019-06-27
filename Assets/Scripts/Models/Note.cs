using System;

namespace NotePro
{
    public class Note
    {
        public string Id = Guid.NewGuid().ToString();

        public string Title;

        public string Description;

        public int Priority;

        public int ColorIndex;

        public string Date;
    }
}