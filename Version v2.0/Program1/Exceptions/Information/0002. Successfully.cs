using System;

namespace Program1.Exceptions.Information
{
    /// <summary> Класс-сообщение «Успешно» </summary>
    public class Successfully : Exception
    {
        public Successfully() : base($"Успешно")
            => Data.Add("Say", "Результат");
    }
}