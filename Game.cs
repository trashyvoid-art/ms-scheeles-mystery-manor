using System;
using System.Collections.Generic;
using static System.Console;
using System.IO;
using System.Threading.Tasks;

namespace MurderMystereee
{
    public class Game
    {
        Player player;
        Victim murderVic;
        // Victim removedVic;
        Location currentLocation;
        string choice;
        Victim randoVic;
        string unknown = "N/A";
        Map map;
        int steps;
        bool hiddenDoor = false;

        Victim Psychologist = new Victim();
        Victim Adventurer = new Victim();
        Victim Paleontologist = new Victim();
        Victim Doctor = new Victim();
        Victim Astronomer = new Victim();
        Victim Sailor = new Victim();
        Victim Scheele = new Victim();


        //text parsing stuff
        string[] bodyWords = { "wound", "neck", "arm", "wrist", "chest" };
        string[] clothing = { "vest", "coat", "pants", "skirt", "dress", "pocket" };

        string[] hidden = { "trap", "secret","door","hidden" };
        string[] weaponAdj = { "sharp", "pointy", "knife", "dagger", "hilt" };
        //string[] investiWords = { "blood", "print","clue","hint" };
        string[] roomWords = {"wall", "floor", "ceiling", "lights" };


        //chucking all the characters into an array
        Victim[] characterList = new Victim[]
        {

        };

        List<Item> advEvi = new List<Item>
        {
            //0
            new Item("Stab Wound", true,"A stabwound on the victim's neck"),

            //1
            new Item("Prescription", true,"A prescription for arsenic"),

            //2
            new Item("Red coat",true,"A red coat that smells like vanilla, and has glass shards from a perfume bottle in it."),

            //3
            new Item("Secret Door", true,"A secret door between the bar and the infirmary")
        };

        List<Item> infirmaryItems = new List<Item>
        {
            new Item("Spilled powder",false,"Powder of some sort that trails off into the wall."),
        };

        //instatiating murder weapons
        Item[] Weapons = new Item[]
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



        public Game()
        {
            player = new Player();
            //instatiating characters the player will interact with!
            map = new Map();

            Psychologist = new Victim("Scarlett", Weapons[1], "Psychologist", map.Locations[2], Sailor);
            Adventurer = new Victim("Sam", Weapons[0], "Adventurer", map.Locations[3], Psychologist);
            Paleontologist = new Victim("Sara", Weapons[0], "Paleontologist", map.Locations[1], Adventurer);
            Astronomer = new Victim("Shamsa", Weapons[3], "Astronomer", map.Locations[5], Doctor);
            Sailor = new Victim("Sean", Weapons[1], "Sailor", map.Locations[6], Astronomer);
            Doctor = new Victim("Sanchez", Weapons[2], "Doctor", map.Locations[4], Paleontologist);
            Scheele = new Victim("Ms. Scheele", Weapons[2], "Eccentric Millionaire", map.Locations[0], null);

           characterList = new Victim[]
             {
                Paleontologist, Psychologist, Adventurer,
                Astronomer, Sailor, Doctor, Scheele

             };

            //if i was more ambitious; i'd let the player pick the character.
            //in fact, most of the stuff commented out is stuff i wrtoe first and then
            //decided i need to size the scope down


            //WriteLine("Pick an occupation:");
            //foreach(Victim victim in characterList)
            //{
            //    WriteLine(victim.occupation);
            //    WriteLine("-----------");
            //}
            //choice = ReadLine();

            //foreach (Victim victim in characterList)
            //{
            //    if (choice.ToLower() == victim.occupation)
            //    {
            //        player.name = victim.name;
            //        player.occupation = victim.occupation;
            //        WriteLine($"You are now {player.name}, the {player.occupation}");

            //        //this was more so for the generate portion (lines 242 onward)
            //        removedVic = victim;
            //    }
            //}

            player.name = Paleontologist.name;
            player.occupation = Paleontologist.occupation;
            currentLocation = map.Locations[0];

            WriteLine($"You are {player.name}, the {player.occupation}. You have been invited to Ms. Scheele's " +
                $"estate for a fabulous dinner. The manor is wall-to-wall green, and stunningly spacious.");

            ReadKey();

            WriteLine("After everyone ate, you all left to do your own thing. Suddenly, the lights go off and you hear shuffling echoing throughout " +
                "the manor. You hear a whack! Or maybe it was a " +
                "scream? Whatever it was, the lights soon come on" +
                " and one of the other guests exclaim...");

            ReadKey();

            murderVic = Adventurer;

            Generate();
            WriteLine($"{randoVic.name}: My God! There's been a murder!");

            nextPage("e");

            WriteLine("Here is your current casefile:");
            CaseFile();
            ReadKey();

            WriteLine("Here is a partial map of Ms. Scheele's Estate");
            userMap();
            ReadKey();
            WriteLine("It might be best to find Ms. Scheele and talk to her first.");
            Choice();

        }

