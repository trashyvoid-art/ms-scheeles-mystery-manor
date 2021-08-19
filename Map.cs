using System;
namespace MurderMystereee
{
    public class Map 
    {
        //instatiating locations
       public Location[] Locations = new Location[]
        {
            //0
            new Location("Lobby", "A large entrance with patterned, dark wooden " +
                "flooring. The walls are a marbled green, and in the center of " +
                "the ceiling an emerald chandelier."),

            //1
            new Location("Oddities Room","A room full of oddities and bones of creatures " +
                "thought to have lived long before humans. This room is lined to the brim with bookshelves, " +
                "charts and models"),

            //2
            new Location("Office","An office with a half-wall trim; wood panelling and dark green paint. There is a lounge chair, a" +
                " desk, and a large, green rug in the center."),

            //3
            new Location("Bar","A bar room with a large gathering area and a counter to the back. The counter is stocked with " +
                "alcohol and non-alcoholic drinks."),

            //4
            new Location("Infirmary","Green linoleum and green cushioning; the infirmary has medical diagrams, a pharmaceutical cabinet, and some " +
                "odd tools at the desk."),

            //5
            new Location("Sky Dome","A domed enclosure of the mansion with constellations painted along the cieling. There is a telescope to the back, as well as a large model of the " +
                "Solar System in the middle of the room."),

            //6
            new Location("Greenhouse","The most green room of the house; plants from all over the world are lined in " +
                "rows and sometimes columns. The room has vines growing along any rare crack or groove."),

            //7
            new Location("Hallway","A long stretch of hall with doors leading to rooms from the side. Connects to the Lobby and Greenhouse.")
        };
    }
}
