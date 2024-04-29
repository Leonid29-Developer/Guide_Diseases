using System;

namespace Program1.Exceptions.Errors
{
    /// <summary> Error #0002 <br/> 
    /// Класс-исключение «Неудачная попытка регистрации» </summary>
    public class Registration : Exception
    {
        public Registration(string ER) : base($"Неудачная попытка регистрации{ER}")
            => Data.Add("Kod", "Ошибка #0002");
    }
}