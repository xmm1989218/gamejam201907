using System;
using System.Collections.Generic;

namespace gamejam {
    public class Human {
        public static readonly int RED = 1;
        public static readonly int BLUE = 2;
        public static readonly int WHITE = 4;
        
        private Random random = new Random();
        private List<Person> human = null;
        private List<Person> blue_alive = null;
        private List<Person> blue_dead = null;
        private List<Person> red_alive = null;
        private List<Person> red_dead = null;
        private List<Person> white_alive = null;
        private List<Person> white_dead = null;

        private int blue_nature_transfer = -1;
        private int red_nature_transfer = +1;

        public Human(int total, int no_faith) {
            int half = (total - no_faith) / 2;
            no_faith = total - 2 * half;
            blue_alive = new List<Person>(half);
            blue_dead = new List<Person>();
            red_alive = new List<Person>(half);
            red_dead = new List<Person>();
            white_alive = new List<Person>(no_faith);
            white_dead = new List<Person>();

            for (int i = 0; i < no_faith; i++)
            { // white
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(-25, 25)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setStrong(random.Next(0, 100) >= 50 ? true : false);
                person.setSensibility(new RangeValue(-10, 10, random.Next(0, 100)));
                white_alive.Add(person);
            }
            for (int i = 0; i < half; i++)
            { // blue
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(-100, -25)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setStrong(random.Next(0, 100) >= 50 ? true : false);
                person.setSensibility(new RangeValue(-10, 10, random.Next(0, 100)));
                blue_alive.Add(person);
            }
            for (int i = 0; i < half; i++)
            { // red
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(25, 100)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setStrong(random.Next(0, 100) >= 50 ? true : false);
                person.setSensibility(new RangeValue(-10, 10, random.Next(0, 100)));
                red_alive.Add(person);
            }

            shuffleArray(white_alive);
            shuffleArray(blue_alive);
            shuffleArray(red_alive);
        }

        public int getTotalHuman() { return human.Count; }
        public int getAliveBlue() { return blue_alive.Count; }
        public int getDeadBlue() { return blue_dead.Count; }
        public int getAliveRed() { return red_alive.Count; }
        public int getDeadRed() { return red_dead.Count; }
        public int getAliveWhite() { return white_alive.Count; }
        public int getDeadWhite() { return white_dead.Count; }

        public int getBlueNatureTransfer() { return blue_nature_transfer; }
        public void setBlueNatureTransfer(int n) { blue_nature_transfer = n; }
        public int getRedNatureTransfer() { return red_nature_transfer; }
        public void setRedNatureTransfer(int n) { red_nature_transfer = n; }

        private int getAliveCountByType(int type) {
            int max = 0;
            if ((type & RED) != 0) {
                max = max + red_alive.Count;
            }
            if ((type & BLUE) != 0) {
                max = max + blue_alive.Count;
            }
            if ((type & WHITE) != 0) {
                max = max + white_alive.Count;
            }
            return max;
        }

        // 按数量诞生 type  可以是RED|BLUE|WHITE的组合
        public List<Person> bornByCount(int type, int count) {
            List<Person> group = new List<Person>();
            if ((type & RED) != 0) {
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(25, 100)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setStrong(random.Next(0, 100) >= 50 ? true : false);
                person.setSensibility(new RangeValue(-10, 10, random.Next(0, 100)));
                group.Add(person);
            }
            if ((type & BLUE) != 0) {
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(-100, -25)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setStrong(random.Next(0, 100) >= 50 ? true : false);
                person.setSensibility(new RangeValue(-10, 10, random.Next(0, 100)));
                group.Add(person);
            }
            if ((type & WHITE) != 0) {
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(-25, 25)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setStrong(random.Next(0, 100) >= 50 ? true : false);
                person.setSensibility(new RangeValue(-10, 10, random.Next(0, 100)));
                group.Add(person);
            }
            return group;
        }

        // 按数量转化 type RED|BLUE|WHITE  不可以组合
        public List<Person> transferByCount(int from_type, int to_type, int count) {
            List<Person> from = null;
            List<Person> to = null;

            if ((from_type & RED) != 0) {
                from = red_alive;
            } else if ((from_type & BLUE) != 0) {
                from = blue_alive;
            } else if ((from_type & WHITE) != 0) {
                from = white_alive;
            }

            if ((to_type & RED) != 0) {
                to = red_alive;
            } else if ((from_type & BLUE) != 0) {
                to = blue_alive;
            } else if ((from_type & WHITE) != 0) {
                to = white_alive;
            }

            List<Person> group = move(from, to, count);
            for (int i = 0; i < group.Count; i++) {
                if ((to_type & RED) != 0)
                {
                    group[i].getFaith().setAsMax();
                }
                else if ((to_type & BLUE) != 0)
                {
                    group[i].getFaith().setAsMin();
                }
                else
                {
                    group[i].getFaith().setAsMiddle();
                }
            }
            return group;
        }

