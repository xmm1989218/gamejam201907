namespace gamejam {

    public class ThreeRangeValue {
        public enum RangeType {
            LOW,
            MIDDLE,
            HIGH,
        }

        private int min;
        private int middle_min;
        private int middle_max;
        private int max;
        private int current;

        public ThreeRangeValue(int min, int middle_min, int middle_max, int max, int current) {
            this.min = min;
            this.max = max;
            this.middle_min = middle_min;
            this.middle_max = middle_max;
            this.current = current;
        }

        public int getMin() { return min; }

        public int getMax() { return max; }

        public int getValue() { return current; }

        public int getLowRange() {
            return middle_min - min;
        }

        public int getMiddleRange() {
            return middle_max - middle_min;
        }

        public int getHighRange() {
            return max - middle_max;
        }

        public ThreeRangeValue.RangeType getRangeType() {
            if (current < middle_min) {
                return ThreeRangeValue.RangeType.LOW;
            }
            if (current < middle_max) {
                return ThreeRangeValue.RangeType.MIDDLE;
            }
            return ThreeRangeValue.RangeType.HIGH;
        }

        public void getMin(int min) {
            this.min = min;
        }

        public void setMax(int max) {
            this.max = max;
        }

        public void setValue(int current) {
            this.current = current;
        }

        public void setAsMin() {
            this.current = this.min;
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