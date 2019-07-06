using System;
using System.Collections.Generic;

namespace gamejam {
    public abstract class ICard
    {
        public enum Level {
            V1,
            V2,
            V3,
        }

        private string name;
        private Level level = Level.V1;
        private DateTime start_time;
        private DateTime end_time;
        private int duration = 0;

        public ICard(string name, int duration) {
            this.name = name;
            this.duration = duration;
        }

        public string getName() { return name; }

        public void setLevel(Level level) {
            this.level = level;
        }

        public Level getLevel() { return level; }

        public int getDuration() { return duration; }

        public int getRemainingTime() {
            if (duration == 0) return 0;
            TimeSpan span = DateTime.UtcNow.Subtract(this.end_time);
            return span.Hours * 3600 + span.Minutes * 60 + span.Seconds;
        }

        public List<Person> effect(int type, Human human) {
            List<Person> group = null;
            switch(this.level) {
                case Level.V1: group = effectV1(type, human); break;
                case Level.V2: group = effectV2(type, human); break;
                case Level.V3: group = effectV3(type, human); break;
            }
            this.start_time = DateTime.UtcNow;
            this.end_time = DateTime.UtcNow.AddSeconds(duration);
            return group;
        }

        abstract public List<Person> effectV1(int type, Human human);
        abstract public List<Person> effectV2(int type, Human human);
        abstract public List<Person> effectV3(int type, Human human);
    }
}