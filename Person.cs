namespace gamejam {
    public class Person
    {
        // 偏向谁 
        // [-100, -25) -> blue, [-25, 25) -> white, (25, 100) -> red 
        private ThreeRangeValue faith = null;

        // 血量 如果为0 那么死亡 [0, 100], 每个人的生命上限制是不同的
        private RangeValue blood = null;

        // 个人是偏向于胆小得还是果敢的，越是胆小的越是容易受到恐惧，反之亦然
        // [-10, 10] 不会变
        private RangeValue nature = null;

        // 情绪随场景变化这个值会变化 [-10, 10] 
        private RangeValue sensibility = null; 

        public Person() {}

        public ThreeRangeValue getFaith() { return faith; }

        public void setFaith(ThreeRangeValue faith) {
            this.faith = faith;
        }

        public RangeValue getNature() { return nature; }

        public void setNature(RangeValue nature) {
            this.nature = nature;
        }

        public RangeValue getSensibility() { return sensibility; }

        public void setSensibility(RangeValue sensibility) {
            this.sensibility = sensibility;
        }

        public RangeValue getBlood() { return blood; }

        public void setBlood(RangeValue blood) {
            this.blood = blood;
        }

        public bool dead() {
            // return isNeverDead() == false && blood.asMin();
            return blood.asMin();
        }

        public bool kill() {
            // if (isNeverDead()) return false;           
            blood.setAsMin();
            return true;
        }

        public bool revive(int percent_blood) {
            blood.updatePercent(percent_blood);
            return true;
        }
    }
}