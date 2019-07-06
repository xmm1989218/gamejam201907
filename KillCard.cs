using System.Collections.Generic;

namespace gamejam {
    public class KillCard : ICard
    {
        public KillCard(string name, int duration) 
            : base(name, duration)
        {
        }

        public override List<Person> effectV1(int type, Human human)
        {
            return human.killByCount(type, 100);
        }

        public override List<Person> effectV2(int type, Human human)
        {
            return human.killByCount(type, 150);
        }

        public override List<Person> effectV3(int type, Human human)
        {
            return human.killByCount(type, 200);
        }
    }
}