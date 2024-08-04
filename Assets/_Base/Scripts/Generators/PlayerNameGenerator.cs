namespace _Base.Scripts.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayerNameGenerator
    {
        private static List<string> adjectives = new List<string>
        {
            "Able", "Brave", "Calm", "Daring", "Eager", "Fancy", "Gentle", "Happy", "Jolly", "Kind",
            "Lively", "Mighty", "Nice", "Odd", "Proud", "Quick", "Rapid", "Silly", "Tiny", "Ugly",
            "Vivid", "Witty", "Young", "Zany", "Bold", "Bright", "Charming", "Cool", "Dashing", "Elegant",
            "Fierce", "Funny", "Gleeful", "Handy", "Ideal", "Jovial", "Keen", "Lucky", "Noble", "Quaint",
            "Shiny", "Tall", "Unique", "Vast", "Worthy", "Yummy", "Zealous", "Amazing", "Bubbly", "Clever",
            "Dapper", "Excited", "Famous", "Glamorous", "Honest", "Inventive", "Joyful", "Kindly", "Loyal", "Mellow",
            "Neat", "Peaceful", "Quirky", "Sleek", "Tidy", "Upbeat", "Valiant", "Warm", "Zippy", "Artful",
            "Cheerful", "Dazzling", "Energetic", "Faithful", "Generous", "Helpful", "Jazzy", "Kindhearted", "Luminous",
            "Modest",
            "Nimble", "Playful", "Radiant", "Savvy", "Thrifty", "Uplifting", "Vigorous", "Whimsical", "Zephyr", "Bold",
            "Crafty", "Diligent", "Efficient", "Fearless", "Gracious", "Heroic", "Inspiring", "Jubilant", "Kooky",
            "Lively"
        };

        private static List<string> nouns = new List<string>
        {
            "Apple", "Bear", "Cat", "Dog", "Eagle", "Fox", "Giraffe", "Horse", "Iguana", "Jaguar",
            "Kangaroo", "Lion", "Monkey", "Nightingale", "Owl", "Penguin", "Quokka", "Rabbit", "Snake", "Tiger",
            "Umbrella", "Vulture", "Whale", "Yak", "Zebra", "Ant", "Bee", "Camel", "Dolphin", "Elephant",
            "Frog", "Goat", "Hawk", "Ibex", "Jellyfish", "Koala", "Lemur", "Moose", "Newt", "Ocelot",
            "Parrot", "Quail", "Raccoon", "Salmon", "Turtle", "Urchin", "Viper", "Wolf", "Xerus", "Yak",
            "Zebu", "Buffalo", "Cheetah", "Dingo", "Emu", "Falcon", "Gorilla", "Hippo", "Impala", "Jackal",
            "Kingfisher", "Lynx", "Meerkat", "Narwhal", "Octopus", "Panda", "Quetzal", "Raven", "Seahorse", "Toad",
            "Uakari", "Vicuna", "Wallaby", "Xrayfish", "Yellowjacket", "Zander", "Badger", "Crocodile", "Dragonfly",
            "Echidna",
            "Flamingo", "Gazelle", "Heron", "Ibis", "Jay", "Kiwi", "Llama", "Mongoose", "Nautilus", "Opossum"
        };

        private static Random random = new Random();

        public static List<string> GeneratePlayerNames(int amount)
        {
            List<string> names = new List<string>();

            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    string adjective = adjectives[random.Next(adjectives.Count)];
                    string noun = nouns[random.Next(nouns.Count)];
                    string baseName = adjective + noun;

                    if (baseName.Length >= 3 && baseName.Length <= 10)
                    {
                        if (random.NextDouble() < 0.5)
                        {
                            baseName += random.Next(0, 10).ToString();
                        }

                        names.Add(baseName);
                        break;
                    }
                }
            }

            return names;
        }
    }
}