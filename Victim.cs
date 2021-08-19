using System;
namespace MurderMystereee
{
    public class Victim : Person
    {
        public Item weapon;
        public Location bodyloc;
        public Victim murderer;
        public int talkCounter;

        public Victim(string _name, Item _weap, string _occ, Location _bodyLoc, Victim _murder)
        {
            name = _name;
            weapon = _weap;
            occupation = _occ;
            bodyloc = _bodyLoc;
            murderer = _murder;
        }
        public Victim() { }
        //instatiating murder weapons
        public Item[] Weapons = new Item[]
        {
            //0
            new Item("Revolver",true,"A revolver that's missing a bullet"),

            //1
            new Item("Dagger", true, "A serrated dagger"),

            //2
            new Item("Ice Pick", true,"An ice pick used for chipping ice... Or other things?"),

            //3
            new Item("Rope",true,"A lengthy piece of rope"),

            //4
            new Item("Shovel",true,"A tool used for gardening")

        };
    }
}
