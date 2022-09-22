namespace Platformer
{
    public class EventsLibrary
    {
        public static string PlayRandomGame = "PlayRandomGame"; //случайная генерация уровня
        public static string PlaySeedGame = "PlaySeedGame"; // генерация уровня с использованием определенного сида
        public static string PlayCreateGame = "PlayCreateGame"; // ручное создание уровня из поатформ и/или противников
        public static string ChacterGotDamage = "ChacterGotDamage"; // персонаж получил урон

        public static string CallLevelCreate = "CallLevelCreate"; // Запрос создания уровня
        public static string LevelCreated = "LevelCreated"; // уровень создан
        public static string CharacterCreated = "CharacterCreated"; // уровень создан

        public static string GameStarted = "GameStarted"; // Игра запущена
        public static string GamePaused = "GamePaused"; // Игра поставлена на паузу
        public static string GameContinued = "GameContinued"; // Игра продолжена
        public static string GameReloaded = "GameReloaded"; // Игра продолжена
        public static string GameEnded = "GameEnded"; // Игра закончена
    }
}
