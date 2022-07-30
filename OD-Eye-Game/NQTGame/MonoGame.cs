namespace NQTGame
{
    public abstract class MonoGame {
        private TimeOut updateTime = new TimeOut();
        protected int UpdateTime { get => updateTime.time; set => updateTime.time = (value > 0) ? value : updateTime.time; }

        private TimeOut fixedUpdateTime = new TimeOut();
        protected int FixedUpdateTime { get => fixedUpdateTime.time; set => fixedUpdateTime.time = (value > 0) ? value : fixedUpdateTime.time; }
        protected bool isPlayGame { get => !updateTime.isStop; set { updateTime.isStop = fixedUpdateTime.isStop = !value; } }
        public MonoGame()
        {
            updateTime.time = (int)1000 / 60;
            fixedUpdateTime.time = 200;
            Awake();
            Start();
            isPlayGame = true;
            updateTime.Handle += UpdateTime_Handle;
            fixedUpdateTime.Handle += FixedUpdateTime_Handle;
            updateTime.Start();
            fixedUpdateTime.Start();
        }

        private void FixedUpdateTime_Handle(object sender, TimeOutResponse e)
        {
            FixedUpdate();
        }

        private void UpdateTime_Handle(object sender, TimeOutResponse e)
        {
            Update();
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}
