namespace Program1
{
    public class Insecticid
    {
        public string ID { private set; get; }

        public string Name { private set; get; }

        public int Count { private set; get; }

        /// <summary> Конструктор </summary>
        public Insecticid(string NewID, string NewName, int NewCount)
        { ID = NewID; Name = NewName; Count = NewCount; }
    }
}