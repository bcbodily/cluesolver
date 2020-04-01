using System;
using System.Collections.Generic;

namespace cluesolver
{
    public static class ClueMasterDetective
    {
        private static string CATEGORY_ROOMS = "room";
        private static string CATEGORY_SUSPECTS = "suspect";
        private static string CATEGORY_WEAPONS = "weapon";

        private static string NAME_SUSPECT_COL_MUSTARD = "Col. Mustard";
        private static string NAME_SUSPECT_M_BRUNETTE = "M. Brunette";
        private static string NAME_SUSPECT_MISS_PEACH = "Miss Peach";
        private static string NAME_SUSPECT_MISS_SCARLET = "Miss Scarlet";
        private static string NAME_SUSPECT_MME_ROSE = "Mme. Rose";
        private static string NAME_SUSPECT_MR_GREEN = "Mr. Green";
        private static string NAME_SUSPECT_MRS_PEACOCK = "Mrs. Peacock";
        private static string NAME_SUSPECT_MRS_WHITE = "Mrs. White";
        private static string NAME_SUSPECT_PROF_PLUM = "Prof. Plum";
        private static string NAME_SUSPECT_SGT_GRAY = "Sgt. Gray";


        private static string NAME_ROOM_BILLIARD_ROOM = "Billiard Room";
        private static string NAME_ROOM_CARRIAGE_HOUSE = "Carriage House";
        private static string NAME_ROOM_CONSERVATORY = "Conservatory";
        private static string NAME_ROOM_COURTYARD = "Courtyard";
        private static string NAME_ROOM_DINING_ROOM = "Dining Room";
        private static string NAME_ROOM_DRAWING_ROOM = "Drawing Room";
        private static string NAME_ROOM_FOUNTAIN = "Fountain";
        private static string NAME_ROOM_GAZEBO = "Gazebo";
        private static string NAME_ROOM_KITCHEN = "Kitchen";
        private static string NAME_ROOM_LIBRARY = "Library";
        private static string NAME_ROOM_STUDIO = "Studio";
        private static string NAME_ROOM_TROPHY_ROOM = "Trophy Room";

        private static string NAME_WEAPON_CANDLESTICK = "Candlestick";
        private static string NAME_WEAPON_HORSESHOE = "Horseshoe";
        private static string NAME_WEAPON_KNIFE = "Knife";
        private static string NAME_WEAPON_LEAD_PIPE = "Lead Pipe";
        private static string NAME_WEAPON_POISON = "Poison";
        private static string NAME_WEAPON_REVOLVER = "Revolver";
        private static string NAME_WEAPON_ROPE = "Rope";
        private static string NAME_WEAPON_WRENCH = "Wrench";

        public static Card CARD_ROOM_BILLIARD_ROOM = new Card(CATEGORY_ROOMS, NAME_ROOM_BILLIARD_ROOM);
        public static Card CARD_ROOM_CARRIAGE_HOUSE = new Card(CATEGORY_ROOMS, NAME_ROOM_CARRIAGE_HOUSE);
        public static Card CARD_ROOM_CONSERVATORY = new Card(CATEGORY_ROOMS, NAME_ROOM_CONSERVATORY);
        public static Card CARD_ROOM_COURTYARD = new Card(CATEGORY_ROOMS, NAME_ROOM_COURTYARD);
        public static Card CARD_ROOM_DINING_ROOM = new Card(CATEGORY_ROOMS, NAME_ROOM_DINING_ROOM);
        public static Card CARD_ROOM_DRAWING_ROOM = new Card(CATEGORY_ROOMS, NAME_ROOM_DRAWING_ROOM);
        public static Card CARD_ROOM_FOUNTAIN = new Card(CATEGORY_ROOMS, NAME_ROOM_FOUNTAIN);
        public static Card CARD_ROOM_GAZEBO = new Card(CATEGORY_ROOMS, NAME_ROOM_GAZEBO);
        public static Card CARD_ROOM_KITCHEN = new Card(CATEGORY_ROOMS, NAME_ROOM_KITCHEN);
        public static Card CARD_ROOM_LIBRARY = new Card(CATEGORY_ROOMS, NAME_ROOM_LIBRARY);
        public static Card CARD_ROOM_STUDIO = new Card(CATEGORY_ROOMS, NAME_ROOM_STUDIO);
        public static Card CARD_ROOM_TROPHY_ROOM = new Card(CATEGORY_ROOMS, NAME_ROOM_TROPHY_ROOM);

