using System;
using System.Collections.Generic;

namespace gamejam {
    public class Cards {
        private Random random = new Random();
        private List<ICard> unused = new List<ICard>();
        private List<ICard> used = new List<ICard>();

        public Cards() {
            for (int i = 0; i < 2; i++)
            {
                unused.Add(new ChunGeCard("春哥永生卡", 0));
            }

            // 请添加更多卡牌

            shuffleArray(unused);
        }

        public ICard pop(ICard.Level level) {
            if (unused.Count == 0) {
                unused = used;
                used = new List<ICard>();
                shuffleArray(unused);
            }
            ICard card = unused[0];
            unused.RemoveAt(0);
            return card;
        }

        public void push(ICard card) {
            used.Add(card);
        }

        private void shuffleArray(List<ICard> array) {
            for (int i = 0; i < array.Count; i++) { // shuffle
                int index = random.Next(i, array.Count);                
                ICard t = array[i];
                array[i] = array[index];
                array[index] = t;
            } 
        }
    }
}