namespace SnakeConsole
{
    public abstract class GameObject 
    {
        private int size;

        #region Costructors
        public GameObject () { }

        public GameObject(Position pos)
        {
            this.Position = pos;
            Size = size;
        }

        public GameObject(int x, int y, int size)
            :this(new Position(x, y), size) 
        { }
      
        public GameObject(Position position, int size)
        {
            this.Position = new Position(position.X, position.Y);
            this.Size = size;
        }

        #endregion

        public virtual int Size
        {
            get {return this.size;}
            set {this.size = value;}
        }

        public virtual Position Position { get; set; }
    }
}
