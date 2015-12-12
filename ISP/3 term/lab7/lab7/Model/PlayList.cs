using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class PlayList
    {
        public List<Composition> compositions { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public TimeSpan TotalLength { get; set; }
        public int Rating { get; set; }
        
        public PlayList()
        {
            compositions = new List<Composition>();
            compositions.Add(new Composition(1, 5, "My name is..", "John Cena",
                new TimeSpan(0, 0, 5)));
            compositions.Add(new Composition(2, 10, "8-800-555-35-35", "Red t-shirt man",
                new TimeSpan(0, 0, 13)));
            compositions.Add(new Composition(3, 9, "Scr, scr, scr, scr", "Pharaoh",
                new TimeSpan(0, 2, 48)));
        }

        public void writeToFile(string path)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create));

            // writing all properties
            foreach (Composition current in compositions)
            {
                if (current != null)
                {
                    writer.Write(current.Name);
                    writer.Write(current.Id);
                    writer.Write(current.Performer);
                    writer.Write(current.Rating);
                    writer.Write(current.Length.ToString());
                }
            }
            writer.Close();
        }
        
            
    }
}
