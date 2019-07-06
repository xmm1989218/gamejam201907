using System.Collections.Generic;

namespace gamejam {
    public class ChunGeCard : ICard
    {
        public ChunGeCard(string name, int duration) 
            : base(name, duration)
        {
        }

        public override List<Person> effectV1(int type, Human human)
        {
            return human.reviveByCount(type, 100, 10);
        }

        public override List<Person> effectV2(int type, Human human)
        {
            return human.reviveByCount(type, 150, 20);
        }

        public override List<Person> effectV3(int type, Human human)
        {
            return human.reviveByCount(type, 200, 30);
        }
    }
}