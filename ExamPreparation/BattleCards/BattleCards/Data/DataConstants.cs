namespace BattleCards.Data
{
    class DataConstants
    {
        public const int IdMaxLength = 40;
       
        public const int UsernameMaxLength = 20;
        public const int UsernameMinLength = 5;
        public const int PasswordMaxLength = 20;
        public const int PasswordMinLength = 6;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int CardDescriptionMaxLength = 200;
        public const int CardNameMinLength = 5;
        public const int CardNameMaxLength = 15;
    }
}
