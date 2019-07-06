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

        public Human(int total, int no_faith) {
            int half = (total - no_faith) / 2;
            no_faith = total - 2 * half;
            human = new List<Person>(total);
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
                person.setNature(new RangeValue(0, 100, random.Next(0, 100)));
                person.setMood(new RangeValue(-100, 100, random.Next(-100, 100)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setPhysical(new RangeValue(0, 100, random.Next(0, 100)));
                person.setSpirit(new RangeValue(0, 100, random.Next(0, 100)));
                person.setSensibility(new RangeValue(0, 100, random.Next(0, 100)));
                human.Add(person);
                white_alive.Add(person);
            }
            for (int i = 0; i < half; i++)
            { // blue
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(-100, -25)));
                person.setNature(new RangeValue(0, 100, random.Next(0, 100)));
                person.setMood(new RangeValue(-100, 100, random.Next(-100, 100)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setPhysical(new RangeValue(0, 100, random.Next(0, 100)));
                person.setSpirit(new RangeValue(0, 100, random.Next(0, 100)));
                person.setSensibility(new RangeValue(0, 100, random.Next(0, 100)));
                human.Add(person);
                blue_alive.Add(person);
            }
            for (int i = 0; i < half; i++)
            { // red
                var person = new Person();
                person.setFaith(new ThreeRangeValue(-100, -25, 25, 100, random.Next(25, 100)));
                person.setNature(new RangeValue(0, 100, random.Next(0, 100)));
                person.setMood(new RangeValue(-100, 100, random.Next(-100, 100)));
                person.setBlood(new RangeValue(0, random.Next(1, 100), true));
                person.setPhysical(new RangeValue(0, 100, random.Next(0, 100)));
                person.setSpirit(new RangeValue(0, 100, random.Next(0, 100)));
                person.setSensibility(new RangeValue(0, 100, random.Next(0, 100)));
                human.Add(person);
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

        public List<Person> selectByCount(int type, int count) {
            // TODO(livexmm)
            return null;
        }

        public List<Person> selectByPercent(int type, int percent) {
            // TODO(livexmm)
            return null;
        }

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

        private void shuffleArray(List<Person> array) {
            for (int i = 0; i < array.Count; i++) { // shuffle
                int index = random.Next(i, array.Count);                
                Person t = array[i];
                array[i] = array[index];
                array[index] = t;
            } 
        }
    }
}