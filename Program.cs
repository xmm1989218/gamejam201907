using System;

namespace gamejam
{
    class Program
    {
        static void Main(string[] args)
        {
            Human human = new Human(1000, 750);
            // 杀红色
            {
                // <= 0 未非持续效果牌  单位秒
                ICard card = new KillCard("kill", 0);
                var killed = card.effect(Human.RED, human);   
            }
            // 复活红色
            {
                ICard card = new KillCard("春哥", 0);
                card.setLevel(ICard.Level.V2);
                var killed = card.effect(Human.RED, human);   
            }
            // 进行一波自然转化
            human.round();
        }
    }
}
