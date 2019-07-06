namespace gamejam {
    public class RangeValue {

        private int min;
        private int max;

        private int current;

        public RangeValue(int min, int max, int current) {
            this.min = min;
            this.max = max;
            this.current = current;
        }

        public RangeValue(int min, int max, bool as_max) {
            this.min = min;
            this.max = max;
            this.current = as_max ? max : min;
        }

        public int getMin() { return min; }

        public int getMax() { return max; }

        public int getValue() { return current; }

        public void getMin(int min) {
            this.min = min;
        }

        public void setMax(int max) {
            this.max = max;
        }

        public void setValue(int current) {
            this.current = current;
        }

        public bool asMin() {
            return this.current == this.min;
        }

        public void setAsMin() {
            this.current = this.min;
        }

        public bool asMax() {
            return this.current == this.max;
        }

        public void setAsMax() {
            this.current = this.max;
        }

        public int updateValue(int value) {
            int t = this.current + value;
            if (t > this.max) {
                this.current = this.max;
            } else if (t < this.min) {
                this.current = this.min;
            } else {
                this.current += value;
            }
            return this.current;
        }       

        public int updatePercent(int percent) {
            if (percent > 100) { percent = 100; }
            if (percent < -100) { percent = -100; }
            // mapping value to [0 - 100]
            int t = (this.current - this.min) * 100 / (this.max - this.min);
            // incr percent
            t = t + t * percent / 100;
            // mapping value to [min, max]
            this.current = t * (this.max - this.min) / 100 + min;
            return this.current;
        }
    }
}