        public void Choice()
        {
            WriteLine("");
            WriteLine("What will you do?");
            WriteLine("-----------------");
            WriteLine("CASE FILE || TALK || INVENTORY || INVESTIGATE || MAP || MOVE");
            WriteLine("");
            choice = ReadLine();
            WriteLine("\n");
            int count = 0;
            switch (choice.ToLower())
            {
                case "map":
                    userMap();
                    break;

                case "case file":
                    CaseFile();
                    Deduce();
                    break;

                case "talk":
                    foreach (Victim npc in characterList)
                    {
                        if (npc.bodyloc.name == currentLocation.name)
                        {
                            npcDialogue(npc);
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        WriteLine("There's nobody to talk to here.");
                    }
                    break;

                case "move":
                    WriteLine($"CURRENT LOCATION: {currentLocation.name}");
                    WriteLine("MOVE TO...");
                    userMap();
                    Move();

                    break;

                case "inventory":

                    WriteLine("INVENTORY:");
                    //WriteLine(player.inventory.Count);
                    foreach (Item item in player.inventory)
                    {
                       if (item.evidence == false)
                        {
                            WriteLine($"* {item.name}");
                            WriteLine($"   {item.desc}");
                            WriteLine("-----------");
                            WriteLine("");
                        }
                    }
                    nextPage("e");

                    break;

                case "investigate":
                    WriteLine($"What would you like to investigate in the {currentLocation.name}?");
                    choice = ReadLine();
                    WriteLine("\n");
                    //maybe add hints for each room ?? just to make investigating easier 
                    if (choice.ToLower().Contains("body") || choice.ToLower().Contains(murderVic.name.ToLower()))
                    {
                        if (currentLocation == murderVic.bodyloc)
                        {
                            WriteLine("What are you searching on the body? The victim is laying on his back with blood coming from his neck. He also wears a thick coat.");

                            choice = ReadLine();
                            foreach (string i in clothing)
                            {
                                if (choice.ToLower().Contains(i))
                                {
                                    WriteLine("You found a prescription for arsenic in his pocket.");
                                    player.inventory.Add(advEvi[1]);
                                    break;
                                }
                            }

                            foreach (string i in bodyWords)
                            {
                                if (choice.ToLower().Contains(i))
                                {
                                    WriteLine("There is a stab wound on his neck.");
                                    player.inventory.Add(advEvi[0]);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            WriteLine("You are not in the same room as the body.");
                        }
                    }
                    else
                    {

                        switch (currentLocation.name)
                        {
                            case "Lobby":
                                WriteLine($"{Scheele.name}: Ah, ah, ah! What do you think you're doing searching my home in front of me?");
                                ReadKey();
                                WriteLine($"{Scheele.name}: Dear, have the decency to peruse my manor where I can't see.");
                                nextPage("e");
                                break;

                            case "Oddities Room":

                                string[] oddities = { "bone", "creature", "dinosaur" };

                                //make it so that people can take the dinoskull!!
                                foreach (string i in oddities)
                                {
                                    if (choice.ToLower().Contains(i))
                                    {
                                        WriteLine("You found a bloody dagger hapharzardly wedged into a dinosaur skull in plain sight.");
                                        player.inventory.Add(murderVic.murderer.weapon);
                                        unknown = murderVic.murderer.weapon.name;
                                        break;
                                    }
                                }

                                foreach(string i in weaponAdj)
                                {
                                    if (choice.ToLower().Contains(i))
                                    {

                                        WriteLine("You found rope.");
                                        Weapons[3].evidence = false;
                                        player.inventory.Add(Weapons[3]);
                                        
                                    }
                                }

                                break;

                            case "Office":

                                if (choice.ToLower().Contains("desk"))
                                {
                                    WriteLine("You found a revolver in the drawers of the desk.");
                                    Weapons[0].evidence = false;
                                    player.inventory.Add(Weapons[0]);
                                }

                                if (choice.ToLower().Contains("chair"))
                                {
                                    WriteLine("There's nothing wrong with the chair");
                                }


                                break;

                            case "Bar":
                                foreach (string i in hidden)
                                {
                                    if (choice.ToLower().Contains(i))
                                    {
                                        if(hiddenDoor == true)
                                        {
                                            WriteLine("You've already taken note of this.");

                                        }
                                        else
                                        {
                                            WriteLine("You found a secret door leading to the infirmary.");
                                            Notes("Confirmed existence of hidden door between Bar & Infirmary; could have been used in murder?");
                                            player.inventory.Add(advEvi[3]);
                                            hiddenDoor = true;
                                        }
                                        break;
                                    }
                                }
                                break;

                            case "Infirmary":

                                foreach (string i in hidden)
                                {
                                    if (choice.ToLower().Contains(i) || choice.ToLower().Contains("crack"))
                                    {
                                        if (hiddenDoor == true)
                                        {
                                            WriteLine("You've already taken note of this.");

                                        }
                                        else
                                        {
                                            WriteLine("You found a secret door leading to the infirmary.");
                                            Notes("Confirmed existence of hidden door between Bar & Infirmary; could have been used in murder?");
                                            player.inventory.Add(advEvi[3]);
                                            hiddenDoor = true;
                                        }
                                        break;
                                    }
                                }

                                foreach (string i in roomWords)
                                {
                                    if (choice.ToLower().Contains(i))
                                    {
                                        WriteLine("After looking around the room, you found spilled powder on the floor that seems to be leading to a crack in the wall.");
                                        player.inventory.Add(infirmaryItems[0]);
                                        break;
                                    }
                                }
                                break;

                            case "Sky Dome":
                                WriteLine("There's not much to look at here.");
                                break;
                            case "Greenhouse":
                                foreach (string i in hidden)
                                {
                                    if (choice.ToLower().Contains(i))
                                    {
                                        WriteLine("You found a red coat hidden under a plant that smells like vanilla, it has a broken perfume bottle in it.");
                                        Notes("The coat you saw the psychologist wearing earlier tonight was found in the greenhouse.");
                                        break;
                                    }
                                }
                                break;
                            case "Hallway":
                                foreach(string i in roomWords)
                                {
                                    if (choice.ToLower().Contains(i))
                                    {
                                        WriteLine("While searching the room, you find a stain on the carpet towards the greenhouse that smells like vanilla.");
                                        Notes("There's a stain which smells like vanilla.");
                                        break;
                                    }
                                }
                                break;
                            default:
                                WriteLine("There's nothing to investigate, or you didn't type the right thing.");
                                break;
                        }
                    }

                    break;

                default:
                    nextPage("");
                    WriteLine("Make a choice:");
                    break;
            }

            Choice();
        }

        public void CaseFile()
        {
            //add ascii title 
            WriteLine("");
            WriteLine($"VICTIM NAME: {murderVic.name}");
            WriteLine($"OCCUPATION: {murderVic.occupation}");
            WriteLine($"BODY LOCATION: {murderVic.bodyloc.name}");
            WriteLine($"MURDER WEAPON: {unknown}");
            WriteLine("EVIDENCE:");
            //add something to suggest the evidence needs to be found, isntead of leaving this blank at first.
            foreach (Item item in player.inventory)
            {
                if (item.evidence == true)
                {
                    WriteLine($"{item.name}");
                    WriteLine($"{item.desc}");
                    WriteLine("-----------");
                }
            }
        }

        public void Deduce()
        {
            WriteLine("");
            WriteLine("Would you care to make a deduction?");
            choice = ReadLine();
            if (choice == "yes")
            {
                WriteLine("Who do you think it is?");
                foreach (Victim npc in characterList)
                {
                    WriteLine(npc.name + " the " + npc.occupation);
                }
                choice = ReadLine();
                if (choice.ToLower() == murderVic.murderer.name.ToLower() && player.inventory.Contains(murderVic.murderer.weapon))
                {
                    WriteLine("You found the murderer! Press any key to end the game.");
                    ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    nextPage("");
                    WriteLine("That is not the murderer, or you have not found the murder weapon yet.");
                    ReadKey();
                }
            }
            else
            {
                WriteLine("Time to get on with investigating");
            }
            Choice();
        }

        public void Move()
        {
            choice = ReadLine();


            //if (steps == 4)
            //{

            //}
            //else
            // {

            nextPage("");
            //thanks to Karen Spriggs for help with this
                for (int i = 0; i < map.Locations.Length; i++)
                {
                    if (choice.ToLower() == map.Locations[i].name.ToLower())
                    {
                        currentLocation = map.Locations[i];
                        WriteLine($"You are now in {currentLocation.name}. {currentLocation.desc}");
                        steps++;
                    }
                }

                foreach (Victim npc in characterList)
                {
                    if (npc.bodyloc.name == currentLocation.name)
                    {
                        WriteLine($"Currently, the {npc.occupation}, {npc.name}, is here.");
                    }
                }
            // }
            ReadKey();
            //foreach (Location loc in map.Locations)
            //{
            //    if (choice.ToLower() == loc.name)
            //    {
            //        currentLocation = loc;
            //        WriteLine($"You are now in {currentLocation.name}");           
            //        steps++;
            //        ReadKey();
            //    }
            //}
        }

        public void userMap()
        {
            //add small ascii title that says map of the estate
            WriteLine("\n");
            foreach (Location location in map.Locations)
            {
                WriteLine(location.name);

                //making sure that if the victim's body is in that location,
                //the game will say so
                if (murderVic.bodyloc == location)
                {
                    WriteLine("The body is here.");
                }
                else if (currentLocation == location)
                {
                    WriteLine("You are here");
                }
                WriteLine("-----------");
            }
        }

        public void npcDialogue(Victim NPC)
        {
            nextPage("");
            switch (NPC.name)
            {
                case "Sanchez":
                    switch (Doctor.talkCounter)
                    {
                        case 0:
                            WriteLine($"{Doctor.name}: Ah, the {player.occupation}! I hope you're well... Among the, erm.. Circumstances.");
                            nextPage("e");
                            WriteLine("YOU ASKED ABOUT AN ALIBI");
                            WriteLine($"{Doctor.name}: This is going to sound incredibly incriminating, but I " +
                                $"can confirm I spent some time with {murderVic.name} before his untimely demise..");
                            ReadKey();
                            WriteLine($"{Doctor.name}: But, I swear! We were having a great time, I wouldn't have" +
                                $" any reason against him. I'm afraid I didn't see anything, either, though...");
                            ReadKey();
                            WriteLine($"{Doctor.name}: I had a little too much, y'know, so I went to the bathroom. On my way there, I bumped into someone... " +
                                $"I can't tell you who it was, sorry.");
                            Notes("The doctor was with the adventurer shortly before the lights went off. He bumped into a stranger during the outage.");
                            Doctor.talkCounter++;
                            nextPage("e");
                            break;

                        case 1:
                            WriteLine($"{Doctor.name}: Hey! How's the clue hunting? I got something for you.");
                            ReadKey();
                            WriteLine($"{Doctor.name}: I remembered something about the person I bumped into! They smelled like vanilla. Does that help?");
                            Notes("The main suspect likely smells like vanilla");
                            Doctor.talkCounter++;
                            break;

                        case 2:
                            WriteLine($"{Doctor.name}: Judging by the wounds, it looks like his neck was torn up. It was either a vicious beast or, well... One of those daggers with jagged edges, I suppose. I thought I saw something" +
                                $" like that somewhere in this manor but I can't for the life of me recall where...");
                            Notes("The murder weapon is likely a serrated dagger");
                            Doctor.talkCounter++;
                            break;

                        case 3:
                            WriteLine($"{Doctor.name}: Ah, you might find it if you searched his pockets, but I did write up a prescription for arsenic.. ");
                            Doctor.talkCounter++;
                            break;
                        default:
                            WriteLine($"{Doctor.name}: Perdon?");
                            break;
                    }
                    break;
                case "Scarlett":
                    switch (Psychologist.talkCounter)
                    {
                        case 0:
                            WriteLine($"{Psychologist.name}: Good evening.");
                            WriteLine("YOU ASKED FOR AN ALIBI");
                            WriteLine($"{Psychologist.name}: Yes, I suppose you'd be the" +
                                $" one to collect everyone's alibi.");
                            ReadKey();
                            WriteLine($"{Psychologist.name}: I was in the infirmary, of course, " +
                                $"admiring the selection of pharmeceuticals our gracious host " +
                                $"Ms. Scheele keeps stocked up... For" +
                                $" some apparent reason.");
                            Notes("The psychologist was in the infirmary, looking at the medicine cabinet, when the lights went off.");
                            ReadKey();
                            Psychologist.talkCounter++;
                            break;

                        case 1:
                            WriteLine("YOU ASKED ABOUT CLUES.");
                            WriteLine($"{Psychologist.name}: I have no idea what you're talking about.");
                            Notes("The psychologist seems annoyed.");
                            ReadKey();
                            Psychologist.talkCounter++;
                            break;

                        case 2:
                            WriteLine($"{Psychologist.name}: I don't appreciate your hovering.");
                            Psychologist.talkCounter++;
                            break;

                        case 3:
                            WriteLine($"{Psychologist.name}: That's enough.");
                            Notes("The psychologist refuses to talk.");
                            break;

                        default:
                            WriteLine($"{Psychologist.name}: Excuse me?");
                            break;
                    }
                    break;
                case "Shamsa":
                    switch (Astronomer.talkCounter)
                    {
                        case 0:
                            WriteLine($"{Astronomer.name}: Hello.");
                            nextPage("e");
                            WriteLine("YOU ASKED FOR AN ALIBI");
                            WriteLine($"{Astronomer.name}: I see. Of course. I've been avoiding the {murderVic.occupation} all night, " +
                                $"so I've simply been wandering around the halls with the sailor. He can confirm my company, and I, him.");
                            Astronomer.talkCounter++;
                            Notes("The astronomer, was with the sailor, when the lights went out.");
                            break;

                        case 1:
                            WriteLine($"{Astronomer.name}: Come to think of it... I believe Sean and I" +
                                $" saw the doctor going into the bar with our dearly departed victim. " +
                                $"You may want to confirm with him, however.");
                            Astronomer.talkCounter++;
                            Notes("The astronomer and the sailor saw someone go into the bar with the adventurer.");
                            break;

                        case 2:
                            WriteLine($"{Astronomer.name}: Have you checked the body? I'm too squemish..");
                            Astronomer.talkCounter++;
                            break;

                        case 3:
                            WriteLine($"{Astronomer.name}: Unfortunately, I don't have anything relevant.");
                            Astronomer.talkCounter++;
                            break;
                        default:
                            WriteLine($"{Astronomer.name}: Hm?");
                            break;
                    }
                    break;
                case "Sean":
                    switch (Sailor.talkCounter)
                    {
                        case 0:
                            WriteLine($"{Sailor.name}: Hm? Oh? Hey, what's up?");
                            nextPage("e");
                            WriteLine("YOU ASKED FOR AN ALIBI");
                            WriteLine($"{Sailor.name}: Alibi? Ah? Ahah, yeah, I was with Shamsa all night.");
                            ReadKey();
                            WriteLine($"{Sailor.name}: Yeah, yeah, we were just sorta, well, wanderin' 'round 'till the lights went off.");
                            ReadKey();
                            WriteLine($"{Sailor.name}: She said she needed some distance after the body was discovered, but she was with me until after someone found Sammy boy taking a dirt nap.");
                            Notes("The sailor confirmed he was with the astronomer all night.");
                            Sailor.talkCounter++;
                            break;

                        case 1:
                            WriteLine($"{Sailor.name}: Huh? Have I found anything? Well, geez, when I was doing my own personal investigative wanderin' earlier, I guess..");
                            ReadKey();
                            WriteLine($"{Sailor.name}: Well, I saw a hole in the wall, I think? Yeah, yeah, in the, uh, bar.");
                            ReadKey();
                            WriteLine($"{Sailor.name}: Yeah, it was in the bar! I may have, er, kicked it to check, but it's still put together. Dunno where it goes, though, I don't pay attention.");
                            Notes("Check the bar for something hidden");
                            Sailor.talkCounter++;
                            break;

                        case 2:
                            WriteLine($"{Sailor.name}: {murderVic.name}, oh, {murderVic.name}, that hearty bastard. He promised he'd drink me under the table, not 6 feet under! What a loss.");
                            Sailor.talkCounter++;
                            break;

                        case 3:
                            WriteLine($"{Sailor.name}: We just met, but there's something familiar about everyone here... Hm...");
                            Sailor.talkCounter++;
                            break;
                        default:
                            WriteLine($"{Sailor.name}: Ah?");
                            break;

                    }
                    break;
                case "Ms. Scheele":
                    switch (Scheele.talkCounter)
                    {
                        case 0:
                            WriteLine($"{Scheele.name}: Hello, dearest {player.name}, how do you do?");
                            WriteLine("YOU ASKED FOR AN ALIBI");
                            WriteLine($"{Scheele.name}: My alibi? My sweet {player.occupation}, I am a noble lady.");
                            ReadKey();
                            WriteLine($"{Scheele.name}: If I were to commit murder, it certainly would not be a guest!");
                            ReadKey();
                            WriteLine($"{Scheele.name}: But I can confirm that I was in the lobby, same as you, when the lights went out " +
                                "and we heard that awful scuffling. I'd hate to think how difficult it would be to get the stains out...");
                            nextPage("e");
                            WriteLine($"{Scheele.name}: Why, {player.name}, are you suggesting I know what happened?");
                            ReadKey();
                            WriteLine($"{Scheele.name}: Well... I might, but that is simply up to you to figure out, is it not?");
                            ReadKey();
                            WriteLine($"{Scheele.name}: If I know anything.. I'll be sure to give you 3 chances to ask, should you need the clue.");
                            nextPage("e");
                            nextPage("e");
                            WriteLine($"{Scheele.name}: ..Oh! Before I forget, dear {player.occupation}, you should look in the debug folder of this little game. For your notebook.");
                            ReadKey();
                            WriteLine($"{Scheele.name}: You don't know what I'm talking about? *laughs* I'm sure you do. Look, I've even got you started.");
                            Notes("It's easier to investigate if you use words like \"hidden\" or \"secret\".\nIt's easier to use descriptors when investigating. \nDo not doubt Ms. Scheele.");
                            Scheele.talkCounter++;
                            Notes("Ms. Scheele was with you in the lobby when the lights went out.");
                            break;

                        case 1:
                            WriteLine($"{Scheele.name}: Hello again, fair {player.occupation}. Here's your first clue; my mansion is full of surprises. Be sure to make your search thorough.");
                            break;

                        case 2:
                            WriteLine($"{Scheele.name}: Ah, {player.name}, I was wondering about when you'd need a hint. If you haven't already found the weapon, perhaps you can guess from inspecting the wound.");
                            break;

                        case 3:
                            WriteLine($"{Scheele.name}: This will be my last hint to you, dear {player.occupation}. Some perfume is missing from my dresser. Have you found it?");
                            break;

                        default:
                            WriteLine($"{Scheele.name}: I beg your pardon?");
                            break;

                    }
                    break;

            }
        }

        //in a more ambitious world, each time you boot up the game, you
        //can investigate a different murder

        public void Generate()
        {
            Random rando = new Random();
            int index = rando.Next(characterList.Length);
            randoVic = characterList[index];

            //making sure that murdervic / player character
            // isn't the same as the random guest that yells
            if (randoVic == murderVic || randoVic.name == player.name || randoVic.name == "Ms. Scheele" )
            {
                Generate();
            }
        }


        int entryNumber = 1;
        // thanks janell for walking me through this!
        // add a way to let players use the notebook for their own notes
        // add notebook as a choice option
        public void Notes(string notes)
        {

            StreamWriter file = File.AppendText("Notebook.txt");
            file.WriteLine($"Entry No.{entryNumber}\n-----------------------\n");
            file.WriteLine(notes);
            file.WriteLine("\n-----------------------");
            entryNumber++;
            file.Close();
        }

        string _ent;
        public void nextPage(string _ent)
        {

            if (_ent == "e")
            {
                ReadKey();
            }
            if (_ent == "")
            {

            }
            
            Clear();
            ForegroundColor = ConsoleColor.Green;

            WriteLine(@"
███╗░░░███╗░██████╗  ░██████╗░█████╗░██╗░░██╗███████╗███████╗██╗░░░░░███████╗██╗░██████╗
████╗░████║██╔════╝  ██╔════╝██╔══██╗██║░░██║██╔════╝██╔════╝██║░░░░░██╔════╝╚█║██╔════╝
██╔████╔██║╚█████╗░  ╚█████╗░██║░░╚═╝███████║█████╗░░█████╗░░██║░░░░░█████╗░░░╚╝╚█████╗░
██║╚██╔╝██║░╚═══██╗  ░╚═══██╗██║░░██╗██╔══██║██╔══╝░░██╔══╝░░██║░░░░░██╔══╝░░░░░░╚═══██╗
██║░╚═╝░██║██████╔╝  ██████╔╝╚█████╔╝██║░░██║███████╗███████╗███████╗███████╗░░░██████╔╝
╚═╝░░░░░╚═╝╚═════╝░  ╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚══════╝╚══════╝░░░╚═════╝░

███╗░░░███╗██╗░░░██╗░██████╗████████╗███████╗██████╗░██╗░░░██╗  ███╗░░░███╗░█████╗░███╗░░██╗░█████╗░██████╗░
████╗░████║╚██╗░██╔╝██╔════╝╚══██╔══╝██╔════╝██╔══██╗╚██╗░██╔╝  ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗
██╔████╔██║░╚████╔╝░╚█████╗░░░░██║░░░█████╗░░██████╔╝░╚████╔╝░  ██╔████╔██║███████║██╔██╗██║██║░░██║██████╔╝
██║╚██╔╝██║░░╚██╔╝░░░╚═══██╗░░░██║░░░██╔══╝░░██╔══██╗░░╚██╔╝░░  ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██╗
██║░╚═╝░██║░░░██║░░░██████╔╝░░░██║░░░███████╗██║░░██║░░░██║░░░  ██║░╚═╝░██║██║░░██║██║░╚███║╚█████╔╝██║░░██║
╚═╝░░░░░╚═╝░░░╚═╝░░░╚═════╝░░░░╚═╝░░░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░  ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝░╚════╝░╚═╝░░╚═╝");

            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\n\n                PRESS ENTER TO CONTINUE");
        }
    }
}
