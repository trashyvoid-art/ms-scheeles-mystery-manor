using System;
namespace MurderMystereee
{
    public class Item
    {
        public string name;
        public bool evidence;
        public string desc;

        public Item(string _name, bool _evi, string _desc)
        {
            name = _name;
            evidence = _evi;
            desc = _desc;
        }
    }
}