        // 按数量杀人 type  可以是RED|BLUE|WHITE的组合
        public List<Person> killByCount(int type, int count) {
            int max = getAliveCountByType(type);
            if (count >= max) count = max;
            List<Person> result = new List<Person>(count);
            if ((type & RED) != 0) {
                int t = count * red_alive.Count / max;
                result.AddRange(move(red_alive, red_dead, t));
            }
            if ((type & BLUE) != 0) {
                int t = count * blue_alive.Count / max;
                result.AddRange(move(blue_alive, blue_dead, t));
            }
            if ((type & WHITE) != 0) {
                int t = count * white_alive.Count / max;
                result.AddRange(move(white_alive, white_dead, t));
            }
            for (int i = 0; i < result.Count; i++) {
                result[i].kill();
            }
            return result;
        }

        // 按比例杀人 type  可以是RED|BLUE|WHITE的组合
        public List<Person> killByPercent(int type, int percent) {
            int max = getAliveCountByType(type);
            int count = max * percent / 100;
            List<Person> result = new List<Person>(count);
            if ((type & RED) != 0) {
                int t = count * red_alive.Count / max;
                result.AddRange(move(red_alive, red_dead, t));
            }
            if ((type & BLUE) != 0) {
                int t = count * blue_alive.Count / max;
                result.AddRange(move(blue_alive, blue_dead, t));
            }
            if ((type & WHITE) != 0) {
                int t = count * white_alive.Count / max;
                result.AddRange(move(white_alive, white_dead, t));
            }
            for (int i = 0; i < result.Count; i++) {
                result[i].kill();
            }
            return result;
        }

        // 按数量重生 type  可以是RED|BLUE|WHITE的组合
        public List<Person> reviveByCount(int type, int count, int percent_blood) {
            int max = getDeadCountByType(type);
            if (count >= max) count = max;
            List<Person> result = new List<Person>(count);
            if ((type & RED) != 0) {
                int t = count * red_dead.Count / max;
                result.AddRange(move(red_dead, red_alive, t));
            }
            if ((type & BLUE) != 0) {
                int t = count * blue_dead.Count / max;
                result.AddRange(move(blue_dead, blue_alive, t));
            }
            if ((type & WHITE) != 0) {
                int t = count * white_dead.Count / max;
                result.AddRange(move(white_dead, white_alive, t));
            }
            for (int i = 0; i < result.Count; i++) {
                result[i].revive(percent_blood);
            }
            return result;
        }

        // 按比例重生 type  可以是RED|BLUE|WHITE的组合
        public List<Person> reviveByPercent(int type, int percent, int percent_blood) {
            int max = getDeadCountByType(type);
            int count = max * percent / 100;
            List<Person> result = new List<Person>(count);
            if ((type & RED) != 0) {
                int t = count * red_dead.Count / max;
                result.AddRange(move(red_dead, red_alive, t));
            }
            if ((type & BLUE) != 0) {
                int t = count * blue_dead.Count / max;
                result.AddRange(move(blue_dead, blue_alive, t));
            }
            if ((type & WHITE) != 0) {
                int t = count * white_dead.Count / max;
                result.AddRange(move(white_dead, white_alive, t));
            }
            for (int i = 0; i < result.Count; i++) {
                result[i].revive(percent_blood);
            }
            return result;
        }

        private List<Person> roundPart(List<Person> from, List<Person> removed, ThreeRangeValue.RangeType type)
        {
            List<Person> remain = new List<Person>();
            for (int i = 0; i < from.Count; i++)
            {
                from[i].round(blue_nature_transfer, red_nature_transfer);
                if (from[i].getFaith().getRangeType() != type)
                {
                    removed.Add(from[i]);
                }
                else
                {
                    remain.Add(from[i]);
                }
            }
            return remain;
        }

        // 每个一定时间需要调用一次 比如一秒调用一次 会触发自然转化
        public void round() {
            List<Person> replacement = new List<Person>();
            red_alive = roundPart(red_alive, replacement, ThreeRangeValue.RangeType.HIGH);
            blue_alive = roundPart(blue_alive, replacement, ThreeRangeValue.RangeType.LOW);
            white_alive = roundPart(white_alive, replacement, ThreeRangeValue.RangeType.MIDDLE);
            for (int i = 0; i < replacement.Count; i++) {
                switch(replacement[i].getFaith().getRangeType()) {
                    case ThreeRangeValue.RangeType.HIGH:
                        red_alive.Add(replacement[i]);
                        break;
                    case ThreeRangeValue.RangeType.LOW:
                        blue_alive.Add(replacement[i]);
                        break;
                    case ThreeRangeValue.RangeType.MIDDLE:
                        white_alive.Add(replacement[i]);
                        break;
                }
            }
        }

        private void shuffleArray(List<Person> array) {
            for (int i = 0; i < array.Count; i++) { // shuffle
                int index = random.Next(i, array.Count);                
                Person t = array[i];
                array[i] = array[index];
                array[index] = t;
            } 
        }

        private int getDeadCountByType(int type) {
            int max = 0;
            if ((type & RED) != 0) {
                max = max + red_dead.Count;
            }
            if ((type & BLUE) != 0) {
                max = max + blue_dead.Count;
            }
            if ((type & WHITE) != 0) {
                max = max + white_dead.Count;
            }
            return max;
        }

        private List<Person> move(List<Person> a, List<Person> b, int count) {
            count = a.Count > count ? count : a.Count;
            List<Person> result = new List<Person>(count);
            for (int i = 0; i < count; i++) {
                b.Add(a[i]);
                result.Add(a[i]);
            }
            a.RemoveRange(0, count);
            return result;
        }

    }
}