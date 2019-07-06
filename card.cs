using System;

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

        public void effect(Human human) {
            switch(this.level) {
                case Level.V1: effectV1(human); break;
                case Level.V2: effectV2(human); break;
                case Level.V3: effectV3(human); break;
            }
            this.start_time = DateTime.UtcNow;
            this.end_time = DateTime.UtcNow.AddSeconds(duration);
        }

        abstract public void effectV1(Human human);
        abstract public void effectV2(Human human);
        abstract public void effectV3(Human human);
    }
}