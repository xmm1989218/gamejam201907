namespace gamejam {
    public class Person
    {
        // 偏向谁 
        // [-100, -25) -> blue, [-25, 25) -> white, (25, 100) -> red 
        private ThreeRangeValue faith = null;
        // 血量 如果为0 那么死亡 [0, 100], 每个人的生命上限制是不同的
        private RangeValue blood = null;
        // 个人是偏向于胆小得还是果敢的，越是胆小的越是容易受到恐惧，反之亦然
        // [0, 100]
        private RangeValue nature = null;
        // 情绪随场景变化这个值会变化 [-10, 10] 
        private RangeValue mood = null;
        // 体能遇到体能类攻击越高越不容易倒戈 [0, 10]
        private RangeValue physical = null;
        // 精神力遇到精神类攻击不容易产生倒戈 [0, 10]
        private RangeValue spirit = null;
        // 敏感度遇到正向效果时容易被感化 [0, 100]
        private RangeValue sensibility = null; 
        // 是否永生 在受到春哥永生卡复活之后会变成死不了
        // private bool is_never_dead = false;

        string group = null;

        public Person() {}

        public ThreeRangeValue getFaith() { return faith; }

        public void setFaith(ThreeRangeValue faith) {
            this.faith = faith;
        }

        public RangeValue getNature() { return nature; }

        public void setNature(RangeValue nature) {
            this.nature = nature;
        }

        public RangeValue getMood() { return mood; }

        public void setMood(RangeValue mood) {
            this.mood = mood;
        }

        public RangeValue getPhysical() { return physical; }

        public void setPhysical(RangeValue physical) {
            this.physical = physical;
        }
        public RangeValue getSpirit() { return spirit; }

        public void setSpirit(RangeValue spirit) {
            this.spirit = spirit;
        }

        public RangeValue getSensibility() { return sensibility; }

        public void setSensibility(RangeValue sensibility) {
            this.sensibility = sensibility;
        }

//        public bool isNeverDead() { return is_never_dead; }
//
//        public void maskAsNeverDead(bool is_never_dead) {
//            this.is_never_dead = is_never_dead;
//        }        

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