        public static Card CARD_SUSPECT_COL_MUSTARD = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_COL_MUSTARD);
        public static Card CARD_SUSPECT_M_BRUNETTE = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_M_BRUNETTE);
        public static Card CARD_SUSPECT_MISS_PEACH = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_MISS_PEACH);
        public static Card CARD_SUSPECT_MISS_SCARLET = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_MISS_SCARLET);
        public static Card CARD_SUSPECT_MME_ROSE = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_MME_ROSE);
        public static Card CARD_SUSPECT_MR_GREEN = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_MR_GREEN);
        public static Card CARD_SUSPECT_MRS_PEACOCK = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_MRS_PEACOCK);
        public static Card CARD_SUSPECT_MRS_WHITE = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_MRS_WHITE);
        public static Card CARD_SUSPECT_PROF_PLUM = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_PROF_PLUM);
        public static Card CARD_SUSPECT_SGT_GRAY = new Card(CATEGORY_SUSPECTS, NAME_SUSPECT_SGT_GRAY);

        public static Card CARD_WEAPON_CANDLESTICK = new Card(CATEGORY_WEAPONS, NAME_WEAPON_CANDLESTICK);
        public static Card CARD_WEAPON_HORSESHOE = new Card(CATEGORY_WEAPONS, NAME_WEAPON_HORSESHOE);
        public static Card CARD_WEAPON_KNIFE = new Card(CATEGORY_WEAPONS, NAME_WEAPON_KNIFE);
        public static Card CARD_WEAPON_LEAD_PIPE = new Card(CATEGORY_WEAPONS, NAME_WEAPON_LEAD_PIPE);
        public static Card CARD_WEAPON_POISON = new Card(CATEGORY_WEAPONS, NAME_WEAPON_POISON);
        public static Card CARD_WEAPON_REVOLVER = new Card(CATEGORY_WEAPONS, NAME_WEAPON_REVOLVER);
        public static Card CARD_WEAPON_ROPE = new Card(CATEGORY_WEAPONS, NAME_WEAPON_ROPE);
        public static Card CARD_WEAPON_WRENCH = new Card(CATEGORY_WEAPONS, NAME_WEAPON_WRENCH);

        public static ISet<Card> CARDS_ROOMS = new SortedSet<Card> { CARD_ROOM_BILLIARD_ROOM, CARD_ROOM_CARRIAGE_HOUSE, CARD_ROOM_CONSERVATORY, CARD_ROOM_COURTYARD, CARD_ROOM_DINING_ROOM, CARD_ROOM_DRAWING_ROOM, CARD_ROOM_FOUNTAIN, CARD_ROOM_GAZEBO, CARD_ROOM_KITCHEN, CARD_ROOM_LIBRARY, CARD_ROOM_STUDIO, CARD_ROOM_TROPHY_ROOM };

        public static ISet<Card> CARDS_SUSPECTS = new SortedSet<Card> { CARD_SUSPECT_COL_MUSTARD, CARD_SUSPECT_M_BRUNETTE, CARD_SUSPECT_MISS_PEACH, CARD_SUSPECT_MISS_SCARLET, CARD_SUSPECT_MME_ROSE, CARD_SUSPECT_MR_GREEN, CARD_SUSPECT_MRS_PEACOCK, CARD_SUSPECT_MRS_WHITE, CARD_SUSPECT_PROF_PLUM, CARD_SUSPECT_SGT_GRAY };

        public static ISet<Card> CARDS_WEAPONS = new SortedSet<Card> { CARD_WEAPON_CANDLESTICK, CARD_WEAPON_HORSESHOE, CARD_WEAPON_KNIFE, CARD_WEAPON_LEAD_PIPE, CARD_WEAPON_POISON, CARD_WEAPON_REVOLVER, CARD_WEAPON_ROPE, CARD_WEAPON_WRENCH };

        public static ISet<Card> CARDS_ALL = new SortedSet<Card> { CARD_ROOM_BILLIARD_ROOM, CARD_ROOM_CARRIAGE_HOUSE, CARD_ROOM_CONSERVATORY, CARD_ROOM_COURTYARD, CARD_ROOM_DINING_ROOM, CARD_ROOM_DRAWING_ROOM, CARD_ROOM_FOUNTAIN, CARD_ROOM_GAZEBO, CARD_ROOM_KITCHEN, CARD_ROOM_LIBRARY, CARD_ROOM_STUDIO, CARD_ROOM_TROPHY_ROOM, CARD_SUSPECT_COL_MUSTARD, CARD_SUSPECT_M_BRUNETTE, CARD_SUSPECT_MISS_PEACH, CARD_SUSPECT_MISS_SCARLET, CARD_SUSPECT_MME_ROSE, CARD_SUSPECT_MR_GREEN, CARD_SUSPECT_MRS_PEACOCK, CARD_SUSPECT_MRS_WHITE, CARD_SUSPECT_PROF_PLUM, CARD_SUSPECT_SGT_GRAY, CARD_WEAPON_CANDLESTICK, CARD_WEAPON_HORSESHOE, CARD_WEAPON_KNIFE, CARD_WEAPON_LEAD_PIPE, CARD_WEAPON_POISON, CARD_WEAPON_REVOLVER, CARD_WEAPON_ROPE, CARD_WEAPON_WRENCH };
    }
}